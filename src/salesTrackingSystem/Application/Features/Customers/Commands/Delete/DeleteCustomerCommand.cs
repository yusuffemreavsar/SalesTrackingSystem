using Application.Features.Customers.Constants;
using Application.Features.Customers.Constants;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<DeletedCustomerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CustomersOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCustomers"];

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public DeleteCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository,
                                         CustomerBusinessRules customerBusinessRules)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<DeletedCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _customerBusinessRules.CustomerShouldExistWhenSelected(customer);

            await _customerRepository.DeleteAsync(customer!);

            DeletedCustomerResponse response = _mapper.Map<DeletedCustomerResponse>(customer);
            return response;
        }
    }
}