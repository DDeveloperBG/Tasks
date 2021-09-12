--CREATE DATABASE WashingMachineService

--USE WashingMachineService

-- contains information about the customers that use the service
CREATE TABLE Clients(
	ClientId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Phone CHAR(12) NOT NULL
)

-- contains information about employees
CREATE TABLE Mechanics(
	MechanicId INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	[Address] VARCHAR(255) NOT NULL,
)

-- list of all washing machine models that the servie operates with
CREATE TABLE Models(
	ModelId	INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL UNIQUE
)

-- contains information about all machines that clients submitted for repairs
CREATE TABLE Jobs(
	JobId INT IDENTITY PRIMARY KEY,
	ModelId	INT NOT NULL REFERENCES Models(ModelId),
	[Status] VARCHAR(11) DEFAULT 'Pending',
	ClientId INT NOT NULL REFERENCES Clients(ClientId),
	MechanicId INT REFERENCES Mechanics(MechanicId),
	IssueDate DATE NOT NULL,	
	FinishDate DATE,
	CHECK ([Status] IN ('Pending', 'In Progress', 'Finished'))
)

-- contains information about orders for parts
CREATE TABLE Orders(
	OrderId	INT IDENTITY PRIMARY KEY,
	JobId INT NOT NULL REFERENCES Jobs(JobId),
	IssueDate DATE,
	Delivered BIT DEFAULT 0
)

-- list of vendors that supply parts to the service
CREATE TABLE Vendors(
	VendorId INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL UNIQUE
)

-- list of all parts the service operates with
CREATE TABLE Parts(
	PartId INT IDENTITY PRIMARY KEY,
	SerialNumber VARCHAR(50) NOT NULL UNIQUE,
	[Description] VARCHAR(255),
	Price MONEY  NOT NULL,
	VendorId INT NOT NULL REFERENCES Vendors(VendorId),
	StockQty INT DEFAULT 0,
	CHECK (NOT(Price = 0)),
	CHECK (StockQty >= 0)
)

-- mapping table between Orders and Parts with additional Quantity field
CREATE TABLE OrderParts(
	OrderId INT NOT NULL REFERENCES Orders(OrderId),
	PartId INT NOT NULL REFERENCES Parts(PartId),
	Quantity INT DEFAULT 1,
	PRIMARY KEY(OrderId, PartId),
	CHECK (Quantity > 0)
)

-- mapping table between Jobs and Parts with additional Quantity field
CREATE TABLE PartsNeeded(
	JobId INT NOT NULL REFERENCES Jobs(JobId),
	PartId INT NOT NULL REFERENCES Parts(PartId),
	Quantity INT DEFAULT 1,
	PRIMARY KEY(JobId, PartId),
	CHECK (Quantity > 0)
)