// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BadRequestBrokenRulesActionResult.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Creates a custom 400 - Bad Request HTTP Status Code for validation failures.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Helpers;
    using System.Web.Http;

    using ShortStuff.Domain.Entities;

    /// <summary>
    ///     Creates a custom 400 - Bad Request HTTP Status Code for validation failures.
    /// </summary>
    public class BadRequestBrokenRulesActionResult : IHttpActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestBrokenRulesActionResult"/> class.
        /// </summary>
        /// <param name="request">
        /// The HTTP request.
        /// </param>
        /// <param name="brokenRules">
        /// A IEnumerable of broken Validation Rules.
        /// </param>
        /// <param name="className">
        /// The name of the Entity Type that failed validation.
        /// </param>
        public BadRequestBrokenRulesActionResult(HttpRequestMessage request, IEnumerable<ValidationRule> brokenRules, string className)
        {
            Request = request;
            BrokenRules = brokenRules;
            ClassName = className;
        }

        /// <summary>
        ///     Gets or sets the broken rules.
        /// </summary>
        private IEnumerable<ValidationRule> BrokenRules { get; set; }

        /// <summary>
        ///     Gets or sets the request.
        /// </summary>
        private HttpRequestMessage Request { get; set; }

        /// <summary>
        ///     Gets or sets the class name.
        /// </summary>
        private string ClassName { get; set; }

        /// <summary>
        /// The method that puts the supplied data together into a HTTP Response Message.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The custom 400 - Bad Request HTTP Response.
        /// </returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (BrokenRules == null)
            {
                return null;
            }

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var validationRules = BrokenRules as IList<ValidationRule> ?? BrokenRules.ToList();
            var errors = new { Error = "The provided " + ClassName + " failed to validate.", ValidationErrors = validationRules.ToList() };
            response.Content = new StringContent(Json.Encode(errors));
            response.RequestMessage = Request;
            return await Task.FromResult(response);
        }
    }
}