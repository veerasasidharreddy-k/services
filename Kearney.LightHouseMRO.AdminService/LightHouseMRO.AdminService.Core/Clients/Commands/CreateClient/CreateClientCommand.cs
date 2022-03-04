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

namespace LightHouseMRO.AdminService.Core.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<ClientResource>
    {
        public CreateClientCommand(CreateClientDto dto)
        {
            Dto = dto;
        }

        public CreateClientDto Dto { get; }

    }
}
