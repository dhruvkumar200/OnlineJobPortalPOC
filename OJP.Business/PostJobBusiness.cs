using OJP.Data.Entities;
using OJP.Models;
using OJP.Repository;

namespace OJP.Business
{
    public class PostJobBusiness : IPostJobBusiness
    {
        public readonly IPostJobRepository _iPostJobRepository;

        public PostJobBusiness(IPostJobRepository iPostJobRepository)
        {
            _iPostJobRepository = iPostJobRepository;
        }

        public bool PostJob(JobPostModel jobPost)
        {
            return _iPostJobRepository.PostJob(jobPost);
        }
        public IEnumerable<JobPost> GetJobPosts(int id)
        {
            return _iPostJobRepository.GetJobPosts(id);
        }
        public bool ApplyJob(ApplyJobModel applyJobModel)
        {
            return _iPostJobRepository.ApplyJob(applyJobModel);
        }
        public IEnumerable<JobApply> GetSeekerAppliedJob(int id)
        {
            return _iPostJobRepository.GetSeekerAppliedJob(id);
        }
        public bool DeleteAppliedJob(int id)
        {
            return _iPostJobRepository.DeleteAppliedJob(id);
        }

    }
}