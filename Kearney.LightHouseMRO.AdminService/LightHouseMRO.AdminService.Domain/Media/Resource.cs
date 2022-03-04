// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Domain.Media
{
    public class Resource<T>
    {
        public Resource(T resource) => Self = resource;

        public T Self { get; }

        public string ApiVersion => "v1";
    }
}
