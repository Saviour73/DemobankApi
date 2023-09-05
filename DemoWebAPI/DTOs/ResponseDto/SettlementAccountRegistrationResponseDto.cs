using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.DTOs.ResponseDto
{
    public class SettlementAccountRegistrationResponseDto
    {
        public string Id { get; set; } 

        public string AccountNumber { get; set; }

        public string DateOpened { get; set; }

        public decimal AccountBalance { get; set; } 
    }
}
