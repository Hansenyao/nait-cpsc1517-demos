using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindLibrary.Entities
{
    //This is just an Example Class to show more Data Annotation - Not part of the Database.
    public class PersonExample
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must provide a {0}.")]
        [MaxLength(50, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must provide a {0}.")]
        [MaxLength(50, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You must provide a {0}.")]
        //Validation for Phone numbers, Regex validation provided by Microsoft 
        //This is not the best, but useable
        [Phone(ErrorMessage = "Please enter a valid {0}.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must provide a {0}.")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be {1} or greater.")]
        public int Age { get; set; }

        [Display(Name = "SIN")]
        [Required(ErrorMessage = "You must provide a {0}.")]
        [RegularExpression("^\\d{3}-\\d{3}-\\d{3}$",
            ErrorMessage = "{0} must be in the format ###-###-###")]
        public string SocialInsuranceNumber { get; set; }

        [Required(ErrorMessage = "You must provide a {0}.")]
        [EmailAddress]
        public string Email { get; set; }

        //This is for confirmation of the email entered by the user, so much match the Email
        [Display(Name = "Confirm Email")]
        [Required(ErrorMessage = "You must provide a {0}.")]
        [EmailAddress]
        //Compare the value of one property to another
        //Takes in the property being compared and can provide an ErrorMessage
        [Compare(nameof(Email), ErrorMessage = "{1} and {0} must match.")]
        public string ConfirmEmail { get; set; }

        //Business Rule: Birthday cannot be in the future
        //Use a Custom Validator
        [CustomValidation(typeof(NotFutureValidation), nameof(NotFutureValidation.Validate))]
        public DateOnly Birthday { get; set; }
    }

    //Outside of the PersonExample Class
    public class NotFutureValidation
    {
        //provide a static method that can be used to return the validation results.
        //The Validate Method must have a paramter that is the same datatype
        //as the Property (Birthdate) that we want to validate
        public static ValidationResult? Validate(DateOnly date)
        {
            return date > DateOnly.FromDateTime(DateTime.Today) ? new ValidationResult("The date cannot be in the future.") : ValidationResult.Success;
        }
    }
}
