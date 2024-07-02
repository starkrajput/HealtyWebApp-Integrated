using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor_1.Data;
using WebRazor_1.Models;

namespace WebRazor_1.Pages.Categories
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public List<Category> CategoryList { get; set; }
		public IndexModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet()
		{
			CategoryList = _db.Categories.ToList();
		}
	}
}
