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
        public bool AddUser(AddEditProfileModel addUser)
        {
            Login user = new Login();
            user.FirstName = addUser.FirstName;
            user.LastName = addUser.LastName;
            user.Email = addUser.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
            user.Phone = addUser.Phone;
            user.Age = addUser.Age;
            user.Profile = addUser.Profile;
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
        public AddEditProfileModel GetUserById(int id)
        {
            var detail=_context.Logins.FirstOrDefault(x=>x.Id==id);
            AddEditProfileModel addEditProfileModel=new AddEditProfileModel();
            addEditProfileModel.Email=detail.Email;
            addEditProfileModel.FirstName=detail.FirstName;
            addEditProfileModel.Password=detail.Password;
            addEditProfileModel.Age=detail.Age;
            addEditProfileModel.Address=detail.Address;
            addEditProfileModel.Phone=detail.Phone;
            addEditProfileModel.Profile=detail.Profile;
            return addEditProfileModel;
        }
    }
}