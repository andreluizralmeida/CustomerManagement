namespace CustomerManagement.Domain.AutoMapperProfiles;

using AutoMapper;
using CustomerManagement.Api.Extensions;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Features.Customers;
using CustomerManagement.Domain.Models;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<UpdateCustomerCommand, CustomerModel>().ReverseMap();
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();

        CreateMap<SearchCustomer, GetCustomerFilterQuery>()
            .ForMember(dest => dest.Offset, m => m.MapFrom(a => a.Page > 0 ? a.Page - 1 : 0))
            .ForMember(dest => dest.Limit, m => m.MapFrom(a => a.Size > 0 ? a.Size : 1));            
            
        CreateMap<CustomerModel, CreateCustomerCommand>().ReverseMap();
        CreateMap<CustomerModel, Customer>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.CustomerId));
        CreateMap<Customer, CustomerModel>()
            .ForMember(dest => dest.CustomerId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Password, m => m.Ignore());

    }
}