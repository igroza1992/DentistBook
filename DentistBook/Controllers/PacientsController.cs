using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DentistBook.Data;
using DentistBook.Methods;
using DentistBook.Models;

namespace DentistBook.Controllers
{
    public class PacientsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Pacients

        public ActionResult Index()
        {
            var pacients = db.Pacients.Include(p => p.Procedure );
                          
            return View(pacients.ToList());
        }

        // GET: Pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // GET: Pacients/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "Name");
            ViewBag.ProcedureId = new SelectList(db.Procedures, "ProcedureId", "Name");
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "PacientId,Name,PhoneNumber,Email,Coments,DoctorId,ProcedureId,DateReservation,HourIdReservation")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                db.Pacients.Add(pacient);
                db.SaveChanges();


                var doctorName = from b in db.Doctors
                    where b.DoctorID == pacient.DoctorId
                    select new {b.Name, b.Surname};
                var procedureName = from b in db.Procedures
                    where b.ProcedureID == pacient.ProcedureId
                    select new { b.Name };

                SendEmail send = new SendEmail();
                send.SendEmailMet(pacient.DateReservation.ToString(), doctorName.FirstOrDefault()?.Name+" "+ doctorName.FirstOrDefault()?.Surname, procedureName.FirstOrDefault()?.Name, pacient.HourIdReservation,pacient.Name,pacient.Email, out result);

              return RedirectToAction("Index");
               
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "Name", pacient.DoctorId);
            ViewBag.ProcedureId = new SelectList(db.Procedures, "ProcedureId", "Name",pacient.ProcedureId);
           return View(pacient);
         
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDoctor(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var repo = new DoctorRepository();
                IEnumerable<SelectListItem> enumar = repo.GetDoctor(id);
                return Json(enumar, JsonRequestBehavior.AllowGet);

            };

            
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetHour(string date , string doctor)
        {
            if (!string.IsNullOrWhiteSpace(date)&& !string.IsNullOrWhiteSpace(doctor))
            {
                var repo = new DoctorRepository();
                IEnumerable<SelectListItem> enumar = repo.GetHour(date, doctor);
                return Json(enumar, JsonRequestBehavior.AllowGet);

            };


            return null;
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "Name", pacient.DoctorId);
            return View(pacient);
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PacientId,Name,PhoneNumber,Email,Coments,DoctorId,ProcedureId")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "Name", pacient.DoctorId);
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacient pacient = db.Pacients.Find(id);
            db.Pacients.Remove(pacient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
