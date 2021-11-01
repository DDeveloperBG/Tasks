using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            Visitations = new HashSet<Visitation>();
            Diagnoses = new HashSet<Diagnose>();
            Prescriptions = new HashSet<PatientMedicament>();
        }

        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
