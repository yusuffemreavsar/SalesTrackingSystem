using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Order:Entity<Guid>
{
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public OrderDetail OrderDetail { get; set; }

}
