-- CREATE Movies DATABASE AND WANTED TABLES, task 13

USE [master]

CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT NOT NULL FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear DATE NOT NULL,
	[Length] REAL NOT NULL,
	GenreId INT NOT NULL FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	Rating REAL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors(DirectorName, Notes)
VALUES ('Author 1', NULL),
	('Author 2', NULL),
	('Author 3', NULL),
	('Author 4', NULL),
	('Author 5', NULL)

INSERT INTO Genres(GenreName, Notes)
VALUES ('Genre 1', NULL),
	('Genre 2', NULL),
	('Genre 3', NULL),
	('Genre 4', NULL),
	('Genre 5', NULL)

INSERT INTO Categories(CategoryName, Notes)
VALUES ('Category 1', NULL),
	('Category 2', NULL),
	('Category 3', NULL),
	('Category 4', NULL),
	('Category 5', NULL)

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
VALUES ('Title 1', 1, '2001', 1.50, 2, 1, 4.5, NULL),
	('Title 2', 2, '2003', 2.50, 2, 2, 2.5, NULL),
	('Title 3', 3, '2006', 0.50, 2, 3, 3.5, NULL),
	('Title 4', 4, '1980', 1.00, 2, 4, 5.0, NULL),
	('Title 5', 4, '2000', 1.20, 2, 5, 2.5, NULL)