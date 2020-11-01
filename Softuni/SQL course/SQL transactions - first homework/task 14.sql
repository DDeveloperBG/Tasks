-- CREATE DATABASE CarRental WITH WANTED TABLES, task 14

USE [master]

CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	CategoryName NVARCHAR(100) NOT NULL,
	DailyRate REAL, 
	WeeklyRate REAL, 
	MonthlyRate REAL,
	WeekendRate REAL
)

CREATE TABLE Cars 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	PlateNumber VARCHAR(50),
	Manufacturer NVARCHAR(50) NOT NULL,
	Model NVARCHAR(50) NOT NULL, 
	CarYear SMALLINT NOT NULL,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	Doors SMALLINT NOT NULL,
	Picture IMAGE,
	Condition NVARCHAR(MAX) NOT NULL, 
	Available BIT NOT NULL
)

CREATE TABLE Employees 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL, 
	Title NVARCHAR(50) NOT NULL, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	DriverLicenceNumber NVARCHAR(50) NOT NULL, 
	FullName NVARCHAR(50) NOT NULL, 
	[Address] NVARCHAR(50) NOT NULL, 
	City NVARCHAR(50) NOT NULL, 
	ZIPCode SMALLINT NOT NULL, 
	Notes NVARCHAR(50)
)

CREATE TABLE RentalOrders
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(Id),
	CarId INT NOT NULL FOREIGN KEY REFERENCES Cars(Id),
	TankLevel REAL NOT NULL, 
	KilometrageStart INT NOT NULL, 
	KilometrageEnd INT NOT NULL,
	TotalKilometrage INT NOT NULL, 
	StartDate DATE NOT NULL, 
	EndDate DATE NOT NULL, 
	TotalDays INT NOT NULL,
	RateApplied REAL NOT NULL,
	TaxRate REAL NOT NULL, 
	OrderStatus BIT NOT NULL, 
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES ('Category 1', 1.5, 3, 5, 2),
	('Category 2', 2.5, 4, 2, 1),
	('Category 3', 3.5, 1, 4, 5)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES ('number 1', 'manufacturer 1', 'model 1', 1990, 1, 2, NULL, 'Good', 0),
	('number 2', 'manufacturer 2', 'model 2', 1992, 2, 4, NULL, 'Bad', 0),
	('number 3', 'manufacturer 3', 'model 3', 1993, 3, 2, NULL, 'Nearly Good', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES ('Employee 1', 'Employee LastName 1', 'title 1', NULL),
	('Employee 2', 'Employee LastName 2', 'title 2', NULL),
	('Employee 3', 'Employee LastName 3', 'title 3', NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes)
VALUES ('Licence number 1', 'Name 1', 'adress 1', 'city 1', 1233, NULL),
	('Licence number 2', 'Name 2', 'adress 2', 'city 2', 1232, NULL),
	('Licence number 3', 'Name 3', 'adress 3', 'city 3', 1333, NULL)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES (1, 1, 1, 10, 20, 25, 5, '2020 - 05 - 2', '2020 - 05 - 2', 1, 5, 4, 1, NULL),
	(2, 2, 2, 10, 20, 25, 5, '2020 - 05 - 2', '2020 - 05 - 2', 1, 1, 3, 1, NULL),
	(3, 3, 3, 10, 20, 25, 5, '2020 - 05 - 2', '2020 - 05 - 2', 1, 2, 2, 1, NULL)