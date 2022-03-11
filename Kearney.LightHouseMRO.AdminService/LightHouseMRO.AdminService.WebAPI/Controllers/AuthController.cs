using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Users.Queries;
using LightHouseMRO.AdminService.Domain.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LightHouseMRO.AdminService.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "AzureAd")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IConfiguration _configuration;

        public AuthController(IConfiguration config, IMediator mediator)
        {
            _configuration = config;
            _mediator = mediator;
        }
        // GET: api/<ClientController>
        [HttpGet]
        [Route("accesstoken")]
        public async Task<ActionResult<UserAuthDto>> Get(string email)
        {
            UserAuthDto token = (await _mediator.Send(new GetUserAuthToken(email))).Self;
            return Ok(token);
        }
    }
}