using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> orderBy { get; set; } = null;
        public Expression<Func<T, object>> orderByDesc { get; set; } = null;
		public int Skip { get ; set ; }
		public int Take { get ; set ; }
		public bool IsPaginationEnable { get ; set ; }

		public BaseSpecifications()
        {

        }

        public BaseSpecifications(Expression<Func<T,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
			orderBy = orderByExpression;

		}

		public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
		{
			orderByDesc = OrderByDescExpression;

		}

		public void ApplyPagination(int skip , int take)
		{
			IsPaginationEnable = true;
			Skip = skip;
			Take = take;
		}
	}
}
