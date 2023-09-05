using DemoWebAPI.DataLayer.ApplicationDatabaseContext;
using DemoWebAPI.DataLayer.IRepository;
using DemoWebAPI.DbSets;
using DemoWebAPI.DTOs.Generic;
using DemoWebAPI.DTOs.RequestDto;
using DemoWebAPI.DTOs.ResponseDto;
using DemoWebAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.DataLayer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> DeleteCustomerAsync(string customerId)
        {
            Result<bool> response = new();

            try
            {
                var customer = await _context.Customers
                    .Where(x => x.Id == customerId)
                    .FirstOrDefaultAsync();

                if (customer is null)
                {
                    response.Error = new Error()
                    { 
                        ErrorCode = 400, 
                        Type = "Bad Request" 
                    };
                    
                    response.Message = "Failed. Cannot delete because the customer does not exist.";
                    return response;
                }

                var delete = _context.Customers.Remove(customer);

                await _context.SaveChangesAsync();

                response.Data = true;
                response.IsSuccess = true;
                response.Message = "Customer record deleted.";
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

        public async Task<Result<CustomerRegistrationResponseDto>> GetCustomerByIdAsync(string customerId)
        {
            Result<CustomerRegistrationResponseDto> response = new();

            try
            {
                Customer customer = await _context.Customers
                    .Where(x => x.Id == customerId)
                    //.AsNoTracking()
                    .FirstOrDefaultAsync();

                if (customer is null)
                {
                    response.IsSuccess = true;
                    response.Message = "No record found";
                    return response;
                }

                var bankAccount = await _context.BankAccounts
                    .Where(x => x.CustomerId == customerId)
                    .FirstOrDefaultAsync();

                GetBankAccountResponseDto bankAccountRes = new() 
                {
                    Id = bankAccount.Id,
                    AccountBalance = bankAccount.AccountBalance,
                    AccountNumber = bankAccount.AccountNumber
                };

                CustomerRegistrationResponseDto customerResponse = new()
                {
                    Id = customer.Id,
                    DateOfRegistration = customer.DateOfRegistration,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,
                    BankAccount = bankAccountRes
                };

                response.Data = customerResponse;
                response.IsSuccess = true;
                response.Message = $"Customer with id of {customerId} found";
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

        public async Task<Results<CustomerRegistrationResponseDto>> GetAllCustomersAsync()
        {
            Results<CustomerRegistrationResponseDto> response = new();

            try
            {
                List<Customer> customers = await _context.Customers.ToListAsync();

                List<CustomerRegistrationResponseDto> responseList = new List<CustomerRegistrationResponseDto>();

                if (customers.Any())
                {
                    CustomerRegistrationResponseDto customerResponse = new();

                    foreach (var customer in customers)
                    {
                        var bankAccount = await _context.BankAccounts
                            .Where(x => x.CustomerId == customer.Id)
                                .FirstOrDefaultAsync();

                        GetBankAccountResponseDto bankAccountRes = new()
                        {
                            Id = bankAccount.Id,
                            AccountBalance = bankAccount.AccountBalance,
                            AccountNumber = bankAccount.AccountNumber
                        };

                        customerResponse.Id = customer.Id;
                        customerResponse.DateOfRegistration = customer.DateOfRegistration;
                        customerResponse.FirstName = customer.FirstName;
                        customerResponse.LastName = customer.LastName;
                        customerResponse.Address = customer.Address;
                        customerResponse.PhoneNumber = customer.PhoneNumber;
                        customerResponse.BankAccount = bankAccountRes;
                       
                        responseList.Add(customerResponse);
                    }

                    response.Data = responseList;
                    response.IsSuccess = true;
                    response.Message = "Customer records found";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "No record found";
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

        public async Task<Result<CustomerRegistrationResponseDto>> RegisterCustomerAsync(CustomerRegistrationRequestDto model)
        {
            Result<CustomerRegistrationResponseDto> response = new();

            try
            {
                CustomerRegistrationResponseDto result = new();
                GetBankAccountResponseDto bankAccountResult = new();

                //Check if the user does not exist
                var existingUser = await _context.Customers
                    .Where(x => x.EmailAddress == model.EmailAddress)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (existingUser is not null)
                {
                    response.Error = new Error()
                    {
                        Type = "Bad Request",
                        ErrorCode = 400
                    };

                    response.IsSuccess = false;
                    response.Data = result;
                    response.Message = "Email address is already used.";

                    Console.WriteLine($"Response : {JsonConvert.SerializeObject(response)}");
                    return response;
                }

                Customer newCustomer = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.EmailAddress,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    //AccountNumber = RandomGenerator.GenerateAccountNumber(5)
                };

                var addCustomer = await _context.Customers.AddAsync(newCustomer);

                await _context.SaveChangesAsync();

                //Register an account for the customer

                BankAccount bankAccount = new()
                {
                    AccountNumber = RandomGenerator.GenerateAccountNumber(5),
                    DateOpened = DateTime.UtcNow.ToShortDateString(),
                    AccountBalance = 0,

                    CustomerId = addCustomer.Entity.Id,
                    Customer = addCustomer.Entity
                };

                var addBankAccount = await _context.BankAccounts.AddAsync(bankAccount);

                await _context.SaveChangesAsync();

                result.Id = addCustomer.Entity.Id;
                result.FirstName = addCustomer.Entity.FirstName;
                result.LastName = addCustomer.Entity.LastName;
                result.PhoneNumber = addCustomer.Entity.PhoneNumber;
                result.Address = addCustomer.Entity.Address;
                result.DateOfRegistration = addCustomer.Entity.DateOfRegistration;

                bankAccountResult.Id = addBankAccount.Entity.Id;
                bankAccountResult.AccountNumber = addBankAccount.Entity.AccountNumber;
                bankAccountResult.AccountBalance = addBankAccount.Entity.AccountBalance;

                result.BankAccount = bankAccountResult;

                response.Data = result;
                response.IsSuccess = true;
                response.Message = "User registration successful.";
                Console.WriteLine($"Response : {JsonConvert.SerializeObject(response)}");
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

        public async Task<Result<bool>> UpdateCustomerAsync(string customerId, UpdateCustomerRequestDto model)
        {
            Result<bool> response = new(); 

            try
            {
                var customer = await _context.Customers
                    .Where(x => x.Id == customerId)
                    .FirstOrDefaultAsync();

                if (customer is null)
                {
                    response.Error = new Error() 
                    { 
                        ErrorCode = 400, 
                        Type = "Bad Request" 
                    };

                    response.Message = "Failed. Cannot update because the customer does not exist.";
                    return response;
                }

                customer.Address = model.Address;
                customer.PhoneNumber = model.PhoneNumber;
                customer.DateUpdated = DateTime.UtcNow.ToShortDateString();

                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Customer record updated successfully";
                response.IsSuccess = true;
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
