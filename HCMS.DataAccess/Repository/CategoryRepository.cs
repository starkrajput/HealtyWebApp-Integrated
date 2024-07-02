using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using HCMS.Models;

namespace HCMS.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>,ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		// Passing these implementatin to all base class.
		public CategoryRepository(ApplicationDbContext db) : base(db) {
			_db = db;
		}
		

		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}
