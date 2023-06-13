using LastClinc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Models.Repositories
{
    public class PatientDataRepository : IClincRepository<PatientData>
    {
        ClincDbContext db;
        public PatientDataRepository(ClincDbContext _db)
        {
            db = _db;
        }
        public void Add(PatientData entity)
        {
            db.PatientDatas.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var patients = FindById(id);
            db.PatientDatas.Remove(patients);
            db.SaveChanges();
        }

        public PatientData FindById(int id)
        {
            var patients = db.PatientDatas.SingleOrDefault(b => b.Id == id);
            return patients;
        }

        public IList<PatientData> List()
        {
            return db.PatientDatas.ToList();
        }

        public void Update(int id, PatientData newPatient)
        {
            db.Update(newPatient);
            db.SaveChanges();
        }
        public List<PatientData> Search(string searchString)
        {
            return db.PatientDatas.Where(b => b.PatientName.Contains(searchString)).ToList();
        }
    }
}
