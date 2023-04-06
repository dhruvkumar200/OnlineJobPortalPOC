
using Microsoft.AspNetCore.Mvc;
using OJP.Data.Entities;
using OJP.Models;

namespace OJP.Business
{
    public interface IUserBusiness
    {
        public Login GetUserDetailByEmail(String Email);
        bool NewRegistration(AddEditProfileModel AddEditProfileModel);
        bool SeekerEducation(EducationDetailModel edm);
        IEnumerable<Login> GetUserList(string Search_Data, int roleId);
        bool VerifyEmail(string email);
        public Login Profile(string emailId);
        public AddEditProfileModel GetUserById(int id);
        public bool IsDetailAdded(int id);
    }
}