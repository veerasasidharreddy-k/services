// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Domain.Media;
using MediatR;

namespace LightHouseMRO.AdminService.Core.Clients.Queries.GetClients
{
    public class GetClientsQuery : IRequest<ClientResourceList>
    {
    }
}
