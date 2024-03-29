CREATE TABLE Planes(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL,
	Seats INT NOT NULL,
	[Range] INT NOT NULL
)

CREATE TABLE Flights(
	Id INT IDENTITY PRIMARY KEY,
	DepartureTime DATETIME,
	ArrivalTime	DATETIME,
	Origin VARCHAR(50) NOT NULL,
	Destination VARCHAR(50) NOT NULL,
	PlaneId	INT NOT NULL REFERENCES Planes(Id)
)

CREATE TABLE Passengers(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	Address VARCHAR(30) NOT NULL,
	PassportId VARCHAR(11) NOT NULL
)

CREATE TABLE LuggageTypes(
	Id INT IDENTITY PRIMARY KEY,
	Type VARCHAR(30) NOT NULL,
)

CREATE TABLE Luggages(
	Id INT IDENTITY PRIMARY KEY,
	LuggageTypeId INT NOT NULL REFERENCES LuggageTypes(Id),
	PassengerId INT NOT NULL REFERENCES Passengers(Id),
)

CREATE TABLE Tickets(
	Id INT IDENTITY PRIMARY KEY,
	PassengerId INT NOT NULL REFERENCES Passengers(Id),
	FlightId INT NOT NULL REFERENCES Flights(Id),
	LuggageId INT NOT NULL REFERENCES Luggages(Id),
	Price DECIMAL(18, 2) NOT NULL
)