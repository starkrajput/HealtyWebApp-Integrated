using Microsoft.AspNetCore.Mvc;
using HCMS.Models;
using HCMS.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace HCMS.Area.Healthcare.Controllers
{
    [Area("Healthcare")]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Doctors.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(doctor);
        }

        public ActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(doctor).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        public ActionResult Details(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        public ActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
