using Application.Features.SalesDetails.Commands.Create;
using Application.Features.SalesDetails.Commands.Delete;
using Application.Features.SalesDetails.Commands.Update;
using Application.Features.SalesDetails.Queries.GetById;
using Application.Features.SalesDetails.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesDetailsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSalesDetailCommand createSalesDetailCommand)
    {
        CreatedSalesDetailResponse response = await Mediator.Send(createSalesDetailCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSalesDetailCommand updateSalesDetailCommand)
    {
        UpdatedSalesDetailResponse response = await Mediator.Send(updateSalesDetailCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSalesDetailResponse response = await Mediator.Send(new DeleteSalesDetailCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSalesDetailResponse response = await Mediator.Send(new GetByIdSalesDetailQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSalesDetailQuery getListSalesDetailQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSalesDetailListItemDto> response = await Mediator.Send(getListSalesDetailQuery);
        return Ok(response);
    }
}