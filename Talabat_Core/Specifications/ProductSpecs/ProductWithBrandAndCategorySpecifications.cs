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
        public ProductWithBrandAndCategorySpecifications(string sort) : base()
        {
            Includes.Add(p=> p.Brand);
            Includes.Add(p=> p.Category);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p=> p.ID == id)
        {
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
    }
}
