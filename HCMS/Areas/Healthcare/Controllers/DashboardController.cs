using Microsoft.AspNetCore.Mvc;
using HCMS.Models;
using HCMS.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HCMS.Area.Healthcare.Controllers
{
    [Area("Healthcare")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

      
    }
}
