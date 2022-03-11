using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Core.Exceptions;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LightHouseMRO.AdminService.Core.Users.Queries
{
    public class GetUserAuthTokenHandler : IRequestHandler<GetUserAuthToken, UserAuthResource>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public GetUserAuthTokenHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<UserAuthResource> Handle(GetUserAuthToken request, CancellationToken cancellationToken)
        {
            //var user = await _unitOfWork.UserRepository.GetUserRolesAsync(request.Email, cancellationToken);
            var user = new Domain.Models.User()
            {
                Email = "sasidhar.karriv@live.com",
                Name = "Veera Sasidhar Reddy karri",
                roles = new List<Domain.Entities.Role>()
                {
                    new Domain.Entities.Role(){Id = 1, Name = Domain.Constants.Role.KearneyAdmin.ToString()},
                    new Domain.Entities.Role(){Id = 2, Name = Domain.Constants.Role.ClientUser.ToString()},
                }
            };
            if (user is null)
            {
                throw new AdminServiceApiException($"No user found with email {request.Email}", HttpStatusCode.Unauthorized);
            }

            var roleClaims = new List<Claim>();
            foreach (var role in user.roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(role)));
            }

            byte[] secret = Encoding.ASCII.GetBytes(_configuration["Jwt:secret"]);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:issuer"],
                Audience = _configuration["Jwt:audience"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                }
                .Union(roleClaims)),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:secret"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            _unitOfWork.Commit();
            return new UserAuthResource(new Domain.Dtos.User.UserAuthDto
            {
                Email = user.Email,
                Token = handler.WriteToken(token),
                Name = user.Name,
                roles = user.roles
            });
        }

    }
}

