using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Repository
{
    public interface IUserRepository
    {
        bool AddUser(AddProfileModel addUser);

        public IEnumerable<Login> GetUserList(string Search_Data, int roleId);
        public Login GetUserDetailByEmail(String Email);
        public bool VerifyEmail(string email);
        public Login Profile(string emailId);
        bool Education(EducationDetailModel edm);
        public bool IsDetailAdded(int id);
        public EditProfileModel GetUserById(int id);
        public bool EditProfileDetail(EditProfileModel editProfileModel);
        public Login GetSeekerDetailById(int id);


    }
}
