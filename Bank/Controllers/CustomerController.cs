using Bank.Repository.DTOs;
using Bank.Repository.Entities;
using Bank.Repository.Entities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Authorize(Roles = "Customer")] // Only users with "Customer" role can access this controller
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        //private readonly CreateBankAccountDto createBankAccountDto;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet("GetCustomerAccounts/{customerId:int}")]
        public IActionResult GetCustomerAccounts(int customerId)
        {
            try
            {
                var accounts = _customerService.GetCustomerAccounts(customerId);

                if (!accounts.Any())
                {
                    return NotFound("Customer has no accounts in the bank.");
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpGet("Loans/{customerId:int}")]
        public IActionResult GetCustomerLoans(int customerId)
        {


            // Get the loans for the specified customer
            var loans = _customerService.GetCustomerLoans(customerId);

            if (!loans.Any())
            {
                return NotFound($"Customer with ID {customerId} has no loans in the bank.");
            }

            return Ok(loans); // Return the list of loans as the response
        }

        [HttpPost("CreateBankAccount")]
        public IActionResult CreateBankAccount([FromBody] CreateBankAccountDto createBankAccountDto)
        {


            var generatedAccountNumber = _customerService.CreateNewBankAccount(
                createBankAccountDto.CustomerId,
                createBankAccountDto.AccountType
            );

            return Ok(new { AccountNumber = generatedAccountNumber });


        }



        [HttpPost("Deposit")]
        public IActionResult DepositToAccount([FromQuery] string accountNumber, [FromQuery] decimal depositAmount)
        {
            if (depositAmount <= 0)
                return BadRequest("The deposit amount must be greater than zero.");

            try
            {
                var result = _customerService.DepositToAccount(accountNumber, depositAmount);

                if (result == null)
                    return NotFound($"Account with number {accountNumber} not found.");

                return Ok(new
                {
                    Message = "Deposit successful",
                    DepositedAmount = depositAmount,
                    AccountNumber = accountNumber
                });
            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(int customerId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                return BadRequest("Old and new passwords are required.");
            }

            // Call the service to change the password
            var result = _customerService.ChangeCustomerPassword(customerId, oldPassword, newPassword);

            if (!result)
            {
                return Ok("Password changed successfully.");
            }
            else
            {
                return Unauthorized("Invalid Customer ID or Old Password.");
            }
        }



    }
}
