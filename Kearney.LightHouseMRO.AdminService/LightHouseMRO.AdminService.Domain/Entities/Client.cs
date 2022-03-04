// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.
//Our entities represent the source of record stored within our database,
//that when extracted, are expected to modify and persist their state

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Domain.Entities
{
    public class Client : TimeStampedEntity
    {
        public string Name { get; set; }

    }
}
