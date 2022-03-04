// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LightHouseMRO.AdminService.Core.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(c => c.Dto).NotNull().WithMessage("A Request cannot have empty client creation data");
            RuleFor(b => b.Dto.Name).NotEmpty();
        }
    }
}
