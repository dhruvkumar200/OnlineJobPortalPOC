
using Microsoft.AspNetCore.Mvc;
using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Business
{
    public interface IUserBusiness
    {
        public Login GetUserDetailByEmail(String Email);
        bool NewRegistration(AddProfileModel AddProfileModel);
        bool SeekerEducation(EducationDetailModel edm);
        IEnumerable<Login> GetUserList(string Search_Data, int roleId);
        bool VerifyEmail(string email);
        public Login Profile(string emailId);
        public EditProfileModel GetUserById(int id);
        public bool IsDetailAdded(int id);
        public bool EditProfileDetail(EditProfileModel editProfileModel);
     

    }
}