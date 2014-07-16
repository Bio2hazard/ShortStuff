// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Web Api 2 access point for Notification-related interaction. Contains RESTful access to CRUD and other required queries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Web.Controllers
{
    using System.Net;
    using System.Web.Http;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;
    using ShortStuff.Domain.Services;
    using ShortStuff.Web.Extensions;

    /// <summary>
    ///     The Web Api 2 access point for Notification-related interaction. Contains RESTful access to CRUD and other required queries.
    /// </summary>
    public class NotificationController : BaseController
    {
        /// <summary>
        ///     The notification service provides the controller with all notification-related API functionality.
        /// </summary>
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        /// <param name="notificationService">
        /// The notification service ( gets injected through Ninject ).
        /// </param>
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        ///     Asks the service to retrieve all notifications.
        /// </summary>
        /// <returns>
        ///     HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public IHttpActionResult Get()
        {
            var result = _notificationService.GetAll();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to retrieve a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 200 - OK + JSON encoded data payload on success,
        ///     HTTP Status Code 404 - Not Found if no data was retrieved,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public IHttpActionResult Get(int id)
        {
            var result = _notificationService.GetById(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to create a new notification, using the supplied information.
        /// </summary>
        /// <param name="data">
        /// The supplied POST-data used to create a new notification.
        /// </param>
        /// <returns>
        /// HTTP Status Code 201 - Created + Unique link to newly created notification,
        ///     HTTP Status Code 400 - Bad Request if no ID was returned, or the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 409 - Conflict if a notification with a provided unique value already exists,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public IHttpActionResult Post(Notification data)
        {
            var result = _notificationService.Create(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("Notification", result.ActionStatus.Id);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to update a existing notification - identified uniquely through their id - using the supplied information. If no notification exists, a new one will be
        ///     created.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <param name="data">
        /// The supplied POST-data used to update a existing notification or create a new notification.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if notification was successfully updated,
        ///     HTTP Status Code 201 - Created + Unique link to newly created notification if no notification was found, and instead created,
        ///     HTTP Status Code 400 - Bad Request if the supplied POST-data failed validation. Also contains information on failed validation cases.
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Notification data)
        {
            data.Id = id;
            var result = _notificationService.Update(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created
                               ? CreateHttpActionResult("Notification", result.ActionStatus.Id)
                               : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType().Name);
            }

            return HandleErrorActionResult(result);
        }

        /// <summary>
        /// Asks the service to delete the notification uniquely identified through Id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// HTTP Status Code 204 - No Content if notification was successfully deleted, or did not exist,
        ///     HTTP Status Code 500 - Internal Server Error if the other codes don't apply. Contains exception on DEBUG.
        /// </returns>
        public IHttpActionResult Delete(int id)
        {
            var result = _notificationService.Delete(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return HandleErrorActionResult(result);
        }
    }
}