// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LightHouseMRO.AdminService.Core.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(request => request.Dto)
                .NotNull()
                .WithMessage("Must supply a request body");

            RuleFor(request => request.ClientId)
                .NotNull()
                .WithMessage("Must supply a valid client ID");
        }
    }
}
