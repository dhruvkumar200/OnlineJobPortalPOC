using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OJP.Business;
using OJP.Data.Entities;
using OJP.Models;
using OnlJbPt.Models;





namespace OnlJbPt.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserBusiness _iUserBusiness;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, IUserBusiness iUserBusiness)
    {
        _logger = logger;
        _iUserBusiness = iUserBusiness;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult LoginForm()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginModel loginModel)
    {
        var userDetails = _iUserBusiness.GetUserDetailByEmail(loginModel.Email);

        if (userDetails != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, userDetails.Password))
        {

            var claims = new Claim[] { new Claim(ClaimTypes.Email, userDetails.Email), new Claim(ClaimTypes.Role, userDetails.RoleId.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            if (userDetails.RoleId == (int)Common.RoleType.Recruiter || userDetails.RoleId == (int)Common.RoleType.Admin)
            {
                return RedirectToAction("JobSeekerDetail", "Home");
            }
            else
            {
                return RedirectToAction("JobPostDetail", "Home");
            }

        }
        else
        {
            ViewData["Errormsg"] = "Incorrect Email or Password";
            return View("Index");
        }



    }
    public IActionResult AddUserForm()
    {
        return View();
    }
    public IActionResult AddUser(AddUserModel addUserModel)
    {
        _iUserBusiness.NewRegistration(addUserModel);

        return RedirectToAction(actionName: "Index", controllerName: "Home");

    }
    public IActionResult RegisterSeekerpage()
    {
        return View();
    }
    public IActionResult RegisterSeeker(AddUserModel addUserModel, IFormFile Profile)
    {
        string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        List<string> uploadedFiles = new List<string>();
        string fileName = Path.GetFileName(Profile.FileName);
        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        {
            Profile.CopyTo(stream);
            uploadedFiles.Add(fileName);
            ViewBag.Message += string.Format("<b>{0}</b> Profile pic uploaded.<br />", fileName);
        }
        _iUserBusiness.RegisterJobseeker(addUserModel);
        return RedirectToAction(actionName: "Index", controllerName: "Home");

    }

    public IActionResult RecruiterProfilepage()
    {
        return View();
    }
    public IActionResult RecruiterSignUpPage()
    {
        return View();
    }

    public IActionResult JobPostPage()
    {
        return View();
    }
    public IActionResult JobPost(JobPostModel jobPost, IFormFile Profile)
    {

        _iUserBusiness.PostJob(jobPost);
        return RedirectToAction(actionName: "Index", controllerName: "Home");

    }

    public IActionResult PostedJobsDetail()
    {
        return View();
    }

    [Authorize(Roles = "1,3")]
    public IActionResult JobSeekerDetail(string Search_Data)
    {
        ViewBag.Search_Data = !String.IsNullOrEmpty(Search_Data) ? Search_Data : null;
        var claims = User.Identities.First().Claims.ToList();
        var claimRole = claims?.FirstOrDefault(x => x.Type.Contains("Role", StringComparison.OrdinalIgnoreCase))?.Value;
        int roleId = Convert.ToInt32(claimRole);
        IEnumerable<Login> UserList = _iUserBusiness.GetUserList(ViewBag.Search_Data, roleId);
        UserList=UserList.ToList();
        //  var list=UserList.ToPagedList(pageNumber, pageSize);
        return View(UserList);
    }

    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyEmail(string email)
    {
        if (_iUserBusiness.VerifyEmail(email) == true)
        {
            return Json($"Email {email} is already in use.");
        }
        return Json(true);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult RecruiterProfile()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
