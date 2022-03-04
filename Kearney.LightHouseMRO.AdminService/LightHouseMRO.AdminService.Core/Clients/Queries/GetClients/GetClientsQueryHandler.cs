// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Domain.Dtos.Client;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;

namespace LightHouseMRO.AdminService.Core.Clients.Queries.GetClients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ClientResourceList>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResourceList> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _unitOfWork.ClientRepository.GetAllClientsAsync(cancellationToken);
            _unitOfWork.Commit();

            var mappedClients = clients.Select(client => new ClientDto { Name = client.Name });

            return new ClientResourceList(mappedClients);
        }
    }
}
