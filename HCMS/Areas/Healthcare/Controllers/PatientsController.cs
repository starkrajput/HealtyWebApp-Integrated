using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HCMS.Models;
using HCMS.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HCMS.Area.Healthcare.Controllers
{
    [Area("Healthcare")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Patients.ToList());
        }

        public ActionResult Create()
        {
            var doctors = _context.Doctors.ToList();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FirstName");

            var diseases = new List<String>{"Dis1","Dis2"};
            ViewBag.Diseases = diseases;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(patient);
        }

        public ActionResult Edit(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return RedirectToAction("Index");
            }
            var doctors = _context.Doctors.ToList();
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FirstName");

            var diseases = new List<String> { "Dis1", "Dis2" };
            ViewBag.Diseases = diseases;
            return View(patient);
        }

        [HttpPost]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(patient).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public ActionResult Details(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public ActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patients.Find(id);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
