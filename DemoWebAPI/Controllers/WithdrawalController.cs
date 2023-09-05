using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IWithdrawalRepository _withdrawalRepository;

        public WithdrawalController(IWithdrawalRepository withdrawalRepository)
        {
            _withdrawalRepository = withdrawalRepository;
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<ActionResult<Result<WithdrawalResponseDto>>> WithdrawalAsync(WithdrawalRequestDto model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            return Ok(await _withdrawalRepository.WithdrawalAsync(model));
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<ActionResult<Result<DepositResponseDto>>> DepositAsync(DepositRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            return Ok(await _withdrawalRepository.DepositAsync(model));
        }
    }
}
