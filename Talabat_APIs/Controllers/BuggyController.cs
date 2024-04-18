using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
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
				return NotFound(new ApiResponse(404));
			}

			return Ok(product);
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var product = DbContext.Products.Find(100);

			var producttoreturn = product.ToString();

			return Ok(producttoreturn);
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

		[HttpGet("Unauthorized")]
		public ActionResult GetUnauthorizedError()
		{
			return Unauthorized(new ApiResponse(401));
		}

	}
}
