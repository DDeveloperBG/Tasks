using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class DoctorsAuthentication
    {
        [Key]
        [Required]
        public string HashsedEmail { get; set; }

        [Required]
        public string HashsedPass { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}