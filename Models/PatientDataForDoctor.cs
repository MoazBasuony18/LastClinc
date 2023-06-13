using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Models
{
    public class PatientDataForDoctor
    {
        public int Id { get; set; }

        [Required]
        
        public string Medicine { get; set; }
        
        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Complain { get; set; }

        [Required]
        public string PastHistory { get; set; }

        [Required]
        public string Exmination { get; set; }

        [Required]
        public string Investigations { get; set; }

        [Required]
        public string Treatment { get; set; }

        public PatientData PatientData { get; set; }
    }
}
