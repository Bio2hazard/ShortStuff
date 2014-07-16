// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The base controller provides a number of methods to create appropriate HTTP response codes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Routing;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    ///     The base controller provides a number of methods to create appropriate HTTP response codes.
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// This method is used for calls that retrieve data from a service. It checks if data exists, and returns either 200 - OK or 404 - Not Found.
        /// </summary>
        /// <param name="entity">
        /// The data returned by the service.
        /// </param>
        /// <typeparam name="TEntity">
        /// The type of the supplied data, used to compare it to it's default value.
        /// </typeparam>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload if data exists,
        ///     HTTP Status Code 404 - Not Found if no data exists.
        /// </returns>
        protected IHttpActionResult GetHttpActionResult<TEntity>(TEntity entity)
        {
            if (!Equals(entity, default(TEntity)))
            {
                return Ok(entity);
            }

            return NotFound();
        }

        /// <summary>
        /// This method is used for calls that persist new entities. It checks if a unique identifying id was supplied, and generates a 204 - Created + valid link.
        /// </summary>
        /// <param name="controllerName">
        /// The name of the web controller that called this method. Must be supplied as a string, used to create the unique URI pointing to the newly created entity.
        /// </param>
        /// <param name="createdId">
        /// The unique id of the newly created entity.
        /// </param>
        /// <typeparam name="TId">
        /// The type of the id used to uniquely identify the entity.
        /// </typeparam>
        /// <returns>
        /// HTTP Status Code 201 - Created + Unique link to newly created entity,
        ///     HTTP Status Code 400 - Bad Request if createdId matches it's default value.
        /// </returns>
        protected IHttpActionResult CreateHttpActionResult<TId>(string controllerName, TId createdId)
        {
            if (!Equals(createdId, default(TId)))
            {
                var urlHelper = new UrlHelper(Request);
                var link = urlHelper.Link("DefaultApi", new { controller = controllerName, id = createdId });

                return Created(link, new { id = createdId, url = link });
            }

            return BadRequest();
        }

        /// <summary>
        /// This method is a default failure state in case no other method was able to handle the result of a service request. This occurs for example if a exception was triggered.
        /// </summary>
        /// <param name="result">
        /// The ActionResult as it was provided by the service.
        /// </param>
        /// <typeparam name="TEntity">
        /// The type of the data attached to the ActionResult, must inherit ValidatableBase to provide validation.
        /// </typeparam>
        /// <typeparam name="TId">
        /// The type of the unique identifier attached to the ActionResult.
        /// </typeparam>
        /// <returns>
        /// HTTP Status Code 500 - Internal Server Error. On debug: contains exception if one was thrown.
        /// </returns>
        protected IHttpActionResult HandleErrorActionResult<TEntity, TId>(ActionResult<TEntity, TId> result) where TEntity : ValidatableBase
        {
#if DEBUG
            if (result.ActionStatus.Status == ActionStatusEnum.ExceptionError)
            {
                return InternalServerError(result.ActionException);
            }

#endif
            return InternalServerError();
        }
    }
}