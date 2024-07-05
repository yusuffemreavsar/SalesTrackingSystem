using Application.Features.OrderDetails.Commands.Create;
using Application.Features.OrderDetails.Commands.Delete;
using Application.Features.OrderDetails.Commands.Update;
using Application.Features.OrderDetails.Queries.GetById;
using Application.Features.OrderDetails.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderDetailCommand createOrderDetailCommand)
    {
        CreatedOrderDetailResponse response = await Mediator.Send(createOrderDetailCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDetailCommand updateOrderDetailCommand)
    {
        UpdatedOrderDetailResponse response = await Mediator.Send(updateOrderDetailCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedOrderDetailResponse response = await Mediator.Send(new DeleteOrderDetailCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOrderDetailResponse response = await Mediator.Send(new GetByIdOrderDetailQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOrderDetailQuery getListOrderDetailQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListOrderDetailListItemDto> response = await Mediator.Send(getListOrderDetailQuery);
        return Ok(response);
    }
}