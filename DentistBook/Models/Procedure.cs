using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class Procedure
    {
        public int ProcedureID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DoctorToProcedure> DoctorToProcedures { get; set; }
        public virtual ICollection<Pacient> Pacients { get; set; }
    }
}   