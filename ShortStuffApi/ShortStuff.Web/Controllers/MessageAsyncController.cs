// ShortStuff.Web
// MessageAsyncController.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class MessageAsyncController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessageAsyncController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _messageService.GetAllAsync();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }
            return HandleErrorActionResult(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _messageService.GetByIdAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }
            return HandleErrorActionResult(result);
        }

        public async Task<IHttpActionResult> Post(Message data)
        {
            var result = await _messageService.CreateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("MessageAsync", result.ActionStatus.Id);
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
        public async Task<IHttpActionResult> Put(int id, Message data)
        {
            data.Id = id;
            var result = await _messageService.UpdateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created ? CreateHttpActionResult("MessageAsync", result.ActionStatus.Id) : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType()
                                                                                                     .Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }
            return HandleErrorActionResult(result);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _messageService.DeleteAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return HandleErrorActionResult(result);
        }
    }
}
