using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.DTOs.RequestDto
{
    public class CustomerRegistrationRequestDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
