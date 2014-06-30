using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Services;

namespace ShortStuff.Web.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }
    }
}
