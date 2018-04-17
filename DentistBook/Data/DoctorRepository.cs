using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DentistBook.Models;

namespace DentistBook.Data
{
    public class DoctorRepository
    {
        public IEnumerable<SelectListItem> GetDoctor(string id)
        {
            using (var context = new MyDbContext())
            {
                int ID = Convert.ToInt32(id);
                var doctor = from b in context.Doctors
                    join cn in context.DoctorToProcedures on b.DoctorID equals cn.DoctorID
                    where (cn.ProcedureId == ID)
                    select new { b.DoctorID, b.Name, b.Surname };

                List<SelectListItem> doctors = doctor.OrderBy(n => n.Name)
                    .Select(n => new SelectListItem
                    {
                        Value = n.DoctorID.ToString(),
                        Text = n.Name + " " + n.Surname
                    }).ToList();

                var doctorTip = new SelectListItem()
                {
                    Value = null,
                    Text = "-- Select Doctor --"
                };
                doctors.Insert(0,doctorTip);

                return new SelectList(doctors,"Value","Text");
            }
        }

        public IEnumerable<SelectListItem> GetHour(string date, string Doctor )
        {
            int IdDoctor = Convert.ToInt32(Doctor);
            DateTime Date = Convert.ToDateTime(date);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("8:00", 1);
            dict.Add("9:00", 2);
            dict.Add("10:00", 3);
            dict.Add("11:00", 4);
            dict.Add("13:00", 5);
            dict.Add("14:00", 6);
            dict.Add("15:00", 7);
            dict.Add("16:00", 8);
           
            using (var context = new MyDbContext())
            {
                var freeHour = from b in context.Pacients
                    where (b.DoctorId == IdDoctor && b.DateReservation == Date)
                    select new { b.HourIdReservation};

                foreach (var VARIABLE in freeHour)
                {
                    dict.Remove(VARIABLE.HourIdReservation);
                }

                List<SelectListItem> hours = dict.Select(n => new SelectListItem
                {
                    Value = n.Key,
                    Text = n.Key
                }).ToList();

                var hourFirst = new SelectListItem()
                {
                    Value = null,
                    Text = "-- Select Hour --"
                };
                hours.Insert(0, hourFirst);
                return new SelectList(hours, "Value", "Text");
            }
        }
    }
}