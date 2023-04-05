using DurableFunctionsStudyCase.Functions.Misc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mime;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace DurableFunctionsStudyCase.Functions.Extensions
{
    public static class ValidatableRequestExtensions
    {
        /// <summary>
        /// Creates a <see cref="HttpResponseMessage"/> containing a collection
        /// of minimal validation error details.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HttpResponseMessage ToBadRequest<T>(this ValidatableRequest<T> request)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var errors = request.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            });

            response.Content = new StringContent(
                JsonConvert.SerializeObject(errors),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            return response;
        }
    }
}
