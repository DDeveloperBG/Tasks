CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber 
	@LIMIT DECIMAL(18, 4)
AS
	SELECT FirstName AS [First Name], 
			LastName AS [Last Name] 
		FROM Employees	
		WHERE Salary >= @LIMIT