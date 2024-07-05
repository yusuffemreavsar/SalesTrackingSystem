using Application.Features.Customers.Constants;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;

namespace Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerQuery : IRequest<GetByIdCustomerResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, GetByIdCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public GetByIdCustomerQueryHandler(IMapper mapper, ICustomerRepository customerRepository, CustomerBusinessRules customerBusinessRules)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<GetByIdCustomerResponse> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _customerBusinessRules.CustomerShouldExistWhenSelected(customer);

            GetByIdCustomerResponse response = _mapper.Map<GetByIdCustomerResponse>(customer);
            return response;
        }
    }
}