using DemoWebAPI.DataLayer.ApplicationDatabaseContext;
using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DbSets;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.ResponseDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SettlementAccountRegistrationResponseDto>> GetAccountAsync()
        {
            Result<SettlementAccountRegistrationResponseDto> response = new();

            try
            {
                SettlementAccount accountDetails = await _context.SettlementAccounts
                    .Where(x => x.AccountNumber == "STA20230728")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (accountDetails is not null)
                {
                    SettlementAccountRegistrationResponseDto result = new()
                    {
                        Id = accountDetails.Id,
                        AccountNumber = accountDetails.AccountNumber,
                        DateOpened = accountDetails.DateOpened,
                        AccountBalance = accountDetails.AccountBalance
                    };

                    response.Data = result;
                    response.IsSuccess = true;
                    response.Message = "Settlement account found.";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Settlement account not found.";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                response.Error = new Error()
                {
                    Type = "Internal Server Error",
                    ErrorCode = 500
                };

                response.Message = "Something went wrong. Please try again later.";

                Console.WriteLine($"Response : {JsonConvert.SerializeObject(response)}");
                return response;
            }
        }

        public async Task<Result<SettlementAccountRegistrationResponseDto>> RegisterAsync()
        {
            Result<SettlementAccountRegistrationResponseDto> response = new();

            try
            {
                SettlementAccount settlementAccount = new() 
                {
                    AccountNumber = "STA20230728",
                    DateOpened = DateTime.UtcNow.ToShortDateString(),
                    AccountBalance = 0
                };

                var registerAccount = await _context.SettlementAccounts.AddAsync(settlementAccount);

                await _context.SaveChangesAsync();

                SettlementAccountRegistrationResponseDto result = new() 
                {
                    Id = registerAccount.Entity.Id,
                    AccountBalance = registerAccount.Entity.AccountBalance,
                    AccountNumber = registerAccount.Entity.AccountNumber,
                    DateOpened = registerAccount.Entity.DateOpened
                };

                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Account opened successfully.";

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                response.Error = new Error()
                {
                    Type = "Internal Server Error",
                    ErrorCode = 500
                };

                response.Message = "Something went wrong. Please try again later.";

                Console.WriteLine($"Response : {JsonConvert.SerializeObject(response)}");
                return response;
            }
        }
    }
}
