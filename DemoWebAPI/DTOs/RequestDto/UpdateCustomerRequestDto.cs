using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.DTOs.RequestDto
{
    public class UpdateCustomerRequestDto
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
