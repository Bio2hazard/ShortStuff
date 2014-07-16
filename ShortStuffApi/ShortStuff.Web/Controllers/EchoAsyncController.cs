// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EchoAsyncController.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Asynchronous Web Api 2 access point for Echo-related interaction. Contains RESTful access to CRUD and other required queries.
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
    ///     The Asynchronous Web Api 2 access point for Echo-related interaction. Contains RESTful access to CRUD and other required queries.
    /// </summary>
    public class EchoAsyncController : BaseController
    {
        /// <summary>
        ///     The echo service provides the controller with all echo-related API functionality.
        /// </summary>
        private readonly IEchoService _echoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoAsyncController"/> class.
        /// </summary>
        /// <param name="echoService">
        /// The echo service ( gets injected through Ninject ).
        /// </param>
        public EchoAsyncController(IEchoService echoService)
        {
            _echoService = echoService;
        }

        /// <summary>
        ///     Asks the service to asynchronously retrieve all echos.
        /// </summary>
        /// <returns>
        ///     HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get()
        {
            var result = await _echoService.GetAllAsync();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously retrieve a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _echoService.GetByIdAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously create a new echo, using the supplied information.
        /// </summary>
        /// <param name="data">
        /// The supplied POST-data used to create a new echo.
        /// </param>
        /// <returns>
        /// HTTP Status Code 201 - Created + Unique link to newly created echo,
        ///     HTTP Status Code 400 - Bad Request if no ID was returned, or the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 409 - Conflict if a echo with a provided unique value already exists,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Post(Echo data)
        {
            var result = await _echoService.CreateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("EchoAsync", result.ActionStatus.Id);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously update a existing echo - identified uniquely through their id - using the supplied information. If no echo exists, a new one will be
        ///     created.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <param name="data">
        /// The supplied POST-data used to update a existing echo or create a new echo.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if echo was successfully updated,
        ///     HTTP Status Code 201 - Created + Unique link to newly created echo if no echo was found, and instead created,
        ///     HTTP Status Code 400 - Bad Request if the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Echo data)
        {
            data.Id = id;
            var result = await _echoService.UpdateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created
                               ? CreateHttpActionResult("EchoAsync", result.ActionStatus.Id)
                               : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously delete the echo uniquely identified through Id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if echo was successfully deleted, or did not exist,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _echoService.DeleteAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return HandleErrorActionResult(result);
        }
    }
}