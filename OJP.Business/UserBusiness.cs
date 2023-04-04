using OJP.Data.Entities;
using OJP.Models;
using OJP.Repository;

namespace OJP.Business
{
    public class UserBusiness : IUserBusiness
    {
        public readonly IUserRepository _iUserRepository;

        public UserBusiness(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }



        public Login GetUserDetailByEmail(String Email)
        {

            return _iUserRepository.GetUserDetailByEmail(Email);
        }
        public bool NewRegistration(AddUserModel addUserModel)
        {
            return _iUserRepository.AddUser(addUserModel);
        }

        public bool RegisterJobseeker(AddUserModel addUserModel)
        {
            return _iUserRepository.AddUser(addUserModel);
        }

        public bool PostJob(JobPostModel jobPost)
        {
            return _iUserRepository.PostJob(jobPost);
        }

        public IEnumerable<Login> GetUserList(string Search_Data, int roleId)
        {
            return _iUserRepository.GetUserList(Search_Data, roleId);
        }
        public bool VerifyEmail(string email)
        {
            return _iUserRepository.VerifyEmail(email);
        }



    }

}