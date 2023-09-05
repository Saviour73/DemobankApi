using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.IRepository
{
    public interface ICustomerRepository
    {
        Task<Result<CustomerRegistrationResponseDto>> RegisterCustomerAsync(CustomerRegistrationRequestDto model);

        Task<Results<CustomerRegistrationResponseDto>> GetAllCustomersAsync();

        Task<Result<CustomerRegistrationResponseDto>> GetCustomerByIdAsync(string customerId);

        Task<Result<bool>> DeleteCustomerAsync(string customerId);

        Task<Result<bool>> UpdateCustomerAsync(string customerId, UpdateCustomerRequestDto model);
    }
}
