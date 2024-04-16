﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(p=> p.Brand);
            Includes.Add(p=> p.Category);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p=> p.ID == id)
        {
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
    }
}