using System;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;
namespace LightHouseMRO.AdminService.Core.Users.Queries
{
    public class GetUserAuthToken : IRequest<UserAuthResource>
    {
        public string Email;
        public GetUserAuthToken(string email)
        {
            Email = email;
        }
    }
}

