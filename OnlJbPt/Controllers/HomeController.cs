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
using PayPal.Api;

namespace OnlJbPt.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserBusiness _iUserBusiness;
    private readonly IPostJobBusiness _iPostJobBusiness;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, IUserBusiness iUserBusiness,
     IPostJobBusiness iPostJobBusiness, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
    {
        _logger = logger;
        _iUserBusiness = iUserBusiness;
        _iPostJobBusiness = iPostJobBusiness;
        Environment = _environment;
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
            var claims = new Claim[] { new Claim(ClaimTypes.Email, userDetails.Email), new Claim(ClaimTypes.Role, userDetails.RoleId.ToString()), new Claim("UserId", userDetails.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            if (userDetails.RoleId == (int)Common.RoleType.Recruiter || userDetails.RoleId == (int)Common.RoleType.Admin)
            {
                return RedirectToAction("DisplayDetails", "Home");
            }
            else
            {
                if (_iUserBusiness.IsDetailAdded(userDetails.Id))
                {
                    return RedirectToAction("PostedJobsDetail", "JobPost");
                }
                else
                {
                    return RedirectToAction("SeekerEducationForm", "Home");
                }
            }
        }
        else
        {
            ViewData["Errormsg"] = "Incorrect Email or Password";
            return View("LoginForm");
        }

    }
    public IActionResult AddProfile()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddUser(AddProfileModel addProfileModel, IFormFile Profile)
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
        addProfileModel.Profile = fileName;
        _iUserBusiness.NewRegistration(addProfileModel);
        if (addProfileModel.RoleType == Common.RoleType.Seeker)
        {
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        else
        {
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

    }

    [Authorize(Roles = "1,3")]
    public IActionResult DisplayDetails(string Search_Data, int pg = 1)
    {
        ViewBag.Search_Data = !String.IsNullOrEmpty(Search_Data) ? Search_Data : null;
        var claims = User.Identities.First().Claims.ToList();
        var claimRole = claims?.FirstOrDefault(x => x.Type.Contains("Role", StringComparison.OrdinalIgnoreCase))?.Value;
        int roleId = Convert.ToInt32(claimRole);
        IEnumerable<Login> UserList = _iUserBusiness.GetUserList(ViewBag.Search_Data, roleId);
        const int pageSize = 3;
        if (pg < 1)
        {
            pg = 1;
        }
        int recsCount = UserList.Count();
        var pager = new Pager(recsCount, pg, pageSize);
        int recSkip = (pg - 1) * pageSize;
        var data = UserList.Skip(recSkip).Take(pager.Pagesize).ToList();
        this.ViewBag.Pager = pager;
        return View(data);
    }
    public IActionResult SeekerEducationForm()
    {
        return View();
    }
    public IActionResult EducationDetail(EducationDetailModel edm, IFormFile Resume)
    {
        string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        List<string> uploadedFiles = new List<string>();
        string fileName = Path.GetFileName(Resume.FileName);
        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        {
            Resume.CopyTo(stream);
            uploadedFiles.Add(fileName);
            ViewBag.Message += string.Format("<b>{0}</b> Profile pic uploaded.<br />", fileName);
        }
        edm.Resume = fileName;
        edm.CreatedBy = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        var details = _iUserBusiness.SeekerEducation(edm);
        return RedirectToAction("PostedJobsDetail", "JobPost");

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

    public IActionResult Profile()
    {
        var claims = User.Identities.First().Claims.ToList();
        var emailId = claims?.FirstOrDefault(x => x.Type.Contains("Email", StringComparison.OrdinalIgnoreCase))?.Value;
        var details = _iUserBusiness.Profile(emailId);
        return View(details);
    }
    [HttpGet]
    public IActionResult GetProfileDetail(int id)
    {
        EditProfileModel editProfileModel = _iUserBusiness.GetUserById(id);
        return View("EditProfile", editProfileModel);
    }
    public IActionResult ChangeProfileDetail(EditProfileModel editProfileModel, IFormFile Profile)
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
        editProfileModel.Profile = fileName;
        _iUserBusiness.EditProfileDetail(editProfileModel);
        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }
    public IActionResult ViewSeekerDetail(int id)
    {
        var details = _iUserBusiness.GetSeekerDetailById(id);
        return View(details);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
