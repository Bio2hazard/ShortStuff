// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicAsyncController.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Asynchronous Web Api 2 access point for Topic-related interaction. Contains RESTful access to CRUD and other required queries.
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
    ///     The Asynchronous Web Api 2 access point for Topic-related interaction. Contains RESTful access to CRUD and other required queries.
    /// </summary>
    public class TopicAsyncController : BaseController
    {
        /// <summary>
        ///     The topic service provides the controller with all topic-related API functionality.
        /// </summary>
        private readonly ITopicService _topicService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicAsyncController"/> class.
        /// </summary>
        /// <param name="topicService">
        /// The topic service ( gets injected through Ninject ).
        /// </param>
        public TopicAsyncController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        /// <summary>
        ///     Asks the service to asynchronously retrieve all topics.
        /// </summary>
        /// <returns>
        ///     HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get()
        {
            var result = await _topicService.GetAllAsync();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously retrieve a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _topicService.GetByIdAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously create a new topic, using the supplied information.
        /// </summary>
        /// <param name="data">
        /// The supplied POST-data used to create a new topic.
        /// </param>
        /// <returns>
        /// HTTP Status Code 201 - Created + Unique link to newly created topic,
        ///     HTTP Status Code 400 - Bad Request if no ID was returned, or the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 409 - Conflict if a topic with a provided unique value already exists,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Post(Topic data)
        {
            var result = await _topicService.CreateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("TopicAsync", result.ActionStatus.Id);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously update a existing topic - identified uniquely through their id - using the supplied information. If no topic exists, a new one will be
        ///     created.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <param name="data">
        /// The supplied POST-data used to update a existing topic or create a new topic.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if topic was successfully updated,
        ///     HTTP Status Code 201 - Created + Unique link to newly created topic if no topic was found, and instead created,
        ///     HTTP Status Code 400 - Bad Request if the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Topic data)
        {
            data.Id = id;
            var result = await _topicService.UpdateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created
                               ? CreateHttpActionResult("TopicAsync", result.ActionStatus.Id)
                               : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to asynchronously delete the topic uniquely identified through Id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if topic was successfully deleted, or did not exist,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _topicService.DeleteAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return HandleErrorActionResult(result);
        }
    }
}