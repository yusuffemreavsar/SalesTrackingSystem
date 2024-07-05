using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SalesDetailRepository : EfRepositoryBase<SalesDetail, Guid, BaseDbContext>, ISalesDetailRepository
{
    public SalesDetailRepository(BaseDbContext context) : base(context)
    {
    }
}