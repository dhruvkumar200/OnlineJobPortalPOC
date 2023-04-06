using System.ComponentModel.DataAnnotations;
namespace OJP.Models
{
    public class SeekerEducationModel
    {
        [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Job title must be between 1 and 50 characters.")]
    public string CompanyName { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(500, ErrorMessage = "Job description must be between 1 and 500 characters.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Job location must be between 1 and 50 characters.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Currency)]
    public String Mobile { get; set; }
    [Required(ErrorMessage = "Required")]
    [StringLength(50, ErrorMessage = "Company name must be between 1 and 50 characters.")]
    public string Designation { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.EmailAddress)]
    public string City{ get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.PhoneNumber)]
    public int EmployeNumber { get; set; }
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Date)]

    public string ClientRequirement{get; set;}
    }
}