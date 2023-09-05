using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("registerAccount")]
        public async Task<ActionResult<Result<SettlementAccountRegistrationResponseDto>>> RegisterAsync() 
        {
            return Ok(await _accountRepository.RegisterAsync());
        }

        [HttpGet]
        [Route("getSettlementAccount")]
        public async Task<ActionResult<Result<SettlementAccountRegistrationResponseDto>>> GetAsync() 
        {
            return Ok(await _accountRepository.GetAccountAsync());
        }
    }
}
