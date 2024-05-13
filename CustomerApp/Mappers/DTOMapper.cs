using AutoMapper;
using CustomerApp.DTOs.Input;
using CustomerApp.DTOs.Output;
using CustomerApp.Entities;

namespace CustomerApp.Mappers;

public class DTOMappers : Profile
{
    public DTOMappers()
    {
        CreateMap<requestCreateCustomerDTO, responseCreateCustomerDTO>();
        CreateMap<requestCreateCustomerDTO, Customer>();
    }
}