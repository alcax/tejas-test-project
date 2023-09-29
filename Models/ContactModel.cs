using SampleProject.Common;
using System.ComponentModel.DataAnnotations;
namespace SampleProject.Models
{
    public class  AddContactModel
    {
        [Required(ErrorMessage ="Please accept the terms and conditions.")]
        public string Agreement { get; set; }
        public long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [MaxLength(64)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        //public const string PhoneNumberRegex = @"[0-9]{3}[-\s\.]?[0-9]{6,8}$";
        //public const string PhoneNumberRegexMessage = "Invalid phone number";
        [Phone]
        [RegularExpression(@"^\+\d{1,4}\d{6,}$", ErrorMessage = "Invalid phone number. Enter Phone number with country code like +91 ......")]
        public string PhoneNumber { get; set; }

        [Required]
        public string  Reason { get; set; }

        [Required(ErrorMessage ="Long text is required.")]
        public string Body { get; set; }
        public AddContactModel() { }
    }


    public class ContactListingModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Reason { get; set; }
        public string Body { get; set; }
    }

    public class ContactDetailsModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Reason { get; set; }
        public string Body { get; set; }
    }
}
