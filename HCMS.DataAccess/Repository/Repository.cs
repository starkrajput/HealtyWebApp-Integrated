using System.Linq.Expressions;
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HCMS.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db) {
			_db = db;
			// This will set dbset to Categories.
			this.dbSet = _db.Set<T>();
			// Here we are getting foreign rel data in prodoct which are connected via product table like category.
			_db.Products.Include(u => u.Category);
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
			//throw new NotImplementedException();
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();
		}

		// if there is category ,categoryid
		public IEnumerable<T> GetAll(string? includeProperties =null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties) ) { 
			   foreach(var includeProp in  includeProperties.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)) {
					query = query.Include(includeProp);
				}
			}
			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
