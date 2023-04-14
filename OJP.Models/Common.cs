namespace OJP.Models
{
    public class Common
    {
        public enum RoleType
        {
            Recruiter = 1,
            Seeker = 2,
            Admin = 3
        }
        public const string Recruiter = "1";
        public const string Seeker = "2";
        public const string Admin = "3";
        public enum JobStatus
        {
            InProgress = 1,
            Scheduled = 2,
            Rejected = 3
        }
        public enum Category
        {
            Finance,
            Sales,
            Developer,
            UXDegigner,
            BusinessAnalyst

        }
        public enum JobType
        {
            Remote=1,
            Office=2
        }
        
    }


}