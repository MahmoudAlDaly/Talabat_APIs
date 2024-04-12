using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseAPIsController
	{
		private readonly IGenericRepository<Product> ProductRepo;

		public ProductController(IGenericRepository<Product> productRepo)
        {
			ProductRepo = productRepo;
		}

		[HttpGet]
		public async Task <ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await ProductRepo.GetAllAsync();

			return Ok(products);
		}
    }

}
