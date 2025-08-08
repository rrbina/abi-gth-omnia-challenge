using DeveloperStore.Sales.Application.Sales.Commands.CreateProduct;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct;
using DeveloperStore.Sales.Application.Sales.Commands.SelectProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeveloperStore.Sales.Application.Sales.Commands.DeleteProduct;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdCommand(id);
            var product = await _mediator.Send(query);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsCommand());
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }
    }
}