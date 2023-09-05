using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Result<CustomerRegistrationResponseDto>>> 
            RegisterAsync(CustomerRegistrationRequestDto model) 
        {
            if (!ModelState.IsValid) //false
            {
                return BadRequest("Invalid request payload.");
            }

            var result = await _customerRepository.RegisterCustomerAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("getAllCustomers")]
        public async Task<ActionResult<Results<CustomerRegistrationResponseDto>>> GetAllAsync() 
        {
            return Ok(await _customerRepository.GetAllCustomersAsync());
        }

        [HttpGet]
        [Route("getCustomerById/{customerId}")]
        public async Task<ActionResult<Results<CustomerRegistrationResponseDto>>> GetAsync(string customerId)
        {
            return Ok(await _customerRepository.GetCustomerByIdAsync(customerId));
        }

        [HttpDelete]
        [Route("deleteCustomer/{customerId}")]
        public async Task<ActionResult<Result<bool>>> DeleteAsync(string customerId)
        {
            return Ok(await _customerRepository.DeleteCustomerAsync(customerId));
        }

        [HttpPut]
        [Route("updateCustomer/{customerId}")]
        public async Task<ActionResult<Result<bool>>> UpdateAsync(string customerId, UpdateCustomerRequestDto model)
        {
            return Ok(await _customerRepository.UpdateCustomerAsync(customerId, model));
        }
    }
}
