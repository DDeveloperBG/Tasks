--POPULATE BOTH TABLES Minions AND Towns FROM Minions DATABASE WITH GIVEN EXAMPLE DATA, task 4

USE Minions

INSERT INTO Towns ([Name])
VALUES('Sofia'),
	('Plovdiv'),
	('Varna')
	
INSERT INTO Minions ([Name], Age, TownId)
VALUES('Kevin', 22, 1),
	('Bob', 15, 3),
	('Steward', 5, 2)