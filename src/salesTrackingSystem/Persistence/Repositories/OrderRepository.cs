using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderRepository : EfRepositoryBase<Order, Guid, BaseDbContext>, IOrderRepository
{
    public OrderRepository(BaseDbContext context) : base(context)
    {
    }
}