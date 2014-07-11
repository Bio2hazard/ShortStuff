// ShortStuff.Web
// UserController.cs
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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(_userService.GetAll());
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

        public IHttpActionResult Get(decimal id)
        {
            try
            {
                return GetHttpActionResult(_userService.GetById(id));
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

        public IHttpActionResult Post(User data)
        {
            var brokenRules = data.GetBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }

            var status = _userService.Create(data);

            if (status.Status == CreateStatusEnum.Conflict)
            {
                return Conflict();
            }

            return CreateHttpActionResult("User", status.Id);
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(decimal id, User data)
        {
            var brokenRules = data.GetUpdateBrokenRules();
            var validationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (validationRules.Any())
            {
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType()
                                                                                    .Name);
            }
            data.Id = id;

            var status = _userService.Update(data);

            switch (status)
            {
                case UpdateStatus.NotFound:
                    return Post(data);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(decimal id)
        {
            _userService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("api/user/bytag/{tag}")]
        public IHttpActionResult GetUserByTag(string tag)
        {
            try
            {
                return GetHttpActionResult(_userService.GetByTag(tag));
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
    }
}
