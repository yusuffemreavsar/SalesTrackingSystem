using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderDetailRepository : EfRepositoryBase<OrderDetail, Guid, BaseDbContext>, IOrderDetailRepository
{
    public OrderDetailRepository(BaseDbContext context) : base(context)
    {
    }
}