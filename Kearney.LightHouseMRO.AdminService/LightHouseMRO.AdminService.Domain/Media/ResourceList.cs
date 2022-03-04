// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Domain.Media
{
    public class ResourceList<T>
    {
        public ResourceList(IEnumerable<T> items) => Items = items;

        public IEnumerable<T> Items { get; }

        public int Count => Items.Count();
    }
}
