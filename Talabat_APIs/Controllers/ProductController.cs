using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.APIs.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseAPIsController
	{
		private readonly IGenericRepository<Product> ProductRepo;

		public IMapper Mapper { get; }

		public ProductController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
			ProductRepo = productRepo;
			Mapper = mapper;
		}

		[HttpGet]
		public async Task <ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
		{
			var spec = new BaseSpecifications<Product>();

			var products = await ProductRepo.GetAllWithSpecAsync(spec);

			return Ok(Mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var products = await ProductRepo.GetWithSpecAsync(spec);

			if (products is null)
			{
				return NotFound(new { message = "Not Found", StatusCode = 404 });
			}

			return Ok(Mapper.Map<Product,ProductToReturnDTO>(products)); //200
		}
    }

}
