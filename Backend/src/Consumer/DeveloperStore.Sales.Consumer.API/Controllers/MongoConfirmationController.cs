using DeveloperStore.Sales.Consumer.API.DTOs;
using DeveloperStore.Sales.Consumer.Application.Handler;
using DeveloperStore.Sales.Consumer.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.Sales.Consumer.API.Controllers
{
    [ApiController]
    [Route("api/confirmation")]
    public class MongoConfirmationController : ControllerBase
    {
        private readonly MongoConfirmationByIdHandler _handler;

        public MongoConfirmationController(MongoConfirmationByIdHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<ActionResult<MongoMessage?>> GetById([FromBody] IdRequest request)
        {
            var guidId = Guid.Parse(request.Id);
            var id = request.Id;            
            var result = await _handler.HandleAsync(guidId);
            return Ok(result);
        }
    }
}
