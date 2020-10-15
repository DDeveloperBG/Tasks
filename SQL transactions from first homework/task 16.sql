-- CREATING THE NEEDED DATABASE FROM SoftUni DATABASE, task 16

CREATE DATABASE SoftUni;

CREATE TABLE Towns
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses
(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	AddressText NVARCHAR(50) NOT NULL,
	TownId INT NOT NULL FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL, 
	MiddleName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL, 
	JobTitle NVARCHAR(50) NOT NULL,
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATE NOT NULL, 
	Salary DECIMAL(9, 2),
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)