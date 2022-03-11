using System;
using LightHouseMRO.AdminService.Domain.Models;

namespace LightHouseMRO.AdminService.Core.Data
{
    public interface IUserRepository
    {

        Task<User> GetUserRolesAsync(string email, CancellationToken cancellationToken);
    }
}

