
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using HCMS.Models;

namespace HCMS.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>,IProductRepository
	{
		private readonly ApplicationDbContext _db;
		// Passing these implementatin to all base class.
		public ProductRepository(ApplicationDbContext db) : base(db) {
			_db = db;
		}
		

		public void Update(Product obj)
		{
			//_db.Products.Update(obj);
			var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
			if (objFromDb != null) { 
				objFromDb.Title = obj.Title;
				objFromDb.Description = obj.Description;
				objFromDb.Category = obj.Category;
				objFromDb.ISBN = obj.ISBN;
				objFromDb.Price = obj.Price;
				objFromDb.Price50 = obj.Price50;
				objFromDb.Price100 = obj.Price100;
				objFromDb.Author = obj.Author;
				objFromDb.ListPrice = obj.ListPrice;
				if (obj.ImageUrl!=null) {
					objFromDb.ImageUrl = obj.ImageUrl;
				}
			}
		}
	}
}
