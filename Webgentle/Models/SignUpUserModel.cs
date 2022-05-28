using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="Enter the first name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please enter your mail")]
        [Display(Name ="Email address")]
        [EmailAddress(ErrorMessage ="please enter a valis mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [Compare("ConfirmPassword",ErrorMessage ="password does not match")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
