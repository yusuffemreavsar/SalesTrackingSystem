using Application.Features.SalesDetails.Commands.Create;
using Application.Features.SalesDetails.Commands.Delete;
using Application.Features.SalesDetails.Commands.Update;
using Application.Features.SalesDetails.Queries.GetById;
using Application.Features.SalesDetails.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.SalesDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SalesDetail, CreateSalesDetailCommand>().ReverseMap();
        CreateMap<SalesDetail, CreatedSalesDetailResponse>().ReverseMap();
        CreateMap<SalesDetail, UpdateSalesDetailCommand>().ReverseMap();
        CreateMap<SalesDetail, UpdatedSalesDetailResponse>().ReverseMap();
        CreateMap<SalesDetail, DeleteSalesDetailCommand>().ReverseMap();
        CreateMap<SalesDetail, DeletedSalesDetailResponse>().ReverseMap();
        CreateMap<SalesDetail, GetByIdSalesDetailResponse>().ReverseMap();
        CreateMap<SalesDetail, GetListSalesDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<SalesDetail>, GetListResponse<GetListSalesDetailListItemDto>>().ReverseMap();
    }
}