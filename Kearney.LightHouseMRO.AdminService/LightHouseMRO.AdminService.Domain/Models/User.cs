using System;

namespace LightHouseMRO.AdminService.Domain.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<Entities.Role> roles { get; set; }
    }
}

