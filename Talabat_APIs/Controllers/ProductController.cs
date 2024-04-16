using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

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
			var spec = new BaseSpecifications<Product>();

			var products = await ProductRepo.GetAllWithSpecAsync(spec);

			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var products = await ProductRepo.GetWithSpecAsync(spec);

			if (products is null)
			{
				return NotFound(new { message = "Not Found", StatusCode = 404 });
			}

			return Ok(products); //200
		}
    }

}
