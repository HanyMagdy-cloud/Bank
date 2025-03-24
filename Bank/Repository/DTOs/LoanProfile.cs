
using AutoMapper;
using Bank.Repository.Entities;
namespace Bank.Repository.DTOs
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            // Map Loan to LoanDto
            CreateMap<Loan, LoanDto>();

            // Map LoanDto to Loan (if needed)
            CreateMap<LoanDto, Loan>();
        }


    }
}
