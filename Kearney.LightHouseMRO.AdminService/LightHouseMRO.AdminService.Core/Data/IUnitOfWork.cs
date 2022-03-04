// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }
        void Commit();
    }
}
