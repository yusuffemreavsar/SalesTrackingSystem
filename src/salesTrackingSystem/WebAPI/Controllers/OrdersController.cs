using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.GetById;
using Application.Features.Orders.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderCommand createOrderCommand)
    {
        CreatedOrderResponse response = await Mediator.Send(createOrderCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderCommand updateOrderCommand)
    {
        UpdatedOrderResponse response = await Mediator.Send(updateOrderCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedOrderResponse response = await Mediator.Send(new DeleteOrderCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOrderResponse response = await Mediator.Send(new GetByIdOrderQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOrderQuery getListOrderQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListOrderListItemDto> response = await Mediator.Send(getListOrderQuery);
        return Ok(response);
    }
}