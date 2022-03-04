// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Domain.Dtos.Client;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;

namespace LightHouseMRO.AdminService.Core.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<ClientResource>
    {
        public UpdateClientCommand(UpdateClientDto dto, int requestId)
        {
            Dto = dto;
            ClientId = requestId;
        }

        public UpdateClientDto Dto { get; }
        public int ClientId { get; }
    }
}
