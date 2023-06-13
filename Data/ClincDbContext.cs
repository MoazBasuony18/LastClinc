using LastClinc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Data
{
    public class ClincDbContext:IdentityDbContext
    {
        public ClincDbContext(DbContextOptions<ClincDbContext> options) : base(options)
        {

        }
        public DbSet<PatientData> PatientDatas { get; set; }
        public DbSet<PatientDataForDoctor> PatientDataForDoctors { get; set; }
    }
}
