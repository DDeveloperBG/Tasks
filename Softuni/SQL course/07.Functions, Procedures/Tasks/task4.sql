CREATE PROC usp_GetEmployeesFromTown @TownName VARCHAR(50)
AS
	SELECT FirstName AS [First Name],
			LastName AS	[Last Name]
		FROM Employees e
		JOIN Addresses a ON a.AddressID = e.AddressID
		JOIN Towns t ON t.TownID = a.TownID
		WHERE t.[Name] = @TownName