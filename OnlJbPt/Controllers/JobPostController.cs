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

public class JobPostController : Controller
{
    private readonly ILogger<JobPostController> _logger;
    private readonly IUserBusiness _iUserBusiness;
    private readonly IPostJobBusiness _iPostJobBusiness;
    public JobPostController(ILogger<JobPostController> logger, IUserBusiness iUserBusiness, IPostJobBusiness iPostJobBusiness)
    {
        _logger = logger;
        _iUserBusiness = iUserBusiness;
        _iPostJobBusiness = iPostJobBusiness;

    }
    public IActionResult JobPostPage()
    {
        return View();
    }
    public IActionResult PostJob(JobPostModel jobPost, IFormFile Profile)
    {
        jobPost.Postedby = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        _iPostJobBusiness.PostJob(jobPost);
        return RedirectToAction(actionName: "DisplayDetails", controllerName: "Home");

    }
    public IActionResult ApplyJob(ApplyJobModel applyJobModel, int id)
    {
        applyJobModel.ApplyBy = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        applyJobModel.JobPostId = id;
        _iPostJobBusiness.ApplyJob(applyJobModel);
        if (_iPostJobBusiness.ApplyJob(applyJobModel) == true)
        {
            ViewData["JobApplymsg"] = "Applied sucessfully";

        }
        else
        {
            ViewData["JobApplymsg"] = "Already Applied";
        }

        return RedirectToAction("Index", "Home");

    }

    public IActionResult PostedJobsDetail()
    {
        IEnumerable<JobPost> PostedList = _iPostJobBusiness.GetJobPosts();
        PostedList = PostedList.ToList();
        return View(PostedList);
    }
    public IActionResult SeekerAppliedJob()
    {
        var id = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        var applyJobList = _iPostJobBusiness.GetSeekerAppliedJob(id);
        return View(applyJobList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
