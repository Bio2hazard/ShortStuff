using System.Collections.Generic;
using System.Web.Http;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Web.Extensions
{
    public static class ApiControllerExtension
    {
        public static BadRequestBrokenRulesActionResult BadRequest(ApiController controller, IEnumerable<ValidationRule> brokenRules, string className)
        {
            return new BadRequestBrokenRulesActionResult(controller.Request, brokenRules, className);
        }
    }
}