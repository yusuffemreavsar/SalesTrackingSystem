using Application.Features.SalesDetails.Constants;
using Application.Features.SalesDetails.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SalesDetails.Constants.SalesDetailsOperationClaims;

namespace Application.Features.SalesDetails.Queries.GetById;

public class GetByIdSalesDetailQuery : IRequest<GetByIdSalesDetailResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSalesDetailQueryHandler : IRequestHandler<GetByIdSalesDetailQuery, GetByIdSalesDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly SalesDetailBusinessRules _salesDetailBusinessRules;

        public GetByIdSalesDetailQueryHandler(IMapper mapper, ISalesDetailRepository salesDetailRepository, SalesDetailBusinessRules salesDetailBusinessRules)
        {
            _mapper = mapper;
            _salesDetailRepository = salesDetailRepository;
            _salesDetailBusinessRules = salesDetailBusinessRules;
        }

        public async Task<GetByIdSalesDetailResponse> Handle(GetByIdSalesDetailQuery request, CancellationToken cancellationToken)
        {
            SalesDetail? salesDetail = await _salesDetailRepository.GetAsync(predicate: sd => sd.Id == request.Id, cancellationToken: cancellationToken);
            await _salesDetailBusinessRules.SalesDetailShouldExistWhenSelected(salesDetail);

            GetByIdSalesDetailResponse response = _mapper.Map<GetByIdSalesDetailResponse>(salesDetail);
            return response;
        }
    }
}