using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DentistBook.Models;

namespace DentistBook.Controllers
{
    public class DoctorsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Doctors
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            var Results = from b in db.Procedures
                select new
                {
                    b.ProcedureID,
                    b.Name,
                    Checked = ((from ab in db.DoctorToProcedures
                        where (ab.DoctorID == id) & (ab.ProcedureId == b.ProcedureID)
                        select ab).Any())
                };

            var MyViewModel = new DoctorViewModel();
            MyViewModel.DoctorID = id.Value;
            MyViewModel.Name = doctor.Name;
            MyViewModel.Surname = doctor.Surname;
            MyViewModel.DayOfBirth = doctor.DayOfBirth;

            var MyCheckBoxlist = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxlist.Add(new CheckBoxViewModel { Id = item.ProcedureID, Name = item.Name, Cheked = item.Checked });
            }

            MyViewModel.Procedures = MyCheckBoxlist;
            return View(MyViewModel);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            var procedure = db.Procedures.ToList();
            var MyCheckBoxlist = new List<CheckBoxViewModel>();

            foreach (var item in procedure)
            {
                MyCheckBoxlist.Add(new CheckBoxViewModel { Id = item.ProcedureID, Name = item.Name , Cheked = false});
            }
            var ViewModel = new DoctorViewModel()
            {
                Procedures = MyCheckBoxlist
            };
            return View("Create",ViewModel);
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,Name,Surname,DayOfBirth,Procedures")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var MyDoctor = db.Doctors.Create();
                MyDoctor.Name = doctor.Name;
                MyDoctor.Surname = doctor.Surname;
                MyDoctor.DayOfBirth = doctor.DayOfBirth.Date;



                foreach (var item2 in doctor.Procedures)
                {
                    if (item2.Cheked)
                    {
                        db.DoctorToProcedures.Add(new DoctorToProcedure()
                        {
                            DoctorID = doctor.DoctorID,
                            ProcedureId = item2.Id
                        });
                    }
                }

                db.Doctors.Add(MyDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }


            var Results = from b in db.Procedures
                select new
                {
                    b.ProcedureID,
                    b.Name,
                    Checked = ((from ab in db.DoctorToProcedures
                        where (ab.DoctorID == id) & (ab.ProcedureId == b.ProcedureID)
                        select ab).Any())
                };

            var  MyViewModel = new DoctorViewModel();
            MyViewModel.DoctorID = id.Value;
            MyViewModel.Name = doctor.Name;
            MyViewModel.Surname = doctor.Surname;
            MyViewModel.DayOfBirth = doctor.DayOfBirth;

            var MyCheckBoxlist = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxlist.Add(new CheckBoxViewModel {Id = item.ProcedureID, Name = item.Name, Cheked = item.Checked});
            }

            MyViewModel.Procedures = MyCheckBoxlist;
            return View(MyViewModel);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorViewModel doctor)
        {
            if (ModelState.IsValid)
            {

                var MyDoctor = db.Doctors.Find(doctor.DoctorID);
                MyDoctor.Name = doctor.Name;
                MyDoctor.Surname = doctor.Surname;
                MyDoctor.DayOfBirth = doctor.DayOfBirth;

                foreach (var item in db.DoctorToProcedures)
                {
                    if (item.DoctorID == doctor.DoctorID)
                    {
                        db.Entry(item).State= System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item2 in doctor.Procedures)
                {
                    if (item2.Cheked)
                    {
                        db.DoctorToProcedures.Add(new DoctorToProcedure()
                        {
                            DoctorID = doctor.DoctorID,
                            ProcedureId = item2.Id
                        });
                    }
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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
