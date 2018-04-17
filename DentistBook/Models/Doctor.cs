using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        [Required(ErrorMessage = "Insert Name")]
        [Display(Name = "Doctor Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert surname")]
        [Display(Name = "Doctor Surname")]
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }
        public List<CheckBoxViewModel> Procedures { get; set; }


        public virtual ICollection<DoctorToProcedure> DoctorToProcedures { get; set; }
        public virtual ICollection<Pacient> Pacients { get; set; }
    }
}