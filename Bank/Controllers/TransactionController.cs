using Bank.Repository.DTOs;
using Bank.Repository.Entities.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")] // Ensure only customers can access this controller
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        // Constructor with dependency injection for the TransactionService.
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("MakeTransfer")]
        public IActionResult MakeTransfer([FromQuery] TransactionDto transactionDto)
        {
            try
            {
                // Validate that the transfer amount is greater than zero.

                if (transactionDto.Amount <= 0)
                {
                    return BadRequest("Transfer amount must be greater than zero.");
                }

                // Call the service method to execute the transfer.

                var result = _transactionService.MakeTransfer(transactionDto);

                if (result)
                {
                    return Ok("Transfer completed successfully.");
                }
                else
                {
                    return BadRequest("Transfer failed. Please check your account balance or details.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Endpoint to fetch all transactions for a specific customer.

        [HttpGet("GetTransactions/{customerId}")]
        public IActionResult GetTransactionsByCustomerId(int customerId)
        {
            // Call the service method to fetch transactions for the given customer ID.

            var transactions = _transactionService.GetTransactionsByCustomerId(customerId);

            // If no transactions are found, return a 404 status.

            if (transactions == null || !transactions.Any())
            {
                return NotFound($"Customer with Id {customerId} has no transactions.");
            }

            return Ok(transactions);
           
        }
    }
}

