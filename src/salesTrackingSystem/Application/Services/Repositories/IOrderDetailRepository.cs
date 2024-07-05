using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOrderDetailRepository : IAsyncRepository<OrderDetail, Guid>, IRepository<OrderDetail, Guid>
{
}