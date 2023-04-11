using System.Diagnostics;
using System.Linq;
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
        if (_iPostJobBusiness.ApplyJob(applyJobModel) == true)
        {
            ViewData["JobApplymsg"] = "Applied sucessfully";
        }
        else
        {
            ViewData["JobApplymsg"] = "Already Applied";
        }

        return RedirectToAction("SeekerAppliedJob", "JobPost");

    }

    public IActionResult PostedJobsDetail(string Search_Data,int pg = 1)
    {
        ViewBag.Search_Data = !String.IsNullOrEmpty(Search_Data) ? Search_Data : null;
        var claims = User.Identities.First().Claims.ToList();
        var claimRole = claims?.FirstOrDefault(x => x.Type.Contains("Role", StringComparison.OrdinalIgnoreCase))?.Value;
        int roleId = Convert.ToInt32(claimRole);
        IEnumerable<JobPost> PostedList = _iPostJobBusiness.GetJobPosts(roleId);
         const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = PostedList.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = PostedList.Skip(recSkip).Take(pager.Pagesize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
    }
    public IActionResult SeekerAppliedJob()
    {
        var id = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        var applyJobList = _iPostJobBusiness.GetSeekerAppliedJob(id);
        return View(applyJobList);
    }
    public IActionResult DeleteAppliedJob(int id)
    {
        _iPostJobBusiness.DeleteAppliedJob(id);
        return RedirectToAction("SeekerAppliedJob", "JobPost");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
