using System.Security.Claims;
using OJP.Data.Entities;
using OJP.Models;
using static OJP.Models.Common;

namespace OJP.Repository

{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _context;
        public UserRepository(UserDBContext context)
        {
            _context = context;
        }
        public bool AddUser(AddProfileModel addUser)
        {
            Login user = new Login();
            user.FirstName = addUser.FirstName;
            user.LastName = addUser.LastName;
            user.Email = addUser.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
            user.Phone = addUser.Phone;
            user.Age = addUser.Age;
            user.Profile = addUser.Profile;
            user.Address = addUser.Address;
            user.CreatedAt = DateTime.Now;
            user.RoleId = (int?)addUser.RoleType;
            _context.Add(user);
            return _context.SaveChanges() > 0;
        }

        public Login GetUserDetailByEmail(string Email)
        {


            return _context.Logins.FirstOrDefault(x => x.Email == Email);

        }

        public IEnumerable<Login> GetUserList(string search, int roleId)
        {


            IEnumerable<Login> userAccount = null;
            if (roleId == (int)Common.RoleType.Recruiter)
            {
                userAccount = _context.Logins.Where(x => x.RoleId == (int)Common.RoleType.Seeker);
            }
            else
            {
                userAccount = _context.Logins.Where(x => x.RoleId != (int)Common.RoleType.Admin);
            }
            if (userAccount.Count() > 0 && !string.IsNullOrEmpty(search))
            {
                userAccount = _context.Logins.Where(x => x.FirstName.Contains(search));
            }
            userAccount = userAccount.OrderBy(stu => stu.FirstName).ToList();
            return userAccount;
        }


        public bool Education(EducationDetailModel edm)
        {

            EducationDetail educationDetail = new EducationDetail();
            educationDetail.Institute = edm.Institute;
            educationDetail.Percentage = edm.Percentage;
            educationDetail.Cgpa = edm.Cgpa;
            educationDetail.Specilization = edm.Specilization;
            educationDetail.Resume = edm.Resume;
            educationDetail.CreatedBy = edm.CreatedBy;
            TechnicalDetail technicalDetail = new TechnicalDetail();
            technicalDetail.JobTitle = edm.JobTitle;
            technicalDetail.CompanyName = edm.CompanyName;
            technicalDetail.Experience = edm.Experience;
            technicalDetail.Description = edm.Description;
            technicalDetail.TechSkills = edm.TechSkills;
            technicalDetail.CreatedBy = edm.CreatedBy;
            _context.Add(educationDetail);
            _context.Add(technicalDetail);
            return _context.SaveChanges() > 0;
        }

        public bool VerifyEmail(string email)
        {
            return _context.Logins.Any(x => x.Email == email);
        }
        public bool IsDetailAdded(int id)
        {
            return _context.TechnicalDetails.Any(x => x.CreatedBy == id) ? true : false;
        }


        public Login Profile(string emailId)
        {
            return _context.Logins.FirstOrDefault(x => x.Email == emailId);

        }
         public bool EditProfileDetail(EditProfileModel editProfileModel)
        {
            Login user = new Login();
            user.FirstName = editProfileModel.FirstName;
            user.LastName = editProfileModel.LastName;
            user.Email = editProfileModel.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(editProfileModel.Password);
            user.Phone = editProfileModel.Phone;
            user.Age = editProfileModel.Age;
            user.Profile = editProfileModel.Profile;
            user.Address = editProfileModel.Address;
            user.CreatedAt = DateTime.Now;
            user.RoleId = (int?)editProfileModel.RoleType;
            user.Id = editProfileModel.Id;
            _context.Update(user);
            return _context.SaveChanges() > 0;
        }
        public EditProfileModel GetUserById(int id)
        {
            var detail = _context.Logins.FirstOrDefault(x => x.Id == id);
            EditProfileModel editProfileModel = new EditProfileModel();
            editProfileModel.Email = detail.Email;
            editProfileModel.FirstName = detail.FirstName;
            editProfileModel.LastName = detail.LastName;
            editProfileModel.Address = detail.Address;
            editProfileModel.Age = detail.Age;
            editProfileModel.Phone = detail.Phone;
            editProfileModel.Profile = detail.Profile;
            editProfileModel.Profile = detail.Password;

            return editProfileModel;
        }
    }
}