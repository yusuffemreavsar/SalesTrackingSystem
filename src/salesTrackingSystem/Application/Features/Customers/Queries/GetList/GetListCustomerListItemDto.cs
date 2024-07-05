using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Customers.Queries.GetList;

public class GetListCustomerListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}