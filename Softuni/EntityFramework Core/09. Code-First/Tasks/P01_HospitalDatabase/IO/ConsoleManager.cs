using P01_HospitalDatabase.IO.Interfaces;
using System;

namespace P01_HospitalDatabase.IO
{
    public class ConsoleManager : IUIManager
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void Clean()
        {
            Console.Clear();
        }
    }
}
