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
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMS.Area.Healthcare.Controllers
{
    [Area("Healthcare")]
    public class MedicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var medications = _context.Medications.Include(m => m.Patient).ToList();
            return View(medications);
        }

        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "FullName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _context.Medications.Add(medication);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "FullName", medication.PatientId);
            return View(medication);
        }

        public ActionResult Edit(int id)
        {
            var medication = _context.Medications.Find(id);
            if (medication == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "FullName", medication.PatientId);
            return View(medication);
        }

        [HttpPost]
        public ActionResult Edit(Medication medication)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(medication).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "FullName", medication.PatientId);
            return View(medication);
        }

        public ActionResult Details(int id)
        {
            var medication = _context.Medications.Include(m => m.Patient).FirstOrDefault(m => m.MedicationId == id);
            if (medication == null)
            {
                return RedirectToAction("Index");
            }

            return View(medication);
        }

        public ActionResult Delete(int id)
        {
            var medication = _context.Medications.Find(id);
            if (medication == null)
            {
                return RedirectToAction("Index");
            }

            return View(medication);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var medication = _context.Medications.Find(id);
            _context.Medications.Remove(medication);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}