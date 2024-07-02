using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor_1.Data;
using WebRazor_1.Models;

namespace WebRazor_1.Pages.Categories
{
	//[BindProperties]
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		[BindProperty]
		public Category Category { get; set; }
		public CreateModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet()
		{
			
		}
		public IActionResult OnPost() {
			_db.Categories.Add(Category);
			_db.SaveChanges();
			TempData["success"] = "Category via Razor Added Successfully.";
			return RedirectToPage("Index");
			
		}
	}
}
