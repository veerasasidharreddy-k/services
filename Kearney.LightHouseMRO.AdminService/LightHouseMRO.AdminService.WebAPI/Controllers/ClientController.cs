// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using LightHouseMRO.AdminService.Core.Clients.Queries.GetClients;
using LightHouseMRO.AdminService.Domain.Dtos.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LightHouseMRO.AdminService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ClientController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
        {
            var clients = await _mediator.Send(new GetClientsQuery());
            return Ok(clients.Items);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
