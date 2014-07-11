// ShortStuff.Web
// EchoController.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class EchoController : BaseController
    {
        private readonly IEchoService _echoService;

        public EchoController(IEchoService echoService)
        {
            _echoService = echoService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(_echoService.GetAll());
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
                return GetHttpActionResult(_echoService.GetById(id));
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
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }

            var status = _echoService.Create(data);

            if (status.Status == CreateStatusEnum.Conflict)
            {
                return Conflict();
            }

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
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }
            data.Id = id;

            var status = _echoService.Update(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            _echoService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
