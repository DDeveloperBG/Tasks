using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.IO;
using P01_HospitalDatabase.IO.Interfaces;
using P01_HospitalDatabase.Services;
using System;

namespace P01_HospitalDatabase
{
    class Program
    {
        static void Main()
        {
            var db = new HospitalContext();
            IUIManager uiManager = new ConsoleManager();

            db.Database.Migrate();

            try
            {
                Engine.Run(db, uiManager);
            }
            catch (Exception e)
            {
                uiManager.WriteLine(e.Message);
            }
        }
    }

    public static class Engine
    {
        public static void Run(HospitalContext db, IUIManager uiManager)
        {

            string options = "Options:" + Environment.NewLine +
                "Sign In: email password name speciality" + Environment.NewLine +
                "Log In: email password" + Environment.NewLine +
                "Exit";
            Doctor doctor = null;

            while (true)
            {
                uiManager.WriteLine(options);
                string[] commandParts = uiManager.ReadLine().Split(' ');

                switch (commandParts[0])
                {
                    case "Sign":
                        string email = commandParts[2];
                        string password = commandParts[3];
                        string name = commandParts[4];
                        string speciality = commandParts[5];

                        doctor = Populator.AddDoctor(db, name, speciality, email, password);

                        if (doctor == null)
                        {
                            uiManager.WriteLine("Failed to sign!!");
                        }
                        else
                        {
                            uiManager.WriteLine("Successfully signed!!");
                        }
                        break;

                    case "Log":
                        email = commandParts[2];
                        password = commandParts[3];

                        doctor = Authorization.LogIn(db, email, password);
                        if (doctor == null)
                        {
                            uiManager.WriteLine("Failed to loggin!!");
                        }
                        else
                        {
                            uiManager.WriteLine("Successfully logged!!");
                        }
                        break;

                    case "Exit": return;
                }

                uiManager.ReadLine();
                uiManager.Clean();
            }
        }
    }
}
