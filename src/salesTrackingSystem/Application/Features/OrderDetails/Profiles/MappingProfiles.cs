using Application.Features.OrderDetails.Commands.Create;
using Application.Features.OrderDetails.Commands.Delete;
using Application.Features.OrderDetails.Commands.Update;
using Application.Features.OrderDetails.Queries.GetById;
using Application.Features.OrderDetails.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.OrderDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OrderDetail, CreateOrderDetailCommand>().ReverseMap();
        CreateMap<OrderDetail, CreatedOrderDetailResponse>().ReverseMap();
        CreateMap<OrderDetail, UpdateOrderDetailCommand>().ReverseMap();
        CreateMap<OrderDetail, UpdatedOrderDetailResponse>().ReverseMap();
        CreateMap<OrderDetail, DeleteOrderDetailCommand>().ReverseMap();
        CreateMap<OrderDetail, DeletedOrderDetailResponse>().ReverseMap();
        CreateMap<OrderDetail, GetByIdOrderDetailResponse>().ReverseMap();
        CreateMap<OrderDetail, GetListOrderDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<OrderDetail>, GetListResponse<GetListOrderDetailListItemDto>>().ReverseMap();
    }
}