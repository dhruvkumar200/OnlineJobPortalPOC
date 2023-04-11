using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Repository
{
    public interface IPostJobRepository
    {
        bool PostJob(JobPostModel jobPost);
        public IEnumerable<JobPost> GetJobPosts(int id);
        bool ApplyJob(ApplyJobModel applyJobModel);
        public IEnumerable<JobApply> GetSeekerAppliedJob(int id);
        bool DeleteAppliedJob(int id);
    }
}