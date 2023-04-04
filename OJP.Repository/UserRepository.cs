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
        public bool AddUser(AddUserModel addUser)
        {
            Login user = new Login();
            user.FirstName = addUser.FirstName;
            user.LastName = addUser.LastName;
            user.Email = addUser.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(addUser.Password);
            user.Phone = addUser.Phone;
            user.Age = addUser.Age;
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

        public bool PostJob(JobPostModel jobPost)
        {
            JobPost user = new JobPost();
            user.JobTitle = jobPost.Title;
            user.JobDescription = jobPost.Description;
            user.JobType = jobPost.JobType;
            user.Email = jobPost.Category;
            user.Location = jobPost.Location;
            user.Salary = jobPost.Salary;
            user.CompanyName = jobPost.CompanyName;
            user.Email = jobPost.ContactEmail;
            user.StartDate = jobPost.StartDate;
            user.EndDate = jobPost.EndDate;
            user.Phone = jobPost.ContactPhone;
            _context.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool RecruiterRegistration(RecruiterRegistrationModel rec)
        {

            RecruiterRegistration user = new RecruiterRegistration();
            user.CompanyName = rec.CompanyName;
            user.Email = rec.Email;
            user.Password = rec.Password;
            user.ConfirmPassword = rec.ConfirmPassword;
            user.Mobile = rec.Mobile;
            user.Designation = rec.Designation;
            user.City = rec.City;
            user.EmployeeNumbers = rec.EmployeNumber;
            user.ClientRequirement = rec.ClientRequirement;
            _context.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool VerifyEmail(string email)
        {
            return _context.Logins.Any(x => x.Email == email);
        }
    }
}