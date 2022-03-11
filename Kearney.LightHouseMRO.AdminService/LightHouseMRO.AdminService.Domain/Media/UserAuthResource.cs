using System;
using LightHouseMRO.AdminService.Domain.Dtos.User;

namespace LightHouseMRO.AdminService.Domain.Media
{
    public class UserAuthResource : Resource<UserAuthDto>
    {
        public UserAuthResource(UserAuthDto resource)
            : base(resource)
        {
        }
    }
}

