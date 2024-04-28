using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase Database ;

		public BasketRepository(IConnectionMultiplexer redis)
        {
			Database = redis.GetDatabase();
		}
        public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await Database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var basket = await Database.StringGetAsync(basketId);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var createdOrUpdated = await Database.StringSetAsync(basket.ID, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

			if (createdOrUpdated is false)
			{
				return null;
			}

			return await GetBasketAsync(basket.ID);
		}
	}
}
