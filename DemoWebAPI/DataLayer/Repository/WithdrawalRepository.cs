using DemoWebAPI.DataLayer.ApplicationDatabaseContext;
using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DbSets;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.Repository
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        private readonly AppDbContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public WithdrawalRepository(AppDbContext context, 
            ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Result<DepositResponseDto>> DepositAsync(DepositRequestDto model)
        {
            Result<DepositResponseDto> response = new();

            try
            {
                DepositResponseDto result = new();

                //get the customer account record
                BankAccount customerAccount = await _context.BankAccounts
                    .Where(x => x.AccountNumber == model.AccountNumber)
                    .FirstOrDefaultAsync();

                if (customerAccount is null)
                {
                    response.IsSuccess = true;
                    response.Message = "Account does not exist.";
                    return response;
                }

                //get the settlement account
                SettlementAccount accountDetails = await GetSettlementAccoount();

                if (accountDetails is null)
                {
                    response.IsSuccess = true;
                    response.Message = "No settlement record found";
                    return response;
                }

                //credit the customer
                customerAccount.AccountBalance += model.Amount;
                await _context.SaveChangesAsync();

                //debit the bank settlement with the same amount   
                accountDetails.AccountBalance -= model.Amount;
                await _context.SaveChangesAsync();

                result.NewBalance = customerAccount.AccountBalance;
                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Deposit successful";
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

        public async Task<Result<WithdrawalResponseDto>> WithdrawalAsync(WithdrawalRequestDto model)
        {
            Result<WithdrawalResponseDto> response = new();

            try
            {
                WithdrawalResponseDto result = new();

                //get the customer record
                BankAccount customerAccount = await _context.BankAccounts
                    .Where(x => x.AccountNumber == model.AccountNumber)
                    .FirstOrDefaultAsync();

                if (customerAccount is null)
                {
                    response.IsSuccess = true;
                    response.Message = "Account does not exist.";
                    return response;
                }

                //get the settlement account
                SettlementAccount accountDetails = await GetSettlementAccoount();

                if (accountDetails is null)
                {
                    response.IsSuccess = true;
                    response.Message = "No settlement record found";
                    return response;
                }

                //check if the customer has sufficient balance
                if (customerAccount.AccountBalance < model.Amount )
                {
                    response.Error = new Error()
                    {
                        ErrorCode = 404,
                        Type = "Bad Request"
                    };

                    response.Message = "Insufficient balance";
                    return response;
                }

                //debit the customer
                customerAccount.AccountBalance -= model.Amount;

                await _context.SaveChangesAsync();

                //credit the bank settlement with the same amount   
                accountDetails.AccountBalance += model.Amount;
                await _context.SaveChangesAsync();

                result.NewBalance = customerAccount.AccountBalance;
                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Withdrawal successful";
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

        private async Task<Customer> GetCustomerById(string customerId) 
        {
            return await _context.Customers
                    .Where(x => x.Id == customerId)
                    .FirstOrDefaultAsync();
        }

        private async Task<SettlementAccount> GetSettlementAccoount()
        {
            return await _context.SettlementAccounts
                    .Where(x => x.AccountNumber == "STA20230728")
                    .FirstOrDefaultAsync();
        }
    }
}
