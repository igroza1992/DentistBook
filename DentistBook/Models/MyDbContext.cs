﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DentistBook.Models
{
    public class MyDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MyDbContext() : base("name=MyDbContext")
        {
        }

        public System.Data.Entity.DbSet<DentistBook.Models.Doctor> Doctors { get; set; }

        public System.Data.Entity.DbSet<DentistBook.Models.Procedure> Procedures { get; set; }
        public System.Data.Entity.DbSet<DentistBook.Models.DoctorToProcedure> DoctorToProcedures { get; set; }

        public System.Data.Entity.DbSet<DentistBook.Models.Pacient> Pacients { get; set; }
    }
}
