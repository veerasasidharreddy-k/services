// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightHouseMRO.AdminService.Infrastructure.Extensions
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Extension to contain all of our business layer dependencies for our external server providers (ASP.NET Core in our case). 
        /// </summary>
        /// <param name="services">Service collection for dependency injection</param>
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration )
        {

            services.AddTransient<IUnitOfWork>(w => new UnitOfWork(configuration.GetSection("AdminService").GetSection("DBConnectionString").Value));


            return services;
        }
    }
}
