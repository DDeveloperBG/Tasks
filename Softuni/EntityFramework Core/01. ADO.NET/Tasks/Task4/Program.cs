using Microsoft.Data.SqlClient;
using System;

namespace Task4
{
    class Program
    {
        static void Main()
        {
            string[] minionData = Console.ReadLine().Split();
            Minion minion = new Minion()
            {
                Name = minionData[1],
                Age = int.Parse(minionData[2]),
                TownName = minionData[3],
            };

            string[] villainData = Console.ReadLine().Split();
            Villain villain = new Villain()
            {
                Name = villainData[1],
            };

            AddMinionForVillain(minion, villain);
        }

        static void AddMinionForVillain(Minion minion, Villain villain)
        {
            string connectionString = "Server=.;Integrated Security=true;Database=MinionsDB";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction("Add minion to villain");
                int townId;

                try
                {
                    townId = GetTownIdAndIfTownNotExistsAddIt(minion.TownName, connection, transaction);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                int minionId;

                try
                {
                    minionId = GetMinionIdAndIfMinionNotExistsAddIt(minion.Name, minion.Age, townId, connection, transaction);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                int villainId;

                try
                {
                    villainId = GetVillainIdAndIfVillainNotExistsAddIt(villain.Name, connection, transaction);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                try
                {
                    InsertMinionToVillain(villain.Name, minion.Name, villainId, minionId, connection, transaction);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return;
                }

                transaction.Commit();
            }
        }

        static int GetTownIdAndIfTownNotExistsAddIt(string townName, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            var insertTownCommand = new SqlCommand(
                @"IF NOT EXISTS(SELECT * FROM Towns WHERE [Name] = @TownName)
	              BEGIN
	              	INSERT INTO Towns([Name], CountryCode)
	              		VALUES (@TownName, (SELECT TOP(1) Id FROM Countries))
	              END", connection, sqlTransaction);
            insertTownCommand.Parameters.AddWithValue("@TownName", townName);

            if(insertTownCommand.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Town {townName} was added to the database.");
            }

            var getTownIdCommand = new SqlCommand(
                @"SELECT TOP(1) Id 
                    FROM Towns 
                    WHERE [Name] = @TownName", connection, sqlTransaction);
            getTownIdCommand.Parameters.AddWithValue("@TownName", townName);

            return (int)getTownIdCommand.ExecuteScalar();
        }

        static int GetMinionIdAndIfMinionNotExistsAddIt(string minionName, int age, int townId, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            var insertMinionCommand = new SqlCommand(
                @"IF NOT EXISTS(SELECT * FROM Minions WHERE [Name] = @MinionName)
	              BEGIN
	              	INSERT INTO Minions([Name], Age, TownId)
	              		VALUES (@MinionName, @Age, @TownId)
	              END", connection, sqlTransaction);
            insertMinionCommand.Parameters.AddWithValue("@MinionName", minionName);
            insertMinionCommand.Parameters.AddWithValue("@Age", age);
            insertMinionCommand.Parameters.AddWithValue("@TownId", townId);

            insertMinionCommand.ExecuteNonQuery();

            var getMinionIdCommand = new SqlCommand(
                @"SELECT TOP(1) Id 
                    FROM Minions 
                    WHERE [Name] = @MinionName", connection, sqlTransaction);
            getMinionIdCommand.Parameters.AddWithValue("@MinionName", minionName);

            return (int)getMinionIdCommand.ExecuteScalar();
        }

        static int GetVillainIdAndIfVillainNotExistsAddIt(string villainName, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            var insertTownCommand = new SqlCommand(
                @"IF NOT EXISTS(SELECT * FROM Villains WHERE [Name] = @VillainName)
	              BEGIN
	              	INSERT INTO Villains([Name], EvilnessFactorId)
			            VALUES (@VillainName, (SELECT TOP(1) Id FROM EvilnessFactors WHERE [Name] = 'Evil'))
	              END", connection, sqlTransaction);
            insertTownCommand.Parameters.AddWithValue("@VillainName", villainName);

            if (insertTownCommand.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }

            var getTownIdCommand = new SqlCommand(
                @"SELECT TOP(1) Id 
                    FROM Villains 
                    WHERE [Name] = @VillainName", connection, sqlTransaction);
            getTownIdCommand.Parameters.AddWithValue("@VillainName", villainName);

            return (int)getTownIdCommand.ExecuteScalar();
        }
    
        static void InsertMinionToVillain(string villainName, string minionName, int villainId, int minionId, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            var insertTownCommand = new SqlCommand(
                @"INSERT INTO MinionsVillains(VillainId, MinionId)
			            VALUES (@VillainId, @MinionId)", connection, sqlTransaction);
            insertTownCommand.Parameters.AddWithValue("@VillainId", villainId);
            insertTownCommand.Parameters.AddWithValue("@MinionId", minionId);
           
            if (insertTownCommand.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                return;
            }

            throw new Exception("Coudn't add minion to villain.");
        }
    }

    class Minion
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string TownName { get; set; }
    }

    class Villain
    {
        public string Name { get; set; }
    }
}