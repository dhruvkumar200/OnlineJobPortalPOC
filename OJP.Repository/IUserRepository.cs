using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Repository
{
    public interface IUserRepository
    {
        bool AddUser(AddUserModel addUser);
        bool PostJob(JobPostModel jobPost);
        public IEnumerable<Login> GetUserList(string Search_Data, int roleId);
        public Login GetUserDetailByEmail(String Email);
        public bool VerifyEmail(string email);



    }
}
