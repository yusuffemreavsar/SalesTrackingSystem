using Application.Features.SalesDetails.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SalesDetails;

public class SalesDetailManager : ISalesDetailService
{
    private readonly ISalesDetailRepository _salesDetailRepository;
    private readonly SalesDetailBusinessRules _salesDetailBusinessRules;

    public SalesDetailManager(ISalesDetailRepository salesDetailRepository, SalesDetailBusinessRules salesDetailBusinessRules)
    {
        _salesDetailRepository = salesDetailRepository;
        _salesDetailBusinessRules = salesDetailBusinessRules;
    }

    public async Task<SalesDetail?> GetAsync(
        Expression<Func<SalesDetail, bool>> predicate,
        Func<IQueryable<SalesDetail>, IIncludableQueryable<SalesDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SalesDetail? salesDetail = await _salesDetailRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return salesDetail;
    }

    public async Task<IPaginate<SalesDetail>?> GetListAsync(
        Expression<Func<SalesDetail, bool>>? predicate = null,
        Func<IQueryable<SalesDetail>, IOrderedQueryable<SalesDetail>>? orderBy = null,
        Func<IQueryable<SalesDetail>, IIncludableQueryable<SalesDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SalesDetail> salesDetailList = await _salesDetailRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return salesDetailList;
    }

    public async Task<SalesDetail> AddAsync(SalesDetail salesDetail)
    {
        SalesDetail addedSalesDetail = await _salesDetailRepository.AddAsync(salesDetail);

        return addedSalesDetail;
    }

    public async Task<SalesDetail> UpdateAsync(SalesDetail salesDetail)
    {
        SalesDetail updatedSalesDetail = await _salesDetailRepository.UpdateAsync(salesDetail);

        return updatedSalesDetail;
    }

    public async Task<SalesDetail> DeleteAsync(SalesDetail salesDetail, bool permanent = false)
    {
        SalesDetail deletedSalesDetail = await _salesDetailRepository.DeleteAsync(salesDetail);

        return deletedSalesDetail;
    }
}
