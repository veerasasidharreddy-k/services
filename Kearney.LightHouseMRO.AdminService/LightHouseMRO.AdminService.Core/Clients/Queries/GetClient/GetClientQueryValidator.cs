// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LightHouseMRO.AdminService.Core.Clients.Queries.GetClient
{
    public class GetClientQueryValidator : AbstractValidator<GetClientQuery>
    {
        public GetClientQueryValidator()
        {
            RuleFor(c => c.ClientId).NotNull()
                .NotEmpty()
                .WithMessage("Must supply an ID to retrieve a client")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Must be a valid client ID");
        }
    }
}
