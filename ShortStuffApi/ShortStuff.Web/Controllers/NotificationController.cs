using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class NotificationController : BaseController
    {
        private INotificationService _notificationService;

        public NotificationController(IUnitOfWork unitOfWork, INotificationService notificationService) : base(unitOfWork)
        {
            _notificationService = notificationService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.NotificationRepository.GetAll());
            }
            catch (Exception ex)
            {
#if DEBUG
                return InternalServerError(ex);
#else
                return InternalServerError();
#endif
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.NotificationRepository.GetById(id));
            }
            catch (Exception ex)
            {
#if DEBUG
                return InternalServerError(ex);
#else
                return InternalServerError();
#endif
            }
        }

        public IHttpActionResult Post(Notification data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }

            var status = UnitOfWork.NotificationRepository.Create(data);

            if (status.Status == CreateStatusEnum.Conflict)
                return Conflict();

            return CreateHttpActionResult("Notification", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Notification data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }
            data.Id = id;

            var status = UnitOfWork.NotificationRepository.Update(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            UnitOfWork.NotificationRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
