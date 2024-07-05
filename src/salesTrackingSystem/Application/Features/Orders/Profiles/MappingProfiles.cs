using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Orders.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Order, CreateOrderCommand>().ReverseMap();
        CreateMap<Order, CreatedOrderResponse>().ReverseMap();
        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<Order, UpdatedOrderResponse>().ReverseMap();
        CreateMap<Order, DeleteOrderCommand>().ReverseMap();
        CreateMap<Order, DeletedOrderResponse>().ReverseMap();
        CreateMap<Order, GetByIdOrderResponse>().ReverseMap();
        CreateMap<Order, GetListOrderListItemDto>().ReverseMap();
        CreateMap<IPaginate<Order>, GetListResponse<GetListOrderListItemDto>>().ReverseMap();
    }
}