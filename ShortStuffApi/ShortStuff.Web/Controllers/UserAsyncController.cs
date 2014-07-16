// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAsyncController.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Asynchronous Web Api 2 access point for User-related interaction. Contains RESTful access to CRUD and other required queries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;
    using ShortStuff.Domain.Services;
    using ShortStuff.Web.Extensions;

    /// <summary>
    ///     The Asynchronous Web Api 2 access point for User-related interaction. Contains RESTful access to CRUD and other required queries.
    /// </summary>
    public class UserAsyncController : BaseController
    {
        /// <summary>
        ///     The user service provides the controller with all user-related API functionality.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAsyncController"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service ( gets injected through Ninject ).
        /// </param>
        public UserAsyncController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Asks the service to asynchronously retrieve all users.
        /// </summary>
        /// <returns>
        ///     HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get()
        {
            var result = await _userService.GetAllAsync();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously retrieve a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get(decimal id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously create a new user, using the supplied information.
        /// </summary>
        /// <param name="data">
        /// The supplied POST-data used to create a new user.
        /// </param>
        /// <returns>
        /// HTTP Status Code 201 - Created + Unique link to newly created user,
        ///     HTTP Status Code 400 - Bad Request if no ID was returned, or the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 409 - Conflict if a user with a provided unique value already exists,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Post(User data)
        {
            var result = await _userService.CreateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("UserAsync", result.ActionStatus.Id);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously update a existing user - identified uniquely through their id - using the supplied information. If no user exists, a new one will be
        ///     created.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <param name="data">
        /// The supplied POST-data used to update a existing user or create a new user.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if user was successfully updated,
        ///     HTTP Status Code 201 - Created + Unique link to newly created user if no user was found, and instead created,
        ///     HTTP Status Code 400 - Bad Request if the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(decimal id, User data)
        {
            data.Id = id;
            var result = await _userService.UpdateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created
                               ? CreateHttpActionResult("UserAsync", result.ActionStatus.Id)
                               : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously delete the user uniquely identified through Id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if user was successfully deleted, or did not exist,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Delete(decimal id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return HandleErrorActionResult(result);
        }

        // Service Methods

        /// <summary>
        /// Asks the service to asynchronously retrieve a single user through their tag property.
        /// </summary>
        /// <param name="tag">
        /// The tag to search for.
        /// </param>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        [HttpGet]
        [Route("api/userasync/bytag/{tag}")]
        public async Task<IHttpActionResult> GetUserByTag(string tag)
        {
            var result = await _userService.GetByTagAsync(tag);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }

            return HandleErrorActionResult(result);
        }

        ///// <summary>
        ///// Returns the users Messages, Echoes
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="includeReplies"></param>
        ///// <returns></returns>
        // [HttpGet]
        // [Route("api/userasync/{id:decimal}/feed")]
        // public async Task<IHttpActionResult> GetUserFeed(decimal id, [FromUri] bool includeReplies = false)
        // {
        // return Ok();
        // }
    }
}