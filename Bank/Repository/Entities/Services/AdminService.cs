using AutoMapper;
using Bank.Repository.DTOs;
using Bank.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace Bank.Repository.Entities.Services
{
    public class AdminService
    {
        //private readonly IAdminRepository _adminRepository;

        private readonly IBank _bank;

        private readonly IMapper _mapper;

        public AdminService( IBank bank, IMapper mapper)
        {
            //_adminRepository = adminRepository;
            _bank = bank;
            _mapper = mapper;
        }


        public bool AddCustomerWithAccount(CustomerWithAccountDto customerDto)
        {
            using var connection = _bank.GetConnection();

            // Map the DTO to an anonymous object using AutoMapper
            var parameters = _mapper.Map<object>(customerDto);

            var rowsAffected = connection.Execute(
                "AddCustomerWithAccount",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0; // Return true if the operation was successful
        }

        public bool AddLoanForCustomer(LoanDto loanDto)
        {
            using var connection = _bank.GetConnection();

            // Map LoanDto to an anonymous object using AutoMapper
            var parameters = _mapper.Map<object>(loanDto);

            var rowsAffected = connection.Execute(
                "AddLoanForCustomer",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return rowsAffected > 0; // Return true if the operation was successful
        }
    }
}
