-- CREATE DATABASE People AND IN IT CREATE TABLE People AND POPULATE IT WITH 5 EXAMPLE ROWS DATA, task 7

USE [master]

CREATE DATABASE People

USE People

CREATE TABLE People
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture IMAGE,
	Height FLOAT(2),
	[Weight] FLOAT(2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People ([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES ('Ivan', NULL, 1.80, 80.30, 'f', '1.02.2004', 'Still None'),
	('Gosho', NULL, 2.00, 80.30, 'f', '2.02.2004', 'Still None'),
	('Pesho', NULL, 1.70, 80.30, 'f', '3.02.2004', 'Still None'),
	('Tosho', NULL, 2.10, 80.30, 'f', '4.02.2004', 'Still None'),
	('Niki', NULL, 2.20, 80.30, 'f', '5.02.2004', 'Still None')