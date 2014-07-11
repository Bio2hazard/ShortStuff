// ShortStuff.Web
// UserAsyncController.cs
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
    public class UserAsyncController : BaseController
    {
        private readonly IUserService _userService;

        public UserAsyncController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return GetHttpActionResult(await _userService.GetAllAsync());
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

        public async Task<IHttpActionResult> Get(decimal id)
        {
            try
            {
                return GetHttpActionResult(await _userService.GetByIdAsync(id));
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

        public async Task<IHttpActionResult> Post(User data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }

            var status = await _userService.CreateAsync(data);

            if (status.Status == CreateStatusEnum.Conflict)
            {
                return Conflict();
            }

            return CreateHttpActionResult("UserAsync", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(decimal id, User data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }
            data.Id = id;

            var status = await _userService.UpdateAsync(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return await Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(decimal id)
        {
            await _userService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Service Methods

        [HttpGet]
        [Route("api/userasync/bytag/{tag}")]
        public async Task<IHttpActionResult> GetUserByTag(string tag)
        {
            try
            {
                return GetHttpActionResult(await _userService.GetByTagAsync(tag));
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
