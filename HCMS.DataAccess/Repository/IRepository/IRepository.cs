using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCMS.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T:class
	{
		// T is caetgory or any genric model in db context.
		IEnumerable<T> GetAll(string? includeProperties = null);
		// To Gte any attribute data from db category. so we cant use Find()
		T Get(Expression<Func<T,bool>> filter, string? includeProperties = null); // To handle lin ksepaeration (u=> u.id=id)
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
