using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.IRepository
{
    public interface IWithdrawalRepository
    {
        Task<Result<WithdrawalResponseDto>> WithdrawalAsync(WithdrawalRequestDto model);

        Task<Result<DepositResponseDto>> DepositAsync(DepositRequestDto model);
    }
}
