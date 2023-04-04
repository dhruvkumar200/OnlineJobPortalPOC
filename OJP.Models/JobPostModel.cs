
using System.ComponentModel.DataAnnotations;
using static OJP.Models.Common;
namespace OJP.Models
{
    public class JobPostModel
    {
        
    [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Job title must be between 1 and 50 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(500, ErrorMessage = "Job description must be between 1 and 500 characters.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Category { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Job location must be between 1 and 50 characters.")]
    public string Location { get; set; }

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Company name must be between 1 and 50 characters.")]
    public string CompanyName { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    public string ContactEmail { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.PhoneNumber)]
    public string ContactPhone { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    public string JobType{get; set;}

}
public enum TypeOfJob{
    Remote,
    Office
}

    
}