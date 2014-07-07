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
    public class EchoController : BaseController
    {
        private IEchoService _echoService;
        

        public EchoController(IUnitOfWork unitOfWork, IEchoService echoService) : base(unitOfWork)
        {
            _echoService = echoService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.EchoRepository.GetAll());
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
                return GetHttpActionResult(UnitOfWork.EchoRepository.GetById(id));
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

        public IHttpActionResult Post(Echo data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }

            var status = UnitOfWork.EchoRepository.Create(data);

            if (status.Status == CreateStatusEnum.Conflict)
                return Conflict();

            return CreateHttpActionResult("Echo", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(int id, Echo data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }
            data.Id = id;

            var status = UnitOfWork.EchoRepository.Update(data);

            switch (status)
            {
                    case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            UnitOfWork.EchoRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
