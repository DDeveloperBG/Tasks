-- CREATE NEW TABLES Minions (Id, Name, Age) AND Towns(Id, Name), 
-- Ids HAVE TO BE PRIMARY KEYS IN Minions DATABASE, task 2

USE Minions

CREATE TABLE Minions 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	[Name] NVARCHAR(50) NOT NULL,
	Age SMALLINT NOT NULL
) 

CREATE TABLE Towns
(	
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	[Name] NVARCHAR(50) NOT NULL
)