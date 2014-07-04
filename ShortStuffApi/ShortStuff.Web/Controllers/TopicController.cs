using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class TopicController : BaseController
    {
        private ITopicService _topicService;

        public TopicController(IUnitOfWork unitOfWork, ITopicService topicService) : base(unitOfWork)
        {
            _topicService = topicService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.TopicRepository.GetAll());
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
                return GetHttpActionResult(UnitOfWork.TopicRepository.GetById(id));
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

        public IHttpActionResult Post(Topic data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }

            var status = UnitOfWork.TopicRepository.Create(data);

            if (status.status == CreateStatusEnum.Conflict)
                return Conflict();

            return CreateHttpActionResult("Topic", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Topic data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }
            data.Id = id;

            var status = UnitOfWork.TopicRepository.Update(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            UnitOfWork.TopicRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
