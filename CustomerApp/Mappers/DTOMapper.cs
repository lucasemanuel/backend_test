using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using Domain.Entities;

namespace CustomerApp.Mappers;

public class DTOMappers : Profile
{
    public DTOMappers()
    {
        CreateMap<RequestCreateCustomerDTO, ResponseCreateCustomerDTO>();
        CreateMap<Customer, ResponseCreateCustomerDTO>();
        CreateMap<Customer, LoginUserDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src._id.ToString()));
        CreateMap<BankTransaction, TransactionResponseDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src._id.ToString()))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.typeBankTransaction.ToString()));
        CreateMap<BankAccount, BankAccountDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src._id.ToString()));
        CreateMap<RequestCreateCustomerDTO, Customer>();
    }
}