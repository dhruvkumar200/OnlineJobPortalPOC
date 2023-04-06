using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Repository
{
    public interface IPostJobRepository
    {
        bool PostJob(JobPostModel jobPost);
        public IEnumerable<JobPost> GetJobPosts();
        bool ApplyJob(ApplyJobModel applyJobModel);
        public IEnumerable<JobApply> GetSeekerAppliedJob(int id);
    }
}