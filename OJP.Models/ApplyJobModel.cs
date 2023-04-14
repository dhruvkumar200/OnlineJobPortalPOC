using System.ComponentModel.DataAnnotations;

namespace OJP.Models
{
    public class ApplyJobModel
    {
        public int Id { get; set; }
        public int ApplyBy { get; set; }
        public int JobPostId { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}