using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;
using System;

namespace P03_SalesDatabase
{
    class Program
    {
        static void Main()
        {
            var db = new SalesContext();

            db.Database.Migrate();
        }
    }
}
