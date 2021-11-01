using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Services
{
    public static class Populator
    {
        public static Doctor AddDoctor(HospitalContext db, string name, string specialty, string email, string password)
        {
            Doctor doctor = new Doctor
            {
                Name = name,
                Speciality = specialty
            };

            Authorization.SignIn(db, email, password, doctor);

            return doctor;
        }
    }
}
