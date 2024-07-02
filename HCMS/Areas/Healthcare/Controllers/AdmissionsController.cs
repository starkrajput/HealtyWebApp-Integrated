using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using CsvHelper;
using System.Globalization;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using HCMS.Models;
using HCMS.DataAccess.Data;
using HCMS.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using Microsoft.Identity.Client.NativeInterop;
using System.Reflection.Metadata.Ecma335;

namespace HCMS.Area.Healthcare.Controllers
{
    [Area("Healthcare")]
    public class AdmissionsController : Controller
    {
        private readonly ApplicationDbContext  _context;

        public AdmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var admissions = _context.Admissions.Include(a => a.Patient).Include(a => a.AttendingDoctor).ToList();
            return View(admissions);
        }

        public ActionResult Create()
        {
            var patients = _context.Patients.ToList();
            var doctors = _context.Doctors.ToList();

            // Check if patients or doctors are null
            if (patients == null || doctors == null)
            {
                // Log an error or handle the null case as appropriate
                return View("Show Down");
            }

            ViewBag.Patients = new SelectList(patients, "PatientId", "FirstName");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FirstName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Admissions.Add(admission);
                _context.SaveChanges();
                return RedirectToAction("Index","Admissions");
            }

            // Log the validation errors for debugging purposes
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var patients = _context.Patients.ToList();
            var doctors = _context.Doctors.ToList();

            ViewBag.Patients = new SelectList(patients, "PatientId", "FirstName", admission.PatientId);
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "FirstName", admission.AttendingDoctorId);

            return View(admission);
        }

        public ActionResult Edit(int id)
        {
            var admission = _context.Admissions.Find(id);
            if (admission == null)
            {
                return View();
            }

            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "FirstName", admission.PatientId);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "FirstName", admission.AttendingDoctorId);
            return View(admission);
        }

        [HttpPost]
        public ActionResult Edit(Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(admission).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "FirstName", admission.PatientId);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "FirstName", admission.AttendingDoctorId);
            return View(admission);
        }

        public ActionResult Details(int id)
        {
            var admission = _context.Admissions.Include(a => a.Patient).Include(a => a.AttendingDoctor).FirstOrDefault(a => a.AdmissionId == id);
            if (admission == null)
            {
                return RedirectToAction("Index");
            }

            return View(admission);
        }

        public ActionResult Delete(int id)
        {
            var admission = _context.Admissions.Find(id);
            if (admission == null)
            {
                return RedirectToAction("Index");
            }

            return View(admission);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var admission = _context.Admissions.Find(id);
            _context.Admissions.Remove(admission);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}