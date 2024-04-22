using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
		{
			var query = inputQuery;  // dbcontext.set<product>()

			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}

			if (spec.orderBy != null)
			{
				query = query.OrderBy(spec.orderBy);
			}
			else if (spec.orderByDesc != null)
			{
				query = query.OrderByDescending(spec.orderByDesc);
			}

			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			return query;
		}
	}
}
