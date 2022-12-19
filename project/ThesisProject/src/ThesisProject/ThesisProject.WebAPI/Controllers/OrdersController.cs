using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.Application.UseCases.Orders.Commands.AddOrderItem;
using ThesisProject.Application.UseCases.Orders.Commands.CreateOrder;
using ThesisProject.Application.UseCases.Orders.Commands.DeleteOrder;
using ThesisProject.Application.UseCases.Orders.Commands.PlaceOrder;
using ThesisProject.Application.UseCases.Orders.Queries.GetOrderById;

namespace ThesisProject.WebAPI.Controllers;

[Route("orders")]
public class OrdersController : ApiController
{
    public OrdersController(IMediator mediator) : base(mediator)
    {

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var response = await _mediator.Send(request);

        return CreatedAtRoute("GetOrderById", new { OrderId = response.CreatedId }, null);
    }

    [HttpPut]
    [Route("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrderItemToOrder(Guid orderId, AddOrderItemRequest request)
    {
        if (request.OrderId != orderId)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }

    [HttpPut]
    [Route("{orderId:guid}/place")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlaceOrder(Guid orderId, PlaceOrderRequest request)
    {
        if (request.OrderId != orderId)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }

    [HttpGet]
    [Route("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var request = new GetOrderByIdRequest
        {
            OrderId = orderId
        };

        var result = await _mediator.Send(request);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete]
    [Route("{orderId:guid}", Name = "GetOrderById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrder(Guid orderId, DeleteOrderRequest request)
    {
        if (request.OrderId != orderId)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }
}
