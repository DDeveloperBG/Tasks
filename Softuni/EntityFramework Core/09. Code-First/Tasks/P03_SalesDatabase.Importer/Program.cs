using System;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase.Importer
{
    class Program
    {
        static void Main()
        {
            SalesContext db = new SalesContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            ImportRandomData.To(db, 50);
        }
    }
}
