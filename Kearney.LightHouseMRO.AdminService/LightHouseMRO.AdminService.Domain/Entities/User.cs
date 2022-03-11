using System;
namespace LightHouseMRO.AdminService.Domain.Entities
{
    public class User : TimeStampedEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsInternalUser { get; set; }
        public bool IsActive { get; set; }
    }
}

