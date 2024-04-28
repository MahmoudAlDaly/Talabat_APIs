using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseAPIsController
	{
		private readonly IGenericRepository<Product> ProductRepo;
		private readonly IGenericRepository<ProductBrand> BrandsRepo;
		private readonly IGenericRepository<ProductCategory> Categoriesrepo;

		public IMapper Mapper { get; }

		public ProductController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> brandsRepo,IGenericRepository<ProductCategory> categoriesrepo ,IMapper mapper)
        {
			ProductRepo = productRepo;
			BrandsRepo = brandsRepo;
			Categoriesrepo = categoriesrepo;
			Mapper = mapper;
		}

		[HttpGet]
		public async Task <ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParams);

			var products = await ProductRepo.GetAllWithSpecAsync(spec);

			var data = Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

			var countSpec = new ProductsWithFillteationsForCount(specParams);

			var count = await ProductRepo.GetCountAsync(countSpec);

			return Ok(new Pagination<ProductToReturnDTO>(specParams.PageIndex,specParams.PageSize,count,data));
		}


		[ProducesResponseType(typeof(ProductToReturnDTO),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]  // /api/products/1
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await ProductRepo.GetWithSpecAsync(spec);

			if (product is null)
			{
				return NotFound(new ApiResponse(404));
			}

			return Ok(Mapper.Map<Product,ProductToReturnDTO>(product)); //200
		}


		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await BrandsRepo.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await Categoriesrepo.GetAllAsync();
			return Ok(categories);
		}
	}

}
