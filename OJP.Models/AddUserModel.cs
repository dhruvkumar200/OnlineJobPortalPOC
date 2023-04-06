using System.ComponentModel.DataAnnotations;
using static OJP.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace OJP.Models
{
    public class AddEditProfileModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Rrequired")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}", ErrorMessage = "Incorrect Email Format")]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Home")]
        public string? Email { get; set; }
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Required(ErrorMessage = "Required")]   
        public string? Password { get; set; }
        [Required(ErrorMessage = "Rrequired")]    
        [DataType(DataType.Password)]    
        [Compare("Password")] 
        public string? ConfirmPassword{ get; set;}
        public int? Age { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Required")] 
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string Phone { get; set; }
        public string? Address { get; set; }
        [Required]
        public string CurrentCity { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PinCode { get; set; }

        public bool? IsVerified { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public string Qualification { get; set; }
        public string Resume { get; set; }
        
        public RoleType RoleType{get; set;}
        public string Profile{ get; set;}
    }
}