using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Services;
using ShortStuff.Web.Extensions;

namespace ShortStuff.Web.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        

        public UserController(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            _userService = userService;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.UserRepository.GetAll());
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

        public IHttpActionResult Get(string id)
        {
            try
            {
                return GetHttpActionResult(UnitOfWork.UserRepository.GetById(id));
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
                return ApiControllerExtension.BadRequest(this, validationRules, data.GetType().Name);
            }
            return CreateHttpActionResult("User", UnitOfWork.UserRepository.Create(data));
        }

        [HttpPatch]
        [HttpPut]
        public IHttpActionResult Put(string id, User data)
        {
            data.Id = id;
            UnitOfWork.UserRepository.Update(data);
            return Ok();
        }
    }
}
