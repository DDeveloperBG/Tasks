using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            ExecuteTask("Server = .; Integrated Security = true; Database = master");
        }

        static void ExecuteTask(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                CreateDB(connection);
                CreateTables(connection);
                InsertValues(connection);
            }
        }

        static void CreateDB(SqlConnection connection)
        {
            var createDB = new SqlCommand("CREATE DATABASE MinionsDB", connection);
            var useDB = new SqlCommand("USE MinionsDB", connection);

            createDB.ExecuteNonQuery();
            useDB.ExecuteNonQuery();
        }

        static void CreateTables(SqlConnection connection)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            commands.Add(new SqlCommand(
                @"CREATE TABLE EvilnessFactors(
                	Id INT IDENTITY PRIMARY KEY,
                	[Name] VARCHAR(50) NOT NULL,
                	CHECK([Name] IN ('super good', 'good', 'bad', 'evil', 'super evil'))
                )", connection));

            commands.Add(new SqlCommand(
                @"CREATE TABLE Villains(
                	Id INT IDENTITY PRIMARY KEY,
                	[Name] VARCHAR(50) NOT NULL,
                	EvilnessFactorId INT NOT NULL REFERENCES EvilnessFactors(Id)
                )", connection));

            commands.Add(new SqlCommand(
                @"CREATE TABLE Countries(
                	Id CHAR(3) PRIMARY KEY,
                	[Name] VARCHAR(50) NOT NULL
                )", connection));

            commands.Add(new SqlCommand(
                @"CREATE TABLE Towns(
                	Id INT IDENTITY PRIMARY KEY,
                	[Name] VARCHAR(50) NOT NULL,
                	-- Country Code by ISO 3166-1 alpha-3 standart
                	CountryCode CHAR(3) NOT NULL REFERENCES Countries(Id)
                )", connection));

            commands.Add(new SqlCommand(
                @"CREATE TABLE Minions(
                	Id INT IDENTITY PRIMARY KEY,
                	[Name] VARCHAR(50) NOT NULL,
                	Age TINYINT NOT NULL,
                	TownId INT NOT NULL REFERENCES Towns(Id)
                )", connection));

            commands.Add(new SqlCommand(
                @"CREATE TABLE MinionsVillains(
                	MinionId INT NOT NULL REFERENCES Minions(Id),
                	VillainId INT NOT NULL REFERENCES Villains(Id)
                )", connection));

            foreach (var command in commands)
            {
                command.ExecuteNonQuery();
            }
        }

        static void InsertValues(SqlConnection connection)
        {
            List<SqlCommand> commands = new List<SqlCommand>();

            commands.Add(new SqlCommand(
                @"INSERT INTO EvilnessFactors([Name])
	                VALUES ('super good'), 
                        ('bad'), 
                        ('good'), 
                        ('evil'), 
                        ('super evil')", connection));

            commands.Add(new SqlCommand(
                @"INSERT INTO Villains([Name], EvilnessFactorId)
	                VALUES ('bIG bRAIN', 3), 
	                	('Small Brain', 2), 
	                	('Dr. Doom', 4), 
	                	('Dark Flash', 1), 
	                	('Evil Mario', 5)", connection));

            commands.Add(new SqlCommand(
                @"INSERT INTO Countries(Id, [Name])
	                VALUES ('BGN', 'Bulgaria'), 
	                	('DMA', 'Dominica'),
	                	('KHM', 'Cambodia'),
	                	('NGA', 'Nigeria'),
	                	('PAN', 'Panama')", connection));

            commands.Add(new SqlCommand(
                @"INSERT INTO Towns([Name], CountryCode)
	                VALUES ('Something1', 'BGN'), 
	                	('Something2', 'DMA'),
	                	('Something3', 'KHM'),
	                	('Something4', 'NGA'),
	                	('Something5', 'PAN')", connection));

            commands.Add(new SqlCommand(
                @"INSERT INTO Minions([Name], Age, TownId)
	                VALUES ('Gosho', 25, 1),
	                	('Pesho', 15, 5),
	                	('Ivan', 35, 3),
	                	('Misho', 24, 2),
	                	('Olek', 22, 4)", connection));

            commands.Add(new SqlCommand(
                @"INSERT INTO MinionsVillains(MinionId, VillainId)
	                VALUES (1, 2),
	                	(2, 3),
	                	(1, 4),
	                	(3, 5),
	                	(5, 1)", connection));

            foreach (var command in commands)
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
