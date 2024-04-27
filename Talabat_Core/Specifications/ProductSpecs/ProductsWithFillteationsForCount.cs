using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
	public class ProductsWithFillteationsForCount : BaseSpecifications<Product>
	{
        public ProductsWithFillteationsForCount(ProductSpecParams specParams) 
            : base(p =>
					(!specParams.BrandId.HasValue || p.BrandID == specParams.BrandId.Value) &&
					(!specParams.CategoryId.HasValue || p.CategoryID == specParams.CategoryId.Value)
                  )


        {
            
        }
    }
}
