using System.Collections;
using Microsoft.EntityFrameworkCore;
using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Repository
{
    public class PostJobRepository : IPostJobRepository
    {
        private readonly UserDBContext _context;
        public PostJobRepository(UserDBContext context)
        {
            _context = context;
        }
        public bool PostJob(JobPostModel jobPost)
        {
            JobPost post = new JobPost();
            post.JobTitle = jobPost.Title;
            post.JobDescription = jobPost.Description;
            post.JobType = (int)jobPost.JobType;
            post.Location = jobPost.Location;
            post.Salary = jobPost.Salary;
            post.CompanyName = jobPost.CompanyName;
            post.Email = jobPost.ContactEmail;
            post.StartDate = DateTime.Now;
            post.EndDate = jobPost.EndDate;
            post.Phone = jobPost.ContactPhone;
            post.PostedBy = jobPost.Postedby;
            _context.Add(post);
            return _context.SaveChanges() > 0;
        }
        public bool ApplyJob(ApplyJobModel applyJobModel)
        {
            var existingApply = _context.JobApplies.FirstOrDefault(x => x.ApplyBy == applyJobModel.ApplyBy && x.JobPostId == applyJobModel.JobPostId);
            if (existingApply != null)
            {
                return false;
            }
            JobApply jobApply = new JobApply();
            jobApply.AppliedAt = DateTime.Now;
            jobApply.ApplyBy = applyJobModel.ApplyBy;
            jobApply.JobPostId = applyJobModel.JobPostId;
            jobApply.Status = (int)Common.JobStatus.InProgress;
            _context.Add(jobApply);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<JobPost> GetJobPosts(int id)
        {
            return _context.JobPosts.Include(x => x.PostedByNavigation).ToList();
        }
        public IEnumerable<JobApply> GetSeekerAppliedJob(int id)
        {
            return _context.JobApplies.Include(x => x.JobPost).Include(x => x.ApplyByNavigation)
            .Where(x => x.ApplyBy == id).OrderByDescending(x => x.AppliedAt).ToList();
        }
        public bool DeleteAppliedJob(int id)
        {
            var detail = _context.JobApplies.FirstOrDefault(x => x.Id == id);
            if (detail != null)
            {
                _context.JobApplies.Remove(detail);
                _context.SaveChanges();
            }
            return true;
        }

    }
}
