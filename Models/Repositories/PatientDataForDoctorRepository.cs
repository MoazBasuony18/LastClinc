using LastClinc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Models.Repositories
{
    public class PatientDataForDoctorRepository : IClincRepository<PatientDataForDoctor>
    {
        ClincDbContext db;
        public PatientDataForDoctorRepository(ClincDbContext _db)
        {
            db = _db;
        }
        public void Add(PatientDataForDoctor entity)
        {
            db.PatientDataForDoctors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var patients = FindById(id);
            db.PatientDataForDoctors.Remove(patients);
            db.SaveChanges();
        }

        public PatientDataForDoctor FindById(int id)
        {
            var patients = db.PatientDataForDoctors.Include(b => b.PatientData).SingleOrDefault(b => b.Id == id);
            return patients;
        }

        public IList<PatientDataForDoctor> List()
        {
            return db.PatientDataForDoctors.Include(b=>b.PatientData).ToList();
        }

        public void Update(int id, PatientDataForDoctor newPatientDoctor)
        {
            db.Update(newPatientDoctor);
            db.SaveChanges();
        }
        public List<PatientDataForDoctor> Search(string searchString)
        {
            var result = db.PatientDataForDoctors.Include(b => b.PatientData).Where(a => a.Complain.Contains(searchString) ||
                a.Diagnosis.Contains(searchString) ||
                    a.PatientData.PatientName.Contains(searchString)||a.Treatment.Contains(searchString)||a.Medicine.Contains(searchString)).ToList();
            return result;
        }
    }
}
