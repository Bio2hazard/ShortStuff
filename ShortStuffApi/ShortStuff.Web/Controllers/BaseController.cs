// ShortStuff.Web
// BaseController.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Web.Http;
using System.Web.Http.Routing;

namespace ShortStuff.Web.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<TEntity>(TEntity entity)
        {
            if (!Equals(entity, default(TEntity)))
            {
                return Ok(entity);
            }
            return NotFound();
        }

        protected IHttpActionResult CreateHttpActionResult<TEntity>(string controllerName, TEntity entity)
        {
            if (!Equals(entity, default(TEntity)))
            {
                var urlHelper = new UrlHelper(Request);
                var link = urlHelper.Link("DefaultApi", new
                {
                    controller = controllerName,
                    id = entity
                });

                return Created(link, new
                {
                    id = entity,
                    url = link
                });
            }
            return BadRequest();
        }
    }
}
