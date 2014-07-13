// ShortStuff.Web
// UserAsyncController.cs
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
    public class UserAsyncController : BaseController
    {
        private readonly IUserService _userService;

        public UserAsyncController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _userService.GetAllAsync();
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionDataSet);
            }
            return HandleErrorActionResult(result);
        }

        public async Task<IHttpActionResult> Get(decimal id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.ActionStatus.Status == ActionStatusEnum.Success)
            {
                return GetHttpActionResult(result.ActionData);
            }
            return HandleErrorActionResult(result);
        }

        public async Task<IHttpActionResult> Post(User data)
        {
            var result = await _userService.CreateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return CreateHttpActionResult("UserAsync", result.ActionStatus.Id);
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
        public async Task<IHttpActionResult> Put(decimal id, User data)
        {
            data.Id = id;
            var result = await _userService.UpdateAsync(data);

            switch (result.ActionStatus.Status)
            {
                case ActionStatusEnum.Success:
                    return result.ActionStatus.SubStatus == ActionSubStatusEnum.Created ? CreateHttpActionResult("UserAsync", result.ActionStatus.Id) : StatusCode(HttpStatusCode.NoContent);
                case ActionStatusEnum.ValidationError:
                    return ApiControllerExtension.BadRequest(this, result.BrokenValidationRules, data.GetType()
                                                                                                     .Name);
                case ActionStatusEnum.Conflict:
                    return Conflict();
            }
            return HandleErrorActionResult(result);
        }

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
        //[HttpGet]
        //[Route("api/userasync/{id:decimal}/feed")]
        //public async Task<IHttpActionResult> GetUserFeed(decimal id, [FromUri] bool includeReplies = false)
        //{
        //    return Ok();
        //}
    }
}
