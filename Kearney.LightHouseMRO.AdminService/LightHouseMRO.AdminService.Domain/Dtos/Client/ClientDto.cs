// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.
//Our DTOs act as containers to transport that persisted data between layers
//(e.g. the domain layer and the data layer, and from the data layer to the API layer in the long run)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Domain.Dtos.Client
{
    public class ClientDto
    {
        public string Name { get; set; }
    }
}
