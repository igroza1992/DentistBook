using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class DoctorToProcedure
    {
        public int  DoctorToProcedureID { get; set; }
        public int DoctorID { get; set; }
        public int ProcedureId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Procedure Procedure { get; set; }  

    }
}