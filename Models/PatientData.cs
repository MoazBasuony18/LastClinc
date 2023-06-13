using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Models
{
    public class PatientData
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("الاسم")]
        public string PatientName { get; set; }

        [Required]
        [DisplayName("النوع")]
        public string PatientGender { get; set; }

        [Required]
        [DisplayName("العنوان")]
        public string Address { get; set; }

        [DisplayName("رقم الهاتف")]
        [Required]
        [Phone]
        public string PatientPhone { get; set; }

        [DisplayName("العمر")]
        [Required]
        public string PatientAge { get; set; }

        [Required]
        [DisplayName("متزوج ؟")]
        public bool MaritalStatus { get; set; }

        [Required]
        [DisplayName("المهنة ")]
        public string Occupation { get; set; }

        [Required]
        [DisplayName(" العادات الروتينية")]
        public string SpecialHabbit { get; set; }
    }
}
