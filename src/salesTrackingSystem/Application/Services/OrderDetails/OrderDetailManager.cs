using Application.Features.OrderDetails.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.OrderDetails;

public class OrderDetailManager : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

    public OrderDetailManager(IOrderDetailRepository orderDetailRepository, OrderDetailBusinessRules orderDetailBusinessRules)
    {
        _orderDetailRepository = orderDetailRepository;
        _orderDetailBusinessRules = orderDetailBusinessRules;
    }

    public async Task<OrderDetail?> GetAsync(
        Expression<Func<OrderDetail, bool>> predicate,
        Func<IQueryable<OrderDetail>, IIncludableQueryable<OrderDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        OrderDetail? orderDetail = await _orderDetailRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return orderDetail;
    }

    public async Task<IPaginate<OrderDetail>?> GetListAsync(
        Expression<Func<OrderDetail, bool>>? predicate = null,
        Func<IQueryable<OrderDetail>, IOrderedQueryable<OrderDetail>>? orderBy = null,
        Func<IQueryable<OrderDetail>, IIncludableQueryable<OrderDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<OrderDetail> orderDetailList = await _orderDetailRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return orderDetailList;
    }

    public async Task<OrderDetail> AddAsync(OrderDetail orderDetail)
    {
        OrderDetail addedOrderDetail = await _orderDetailRepository.AddAsync(orderDetail);

        return addedOrderDetail;
    }

    public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
    {
        OrderDetail updatedOrderDetail = await _orderDetailRepository.UpdateAsync(orderDetail);

        return updatedOrderDetail;
    }

    public async Task<OrderDetail> DeleteAsync(OrderDetail orderDetail, bool permanent = false)
    {
        OrderDetail deletedOrderDetail = await _orderDetailRepository.DeleteAsync(orderDetail);

        return deletedOrderDetail;
    }
}
