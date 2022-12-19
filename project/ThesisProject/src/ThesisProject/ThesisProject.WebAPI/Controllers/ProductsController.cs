using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThesisProject.Application.UseCases.Products.Commands.AddProductStock;
using ThesisProject.Application.UseCases.Products.Commands.CreateProduct;
using ThesisProject.Application.UseCases.Products.Commands.DeleteProduct;
using ThesisProject.Application.UseCases.Products.Commands.UpdateProduct;
using ThesisProject.Application.UseCases.Products.Queries.GetProductById;

namespace ThesisProject.WebAPI.Controllers;

[Route("products")]
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var response = await _mediator.Send(request);

        return CreatedAtRoute("GetProductById", new { ProductId = response.CreatedId }, null);
    }

    [HttpPut]
    [Route("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductRequest request)
    {
        if (request.ProductId != productId)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }

    [HttpPut]
    [Route("{productId:guid}/stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProductStock(Guid productId, AddProductStockRequest request)
    {
        if (request.ProjectId != productId)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }

    [HttpGet]
    [Route("{productId:guid}", Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var request = new GetProductByIdRequest
        {
            ProductId = productId
        };

        var result = await _mediator.Send(request);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete]
    [Route("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var request = new DeleteProductRequest
        {
            ProductId = productId
        };

        await _mediator.Send(request);

        return NoContent();
    }
}
