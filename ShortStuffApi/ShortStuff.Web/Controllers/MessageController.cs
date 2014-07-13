// ShortStuff.Web
// MessageController.cs
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
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IHttpActionResult Get()
        {
            var result = _messageService.GetAll();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }
            return HandleErrorActionResult(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = _messageService.GetById(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }
            return HandleErrorActionResult(result);
        }

        public IHttpActionResult Post(Message data)
        {
            var result = _messageService.Create(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("Message", result.ActionStatus.Id);
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
        public IHttpActionResult Put(int id, Message data)
        {
            data.Id = id;
            var result = _messageService.Update(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created ? CreateHttpActionResult("Message", result.ActionStatus.Id) : StatusCode(HttpStatusCode.NoContent);
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
            var result = _messageService.Delete(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return HandleErrorActionResult(result);
        }
    }
}
