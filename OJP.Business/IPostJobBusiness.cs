
using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Business
{
    public interface IPostJobBusiness
    {
        bool PostJob(JobPostModel jobPost);
        IEnumerable<JobPost> GetJobPosts(int id);
        bool ApplyJob(ApplyJobModel applyJobModel);
        public IEnumerable<JobApply> GetSeekerAppliedJob(int id);
        bool DeleteAppliedJob(int id);
    }
}