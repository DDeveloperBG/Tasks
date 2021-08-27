CREATE TABLE Manufacturers(
	ManufacturerID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	EstablishedOn DATE NOT NULL
);

CREATE TABLE Models(
	ModelID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	ManufacturerID INT NOT NULL REFERENCES Manufacturers(ManufacturerID)
);