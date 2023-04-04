
using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Business
{
    public interface IUserBusiness
    {
          public Login GetUserDetailByEmail(String Email);
          bool NewRegistration(AddUserModel addUserModel);
          bool RegisterJobseeker(AddUserModel addUserModel);
          bool PostJob(JobPostModel jobPost);
          IEnumerable<Login> GetUserList(string Search_Data ,int roleId);
          bool VerifyEmail(string email);
           
    }         
}