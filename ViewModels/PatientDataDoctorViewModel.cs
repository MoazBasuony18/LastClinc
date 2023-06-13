using LastClinc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.ViewModels
{
    public class PatientDataDoctorViewModel
    {
        public int PatientDoctorId { get; set; }
        public string Medicine { get; set; }
        public string Diagnosis { get; set; }
        public string Complain { get; set; }
        public string PastHistory { get; set; }
        public string Exmination { get; set; }
        public string Investigations { get; set; }
        public string Treatment { get; set; }
        public int PatientId { get; set; }
        public List<PatientData> PatientDatas { get; set; }

    }
}
