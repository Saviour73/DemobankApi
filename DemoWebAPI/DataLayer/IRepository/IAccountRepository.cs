using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.ResponseDto;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.IRepository
{
    public interface IAccountRepository
    {
        Task<Result<SettlementAccountRegistrationResponseDto>> RegisterAsync();

        Task<Result<SettlementAccountRegistrationResponseDto>> GetAccountAsync();
    }
}
