// ShortStuff.Web
// NotificationAsyncController.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class NotificationAsyncController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationAsyncController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return GetHttpActionResult(await _notificationService.GetAllAsync());
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

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                return GetHttpActionResult(await _notificationService.GetByIdAsync(id));
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

        public async Task<IHttpActionResult> Post(Notification data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }

            var status = await _notificationService.CreateAsync(data);

            if (status.Status == CreateStatusEnum.Conflict)
            {
                return Conflict();
            }

            return CreateHttpActionResult("NotificationAsync", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Notification data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }
            data.Id = id;

            var status = await _notificationService.UpdateAsync(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return await Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            await _notificationService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
