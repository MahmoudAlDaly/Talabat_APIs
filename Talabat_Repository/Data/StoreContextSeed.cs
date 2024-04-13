using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext context)
		{
			//if (context.ProductBrands.Count() == 0)
			//{
			//	var brandsdata = File.ReadAllText("../Talabat_Repository/Data/DataSeed/brands.json");

			//	var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

			//	if (brands?.Count > 0)
			//	{
			//		brands = brands.Select(b => new ProductBrand()
			//		{
			//			Name = b.Name,
			//		}).ToList();

			//		foreach (var item in brands)
			//		{
			//			context.Set<ProductBrand>().Add(item);
			//		}

			//		await context.SaveChangesAsync();
			//	}
			//}

			//if (context.ProductCategories.Count() == 0)
			//{
			//	var categoriesdata = File.ReadAllText("../Talabat_Repository/Data/DataSeed/categories.json");

			//	var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesdata);

			//	if (categories?.Count > 0)
			//	{
			//		categories = categories.Select(b => new ProductCategory()
			//		{
			//			Name = b.Name,
			//		}).ToList();

			//		foreach (var category in categories)
			//		{
			//			context.Set<ProductCategory>().Add(category);
			//		}

			//		await context.SaveChangesAsync();
			//	}
			//}

			if (context.Products.Count() == 0)
			{
				var productsdata = File.ReadAllText("../Talabat_Repository/Data/DataSeed/products.json");

				var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

				if (products?.Count > 0)
				{
					//products = products.Select(b => new Product()
					//{
					//	Name = b.Name,
					//}).ToList();

					foreach (var product in products)
					{
						context.Set<Product>().Add(product);
					}

					await context.SaveChangesAsync();
				}
			}
		}
	}
}
