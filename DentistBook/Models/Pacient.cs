using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class Pacient
    {
        public int PacientId { get; set; }

        [Required(ErrorMessage = "Insert your name")]
        [StringLength(30,ErrorMessage = "The field {0} can contain maximum {1} characters and minimum {2} characters",MinimumLength = 3)]
        [Display(Name = "Pacient Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert your phone number")]
        [Display(Name = "Phone number ")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Insert your email adres")]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }

        public string  Coments { get; set; }

        [Display(Name = "Doctor Name")]
        public int DoctorId { get; set; }

        [Display(Name = "Procedure ")]
        public int ProcedureId { get; set; }

        [Required(ErrorMessage = "Insert reservation date ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd}", ApplyFormatInEditMode = true)]
        [MinData]
        public DateTime DateReservation { get; set; }

     
        [Required(ErrorMessage = "Insert reservation time ")]
        public string HourIdReservation { get; set; }

        public virtual Procedure Procedure { get; set; }
        public virtual Doctor Doctor { get; set; }  
      
    }
}