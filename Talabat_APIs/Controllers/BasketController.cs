using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
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
		private readonly IMapper Mapper;

		public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
			BasketRepository = basketRepository;
			Mapper = mapper;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await BasketRepository.GetBasketAsync(id);
			return basket ?? new CustomerBasket(id);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> updateBasket(CustomerBasketDto Basket)
		{
			var mappedBasket = Mapper.Map<CustomerBasketDto, CustomerBasket>(Basket);

			var createOrUpdateBasket = await BasketRepository.UpdateBasketAsync(mappedBasket);
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
