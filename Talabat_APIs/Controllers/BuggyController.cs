using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : BaseAPIsController
	{
		private readonly StoreContext DbContext;

		public BuggyController(StoreContext dbcontext)
        {
			DbContext = dbcontext;
		}

		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = DbContext.Products.Find(100);

			if (product is null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		[HttpGet("servererror")]
		public ActionResult GetNotFoundResponse()
		{
			var product = DbContext.Products.Find(100);

			var producttoreturn = product.ToString();

			return Ok(producttoreturn);
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest();
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

	}
}
