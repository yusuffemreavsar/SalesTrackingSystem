using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISalesDetailRepository : IAsyncRepository<SalesDetail, Guid>, IRepository<SalesDetail, Guid>
{
}