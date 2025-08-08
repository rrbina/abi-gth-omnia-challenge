using DeveloperStore.Sales.Application.Sales.Commands.CancelItem;
using DeveloperStore.Sales.Application.Sales.Commands.CancelSale;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.Sales.Commands.DeleteSale;
using DeveloperStore.Sales.Application.Sales.Commands.SelectSale;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateSale;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetSaleByIdCommand(id);
            var sale = await _mediator.Send(query);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _mediator.Send(new GetAllSalesCommand());
            return Ok(sales);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
        {
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleCommand command)
        {
            if (id != command.SaleNumber)
                return BadRequest();

            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var eventId = await _mediator.Send(new DeleteSaleCommand(id));
            return Ok(new { eventId });
        }

        [HttpPatch("{saleId}/items/{itemId}/cancel")]
        public async Task<IActionResult> CancelItem(Guid saleId, Guid itemId)
        {
            var command = new CancelItemCommand(saleId, itemId);
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpPatch("{saleId}/cancel")]
        public async Task<IActionResult> CancelSale(Guid saleId)
        {
            var command = new CancelSaleCommand(saleId);
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

    }
}
