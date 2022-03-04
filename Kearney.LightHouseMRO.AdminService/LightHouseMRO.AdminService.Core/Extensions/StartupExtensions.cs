// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Core.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LightHouseMRO.AdminService.Core.Extensions
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Extension to contain all of our business layer dependencies for our external server providers (ASP.NET Core in our case). 
        /// </summary>
        /// <param name="services">Service collection for dependency injection</param>
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
        {
            // Add our MediatR and FluentValidation dependencies
            services.AddMediatR(Assembly.GetExecutingAssembly());


            // Add our MediatR validation pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
