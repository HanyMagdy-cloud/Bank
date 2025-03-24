using Bank.Repository.DTOs;
using Bank.Repository.Entities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bank.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to administrators only.
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService; // Dependency for admin-related services.

        // Constructor for dependency injection of the admin service.
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        // Add a new customer with a bank account


        [HttpPost("AddCustomer")]
        public IActionResult AddCustomerWithAccount([FromQuery] CustomerWithAccountDto customerDto)
        {

            
            {
                var result = _adminService.AddCustomerWithAccount(customerDto);

                if (!result)
                    return Ok("Customer added successfully.");

                return BadRequest("Failed to add customer.");
            }
            
        }


        // Add a loan for a customer
        [HttpPost("AddLoan")]
        public IActionResult AddLoan([FromQuery] LoanDto loanDto)
        {
            try
            {
                if (loanDto == null)
                {
                    return BadRequest("Invalid loan details.");
                }

                loanDto.LoanDate = DateTime.Now; // Set the loan date

                var result = _adminService.AddLoanForCustomer(loanDto);

                if (!result)
                {
                    return Ok("Loan added successfully.");
                }
                return BadRequest("Failed to add loan.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
