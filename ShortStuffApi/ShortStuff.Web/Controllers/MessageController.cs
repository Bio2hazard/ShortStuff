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
    public class MessageController : BaseController
    {
        private IMessageService _messageService;
        

        public MessageController(IUnitOfWork unitOfWork, IMessageService messageService) : base(unitOfWork)
        {
            _messageService = messageService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.MessageRepository.GetAll());
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
                return GetHttpActionResult(UnitOfWork.MessageRepository.GetById(id));
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

        public IHttpActionResult Post(Message data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }

            var status = UnitOfWork.MessageRepository.Create(data);

            if (status.status == CreateStatusEnum.Conflict)
                return Conflict();

            return CreateHttpActionResult("Message", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Message data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }
            data.Id = id;

            var status = UnitOfWork.MessageRepository.Update(data);

            switch (status)
            {
                    case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            UnitOfWork.MessageRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
