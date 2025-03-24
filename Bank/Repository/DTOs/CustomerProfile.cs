using AutoMapper;
namespace Bank.Repository.DTOs
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerWithAccountDto, CustomerProfile>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash)); // Mapping Password to PasswordHash
        }

        public object PasswordHash { get; private set; }
    }
}
