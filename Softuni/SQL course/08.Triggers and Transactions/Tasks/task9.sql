CREATE TABLE Deleted_Employees(
	EmployeeId INT PRIMARY KEY, 
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL, 
	MiddleName VARCHAR(50) NOT NULL, 
	JobTitle VARCHAR(50) NOT NULL, 
	DepartmentId INT, 
	Salary MONEY
)

GO

CREATE TRIGGER TR_Employees_AfterDelete
ON Employees AFTER DELETE
AS 
	INSERT INTO Deleted_Employees
		SELECT *
			FROM deleted