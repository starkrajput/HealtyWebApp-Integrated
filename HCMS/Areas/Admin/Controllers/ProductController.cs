using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using HCMS.Models;
using HCMS.Models.ViewModels;
using HCMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace HCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

		}
        public IActionResult Index()
        {
            // Here var is List<Category>
            var objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            // projection in EF Core
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }); 
            // Display them index.cshtml which is passed as parameter below.
            return View(objProductList);
        }
/**/// ViewBag : transfer data from controller to view, not vice versa. Ideal where temporary data is not in model.
     //These are dynamic property. its life exists during the current http request.
        // its wrapper around viewdata.
        // ViewData ?

        // Update+Insert
        public IActionResult Upsert(int? id)
        {
           /* IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString(),
               });*/
            //ViewBag.CategoryList = CategoryList;
           // ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
				CategoryList =  _unitOfWork.Category
			   .GetAll().Select(u => new SelectListItem
			   {
				   Text = u.Name,
				   Value = u.Id.ToString(),
			   }),
			Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else {
                // Update 
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
            //return View(productVM);

            // if id is present that means we need to update else will creat new product

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) {
                    string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName) ;
                    string productPath = Path.Combine(wwwRootPath,@"images\product");
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else {
					_unitOfWork.Product.Update(productVM.Product);
				}
               // _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully.";
                return RedirectToAction("Index", "Product");
            }
            else {
				productVM.CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
				return View(productVM);
			}
           // return View();

        }

        //Edit Code :
        /*public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //	Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //	Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }*/
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully.";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }

       /* //Delete Code :
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //	Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //	Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }*/
        // Action name is to handle DeletePOST to have same funcitonlaity of delete function.
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");

        }

        // Mvc take api calls automatically
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
			var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
		}
        [HttpDelete]
        public IActionResult Delete(int? id) {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null) { 
              return Json(new {success=false,message="Error While Deleting"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, MessagePack = "Delete Successful" });
			// We have deleted Delete cshtml code and in this widnow delte function
			// @model Product

/*< form method = "post" >
	< input asp -for= "Id" hidden />
	< div class="border p-3 mt-4">
		<div class="row pb-2">
			<h2 class="text-primary">Delete Product</h2>
		</div>

		 <div asp-validation-summary= "All" ></ div >
		@* <div asp-validation-summary= "ModelOnly" ></ div > *@
		<div class="row pb-2">
			<label asp-for="Title"></label>
			<input type = "text" asp-for="Title"  disabaled class="form-control">
			
		</div>

		<div class="row pb-2">
			<label asp-for="Description"></label>
			<input type = "text" asp-for="Description" disabaled class="form-control">
			
		</div>

		<div class="row pb-2">
			<label asp-for="ISBN"></label>
			<input type = "text" asp-for="ISBN" disabaled class="form-control">
		
		</div>
		<div class="row pb-2">
			<label asp-for="Author"></label>
			<input type = "text" asp-for="Author" disabaled class="form-control">
			
		</div>
		<div class="row pb-2">
			<label asp-for="ListPrice"></label>
			<input type = "text" asp-for="ListPrice" disabaled class="form-control">
			
		</div>
		<div class="row pb-2">
			<label asp-for="Price"></label>
			<input type = "text" asp-for="Price" disabaled class="form-control">
			
		</div>
		<div class="row pb-2">
			<label asp-for="Price50"></label>
			<input type = "text" asp-for="Price50" disabaled class="form-control">
		
		</div>
		<div class="row pb-2">
			<label asp-for="Price100"></label>
			<input type = "text" asp-for="Price100" disabaled class="form-control">
		
		</div>

		<div class="row">
			<div class="col-6 col-md-3">
				<button type = "submit" class="btn btn-primary form-control" style="width:150px">Delete</button>

			</div>

			<div class="col-6 col-md-3">
				<a asp-controller="Product" asp-action="Index" clas="btn btn-secondary form-control" style="width:150px">Back To List</a>

			</div>
		</div>
	</div>
</form>

*/		
        }
	#endregion
}
}
