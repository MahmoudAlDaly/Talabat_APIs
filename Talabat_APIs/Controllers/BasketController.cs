using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	//[Route("api/[controller]")]
	//[ApiController]
	public class BasketController : BaseAPIsController
	{
		private readonly IBasketRepository BasketRepository;

		public BasketController(IBasketRepository basketRepository)
        {
			BasketRepository = basketRepository;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await BasketRepository.GetBasketAsync(id);
			return basket ?? new CustomerBasket(id);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> updateBasket(CustomerBasket customerBasket)
		{
			var createOrUpdateBasket = await BasketRepository.UpdateBasketAsync(customerBasket);
			if (createOrUpdateBasket is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(createOrUpdateBasket);
		}

		[HttpDelete]
		public async Task DeleteBaskt(string id)
		{
			await BasketRepository.DeleteBasketAsync(id);
		}
    }
}
