using Application.Features.Customers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;

namespace Application.Features.Customers.Queries.GetList;

public class GetListCustomerQuery : IRequest<GetListResponse<GetListCustomerListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListCustomers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetCustomers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCustomerQueryHandler : IRequestHandler<GetListCustomerQuery, GetListResponse<GetListCustomerListItemDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetListCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCustomerListItemDto>> Handle(GetListCustomerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Customer> customers = await _customerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCustomerListItemDto> response = _mapper.Map<GetListResponse<GetListCustomerListItemDto>>(customers);
            return response;
        }
    }
}