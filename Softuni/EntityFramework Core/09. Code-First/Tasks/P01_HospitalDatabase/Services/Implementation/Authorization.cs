using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Linq;

namespace P01_HospitalDatabase.Services
{
    public static class Authorization
    {
        public static Doctor LogIn(HospitalContext db, string email, string password)
        {
            var hashedEmail = SHA256_Encrypter.Encrypt(email);
            var hashedPass = SHA256_Encrypter.Encrypt(password);

            var doctor = db.DoctorsAuthentications
                .FirstOrDefault(x => x.HashsedEmail == hashedEmail && x.HashsedPass == hashedPass)
                ?.Doctor;

            return doctor;
        }

        public static void SignIn(HospitalContext db, string email, string password, Doctor doctor)
        {
            var hashedEmail = SHA256_Encrypter.Encrypt(email);

            var doctorAuth = db.DoctorsAuthentications
                .FirstOrDefault(x => x.HashsedEmail == hashedEmail);
            if (doctorAuth != null)
            {
                throw new ArgumentException("There is already a doctor with this email!");
            }

            db.DoctorsAuthentications.Add(new DoctorsAuthentication
            {
                HashsedEmail = hashedEmail,
                HashsedPass = SHA256_Encrypter.Encrypt(password),
                Doctor = doctor
            });

            db.SaveChanges();
        }
    }
}
