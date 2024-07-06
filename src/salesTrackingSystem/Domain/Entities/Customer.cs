using NArchitecture.Core.Persistence.Repositories;


namespace Domain.Entities;
public class Customer : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<Sale> Sales { get; set; }
    public ICollection<Order> Orders { get; set; }
}
