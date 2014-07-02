using System.Web.Http;
using System.Web.Http.Routing;
using ShortStuff.Domain;

namespace ShortStuff.Web.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

                return Created(link, new { id = entity, url = link});
            }
            return BadRequest();
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
    }
}
