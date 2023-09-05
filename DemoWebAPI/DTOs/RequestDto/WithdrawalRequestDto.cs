using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.DTOs.RequestDto
{
    public class WithdrawalRequestDto
    {
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
