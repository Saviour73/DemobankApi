namespace DemoWebAPI.DTOs.ResponseDto
{
    public class GetBankAccountResponseDto
    {
        public string Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
