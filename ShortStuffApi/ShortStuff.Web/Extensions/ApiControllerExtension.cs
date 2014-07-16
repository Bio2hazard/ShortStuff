// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiControllerExtension.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Extends the default Web Api 2 controller with some helper methods to provide customized HTTP Status Code responses.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web.Extensions
{
    using System.Collections.Generic;
    using System.Web.Http;

    using ShortStuff.Domain.Entities;

    /// <summary>
    ///     Extends the default Web Api 2 controller with some helper methods to provide customized HTTP Status Code responses.
    /// </summary>
    public static class ApiControllerExtension
    {
        /// <summary>
        /// Creates, initializes and returns a new instance of the <see cref="BadRequestBrokenRulesActionResult"/> class.
        /// </summary>
        /// <param name="controller">
        /// The Api Controller to grab the response from.
        /// </param>
        /// <param name="brokenRules">
        /// A IEnumerable of broken Validation Rules.
        /// </param>
        /// <param name="className">
        /// The name of the Entity Type that failed validation.
        /// </param>
        /// <returns>
        /// A custom 400 - Bad Request Http response, for validation failures.
        /// </returns>
        public static BadRequestBrokenRulesActionResult BadRequest(ApiController controller, IEnumerable<ValidationRule> brokenRules, string className)
        {
            return new BadRequestBrokenRulesActionResult(controller.Request, brokenRules, className);
        }
    }
}