// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Core.Exceptions;
using LightHouseMRO.AdminService.Domain.Dtos.Client;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;

namespace LightHouseMRO.AdminService.Core.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientResource>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateClientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResource> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var clientToUpdate = await _unitOfWork.ClientRepository.GetClientAsync(request.ClientId, cancellationToken);
            if(clientToUpdate == null)
            {
                throw new AdminServiceApiException($"No client was found with ID {request.ClientId}", HttpStatusCode.NotFound);
            }
            clientToUpdate!.Name = request.Dto.Name;
            clientToUpdate!.ModifiedAt = DateTime.UtcNow;
            clientToUpdate!.ModifiedBy = "vreddy02";

            await _unitOfWork.ClientRepository.UpdateClientAsync(clientToUpdate, cancellationToken);

            var uppdatedClient = await _unitOfWork.ClientRepository.GetClientAsync(clientToUpdate.Id, cancellationToken);
            _unitOfWork.Commit();

            return new ClientResource(new ClientDto { Name = uppdatedClient.Name });

        }
    }
}
