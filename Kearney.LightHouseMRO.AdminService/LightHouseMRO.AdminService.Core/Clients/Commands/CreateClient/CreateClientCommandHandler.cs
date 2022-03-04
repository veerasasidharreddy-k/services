// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Domain.Dtos.Client;
using LightHouseMRO.AdminService.Domain.Entities;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;

namespace LightHouseMRO.AdminService.Core.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientResource>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResource> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clientToCreate = new Client
            {
                Name = request.Dto.Name,
                ModifiedAt = DateTime.UtcNow,
                ModifiedBy = "vreddy02"
            };

            var insertedClientId = await _unitOfWork.ClientRepository.CreateClientAsync(clientToCreate, cancellationToken);
            var insertedClient = await _unitOfWork.ClientRepository.GetClientAsync(insertedClientId, cancellationToken);
            _unitOfWork.Commit();

            return new ClientResource(new ClientDto { Name = insertedClient.Name });
        }

    }
}
