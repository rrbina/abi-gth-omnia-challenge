using DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.DeleteCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.SelectCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCustomerByIdCommand(id);
            var customer = await _mediator.Send(query);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersCommand());
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var eventId  = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCustomerCommand(id);
            var eventId = await _mediator.Send(command);
            return Ok(new { eventId });
        }
    }
}