using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
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
			if (typeof(T) == typeof(Product))
			{
				return (IEnumerable<T>)await DbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			}
			return await DbContext.Set<T>().ToListAsync();

			if (typeof(T) == typeof(Product))
			{
				return (IEnumerable<T>)await DbContext.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			}
			return await DbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await DbContext.Set<T>().FindAsync(id);
		}
	}
}
