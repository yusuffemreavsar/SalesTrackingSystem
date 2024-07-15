using Application.Features.Customers.Constants;
using Application.Features.Products.Constants;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public UserBusinessRules(IUserRepository userRepository, ILocalizationService localizationService, ICustomerRepository customerRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UsersMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(UsersMessages.UserDontExists);
    }

    public async Task UserIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await _userRepository.AnyAsync(predicate: u => u.Id == id);
        if (doesExist)
            await throwBusinessException(UsersMessages.UserDontExists);
    }

    public async Task CustomerIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await _customerRepository.AnyAsync(predicate: u => u.Id == id);
        if (doesExist)
            await throwBusinessException(CustomersBusinessMessages.CustomerNotExists);
    }

    public async Task UserPasswordShouldBeMatched(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            await throwBusinessException(UsersMessages.PasswordDontMatch);
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(UsersMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(Guid id, string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
        if (doesExists)
            await throwBusinessException(UsersMessages.UserMailAlreadyExists);
    }

    public async Task userCustomerIntegrity(UpdateUserCommand user,Guid id)
    {
        var customer= await _customerRepository.GetAsync(p=>p.UserId== id);
        customer.FirstName = user.FirstName;
        customer.LastName = user.LastName;
        customer.Email = user.Email;    
        customer.PhoneNumber=customer.PhoneNumber;
        await _customerRepository.UpdateAsync(customer);

    }


}
