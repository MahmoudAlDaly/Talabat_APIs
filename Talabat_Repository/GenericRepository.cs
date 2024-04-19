using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext DbContext;

		public GenericRepository(StoreContext dbcontext)
        {
			DbContext = dbcontext;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			#region MyRegion
			//if (typeof(T) == typeof(Product))
			//{
			//	return (IEnumerable<T>)await DbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			//}
			//return await DbContext.Set<T>().ToListAsync();

			//if (typeof(T) == typeof(Product))
			//{
			//	return (IEnumerable<T>)await DbContext.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			//} 
			#endregion

			return await DbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await DbContext.Set<T>().FindAsync(id);
		}


		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(DbContext.Set<T>(),spec);
		}
	}
}
