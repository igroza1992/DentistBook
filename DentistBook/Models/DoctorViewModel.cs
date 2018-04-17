using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class DoctorViewModel
    {
        public int DoctorID { get; set; }

        [Display(Name = "Doctor Name")]
        [Required(ErrorMessage = "Insert Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert surname")]
        [Display(Name = "Doctor Surname")]
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DayOfBirth { get; set; }

        public List<CheckBoxViewModel> Procedures { get; set; }
    }
}