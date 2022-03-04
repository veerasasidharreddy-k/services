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

namespace LightHouseMRO.AdminService.Core.Clients.Queries.GetClient
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientResource>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResource> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.ClientRepository.GetClientAsync(request.ClientId, cancellationToken);

            if (client is null)
            {
                throw new AdminServiceApiException($"No client found with ID {request.ClientId}", HttpStatusCode.NotFound);
            }

            _unitOfWork.Commit();

            return new ClientResource(new ClientDto { Name = client.Name });

        }
    }
}
