// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Core.Exceptions
{
    public class AdminServiceApiException : Exception
    {
        public AdminServiceApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
            ApiErrors = new List<AdminServiceApiError>();
        }

        public HttpStatusCode StatusCode { get; }

        public ICollection<AdminServiceApiError> ApiErrors { get; }
    }
}
