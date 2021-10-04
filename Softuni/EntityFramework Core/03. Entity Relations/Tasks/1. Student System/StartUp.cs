using P01_StudentSystem.Data;

namespace P01_StudentSystem 
{
    class StartUp
    {
        static void Main()
        {
            var db = new StudentSystemContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
