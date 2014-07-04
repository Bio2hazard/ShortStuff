using System.Collections;
using System.Collections.Generic;
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
        private IEnumerable<ValidationRule> _brokenRules { get; set; }
        private HttpRequestMessage _request { get; set; }
        private string _className { get; set; }

        public BadRequestBrokenRulesActionResult(HttpRequestMessage request, IEnumerable<ValidationRule> brokenRules, string className)
        {
            _request = request;
            _brokenRules = brokenRules;
            _className = className;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_brokenRules == null)
            {
                return null;
            }
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var validationRules = _brokenRules as IList<ValidationRule> ?? _brokenRules.ToList();
            var errors = new
            {
                Error = "The provided " + _className + " failed to validate.",
                ValidationErrors = validationRules.ToList()
            };
            response.Content = new StringContent(System.Web.Helpers.Json.Encode(errors));
            response.RequestMessage = _request;
            return Task.FromResult(response);
        }
    }
}