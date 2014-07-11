// ShortStuff.Web
// TopicAsyncController.cs
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
    public class TopicAsyncController : BaseController
    {
        private readonly ITopicService _topicService;

        public TopicAsyncController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return GetHttpActionResult(await _topicService.GetAllAsync());
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
                return GetHttpActionResult(await _topicService.GetByIdAsync(id));
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

        public async Task<IHttpActionResult> Post(Topic data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }

            var status = await _topicService.CreateAsync(data);

            if (status.Status == CreateStatusEnum.Conflict)
            {
                return Conflict();
            }

            return CreateHttpActionResult("TopicAsync", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Topic data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }
            data.Id = id;

            var status = await _topicService.UpdateAsync(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return await Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            await _topicService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
