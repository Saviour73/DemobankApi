namespace DemoWebAPI.DTOs.ResponseDto
{
    public class CustomerRegistrationResponseDto
    {
        public string Id { get; set; } 

        public string DateOfRegistration { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public GetBankAccountResponseDto BankAccount { get; set; }
    }
}
