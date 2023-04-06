using System.ComponentModel.DataAnnotations;

namespace OJP.Models
{
    public class EducationDetailModel
    {
        [Required(ErrorMessage = "Required")]
        public string? Institute { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal Percentage { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal Cgpa { get; set; }

        [Required(ErrorMessage = "Rrequired")]
        public string? Specilization { get; set; }

        [Required(ErrorMessage = "Rrequired")]
        public string? Resume { get; set; }
    
        public string CompanyName { get; set; }
        public decimal? Experience { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string TechSkills { get; set; }
        public string JobTitle { get; set; }
        public int CreatedBy { get; set; }
     
       
    }
}
