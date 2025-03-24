using AutoMapper;
using System.Transactions;

namespace Bank.Repository.DTOs
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile()
        {
            // Map from Transaction to TransactionDto
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }

    }
}
