﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Web.Extensions
{
    public class BadRequestBrokenRulesActionResult : IHttpActionResult
    {
        private IEnumerable<ValidationRule> BrokenRules { get; set; }
        private HttpRequestMessage Request { get; set; }
        private string ClassName { get; set; }

        public BadRequestBrokenRulesActionResult(HttpRequestMessage request, IEnumerable<ValidationRule> brokenRules, string className)
        {
            Request = request;
            BrokenRules = brokenRules;
            ClassName = className;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (BrokenRules == null)
            {
                return null;
            }
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var validationRules = BrokenRules as IList<ValidationRule> ?? BrokenRules.ToList();
            var errors = new
            {
                Error = "The provided " + ClassName + " failed to validate.",
                ValidationErrors = validationRules.ToList()
            };
            response.Content = new StringContent(System.Web.Helpers.Json.Encode(errors));
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}