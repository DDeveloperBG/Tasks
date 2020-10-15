-- CREATE DATABASE Hotel WITH WANTED TABLES, task 14

USE [master]

CREATE DATABASE Hotel

USE Hotel

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
	AccountNumber INT NOT NULL IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL, 
	LastName NVARCHAR(50) NOT NULL,
	PhoneNumber CHAR(10),
	EmergencyName NVARCHAR(50) NOT NULL,
	EmergencyNumber CHAR(10) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus
(
	RoomStatus INT NOT NULL IDENTITY PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes
(
	RoomType INT NOT NULL IDENTITY PRIMARY KEY, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes
(
	BedType INT NOT NULL IDENTITY PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms
(
	RoomNumber INT NOT NULL IDENTITY PRIMARY KEY,
	RoomType INT NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomType), 
	BedType INT NOT NULL FOREIGN KEY REFERENCES BedTypes(BedType), 
	Rate REAL NOT NULL,
	RoomStatus REAL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Payments 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id), 
	PaymentDate DATE NOT NULL, 
	AccountNumber INT NOT NULL FOREIGN KEY REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL, 
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL(10, 2) NOT NULL,
	TaxRate DECIMAL(10, 2) NOT NULL, 
	TaxAmount DECIMAL(10, 2) NOT NULL,
	PaymentTotal DECIMAL(10, 2) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Occupancies 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY, 
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id), 
	DateOccupied DATE NOT NULL, 
	AccountNumber INT NOT NULL FOREIGN KEY REFERENCES Customers(AccountNumber), 
	RoomNumber INT NOT NULL FOREIGN KEY REFERENCES Rooms(RoomNumber), 
	RateApplied REAL NOT NULL, 
	PhoneCharge CHAR(10) NOT NULL, 
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES ('N 1', 'L 1', 'T 1', NULL),
	('N 2', 'L 2', 'T 2', NULL),
	('N 3', 'L 3', 'T 3', NULL)

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
VALUES ('N 1', 'L 1', 'PN 1', 'EM NAME 1', 'EM NUMB 1', NULL),
	('N 2', 'L 2', 'PN 2', 'EM NAME 2', 'EM NUMB 2', NULL),
	('N 3', 'L 3', 'PN 3', 'EM NAME 3', 'EM NUMB 3', NULL)

INSERT INTO RoomStatus (Notes)
VALUES (NULL),
	(NULL),
	(NULL)

INSERT INTO RoomTypes (Notes)
VALUES (NULL),
	(NULL),
	(NULL)

INSERT INTO BedTypes (Notes)
VALUES (NULL),
	(NULL),
	(NULL)

INSERT INTO	Rooms (RoomType, BedType, Rate, RoomStatus, Notes)
VALUES (1, 1, 5, 1, NULL),
	(2, 2, 2, 2, NULL),
	(3, 3, 3, 3, NULL)

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal, Notes)
VALUES (1, '2020 - 05 - 03', 1, '2020 - 05 - 04', '2020 - 05 - 08', 4, 5000.0, 0.20, 1000.0, 6000.0, NULL),
	(1, '2020 - 05 - 01', 1, '2020 - 05 - 04', '2020 - 05 - 08', 4, 5000.0, 0.20, 1000.0, 6000.0, NULL),
	(1, '2020 - 05 - 02', 1, '2020 - 05 - 05', '2020 - 05 - 09', 4, 5000.0, 0.20, 1000.0, 6000.0, NULL)

INSERT INTO  Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes)
VALUES (1, '2020 - 05 - 04', 1, 1, 0.2, 'Phone Num1', NULL),
	(2, '2020 - 05 - 04', 2, 2, 0.2, 'Phone Num2', NULL),
	(3, '2020 - 05 - 05', 3, 3, 0.2, 'Phone Num3', NULL)