// ShortStuff.Web
// NotificationController.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Net;
using System.Web.Http;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IHttpActionResult Get()
        {
            var result = _notificationService.GetAll();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }
            return HandleErrorActionResult(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = _notificationService.GetById(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }
            return HandleErrorActionResult(result);
        }

        public IHttpActionResult Post(Notification data)
        {
            var result = _notificationService.Create(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("Notification", result.ActionStatus.Id);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType()
                                                                                                     .Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }
            return HandleErrorActionResult(result);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Notification data)
        {
            data.Id = id;
            var result = _notificationService.Update(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created ? CreateHttpActionResult("Notification", result.ActionStatus.Id) : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType()
                                                                                                     .Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }
            return HandleErrorActionResult(result);
        }

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
