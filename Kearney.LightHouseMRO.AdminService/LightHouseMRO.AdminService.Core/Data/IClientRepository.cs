using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Domain.Entities;

namespace LightHouseMRO.AdminService.Core.Data
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken);
        Task<int> CreateClientAsync(Client client, CancellationToken cancellationToken);
        Task<Client> GetClientAsync(int id, CancellationToken cancellationToken);
        Task UpdateClientAsync(Client client, CancellationToken cancellationToken);

    }
}
