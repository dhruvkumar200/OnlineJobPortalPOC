using Microsoft.AspNetCore.Mvc;
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
        public bool NewRegistration(AddProfileModel AddProfileModel)
        {
            return _iUserRepository.AddUser(AddProfileModel);
        }

        public bool SeekerEducation(EducationDetailModel edm)
        {
            return _iUserRepository.Education(edm);
        }
        public IEnumerable<Login> GetUserList(string Search_Data, int roleId)
        {
            return _iUserRepository.GetUserList(Search_Data, roleId);
        }
        public bool VerifyEmail(string email)
        {
            return _iUserRepository.VerifyEmail(email);
        }
        public Login Profile(string emailId)
        {
            return _iUserRepository.Profile(emailId);
        }

        public EditProfileModel GetUserById(int id)
        {
            return _iUserRepository.GetUserById(id);
        }
        public bool IsDetailAdded(int id)
        {
            return _iUserRepository.IsDetailAdded(id);
        }
        public bool EditProfileDetail(EditProfileModel editProfileModel)
        {
            return _iUserRepository.EditProfileDetail(editProfileModel);
        }
        public Login GetSeekerDetailById(int id)
        {
            return _iUserRepository.GetSeekerDetailById(id);
        }
    }

}