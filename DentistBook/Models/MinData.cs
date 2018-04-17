using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class MinData : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var pacient = (Pacient) validationContext.ObjectInstance;
            if (pacient.DateReservation >= DateTime.Now)
                return ValidationResult.Success;
            else
                return new ValidationResult("Reservation date should be least form actual date ");
        }
    }
}