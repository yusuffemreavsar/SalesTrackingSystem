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

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string[] Roles => [Admin, Write, CustomersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCustomers"];

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public UpdateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository,
                                         CustomerBusinessRules customerBusinessRules)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<UpdatedCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _customerBusinessRules.CustomerShouldExistWhenSelected(customer);
            customer = _mapper.Map(request, customer);

            await _customerRepository.UpdateAsync(customer!);

            UpdatedCustomerResponse response = _mapper.Map<UpdatedCustomerResponse>(customer);
            return response;
        }
    }
}