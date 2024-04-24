using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) :
			base(p =>
					(!specParams.BrandId.HasValue || p.BrandID == specParams.BrandId.Value) &&
					(!specParams.CategoryId.HasValue || p.CategoryID == specParams.CategoryId.Value)

				)
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);

			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						//orderBy = p => p.Price;
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						//orderByDesc = p => p.Price;
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(p => p.Name);
			}

			ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);
		}

		public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.ID == id)
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}
