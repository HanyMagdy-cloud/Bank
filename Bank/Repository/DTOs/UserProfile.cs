using AutoMapper;
using Bank.Repository.Entities;
using System.Data;
namespace Bank.Repository.DTOs
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            // Map between DataRow and User class 
            CreateMap<IDataRecord, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["Name"]))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src["Email"]))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src["PasswordHash"]))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src["Role"]));


            
        }
    }
}
