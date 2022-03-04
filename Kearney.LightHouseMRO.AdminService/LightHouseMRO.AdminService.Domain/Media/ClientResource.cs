// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Domain.Dtos.Client;

namespace LightHouseMRO.AdminService.Domain.Media
{
    public class ClientResource : Resource<ClientDto>
    {
        public ClientResource(ClientDto resource)
            : base(resource)
        {
        }
    }
}
