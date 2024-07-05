using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class SalesDetail:Entity<Guid>
{
    public Guid SaleId{ get; set; }
    public Sale Sale { get; set; }
    public Guid ProductSale { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
