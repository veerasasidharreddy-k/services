using System;
using LightHouseMRO.AdminService.Domain.Entities;

namespace LightHouseMRO.AdminService.Domain.Dtos.User
{
    public class UserAuthDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<Role> roles { get; set; }
        public string Token { get; set; }
    }
}

