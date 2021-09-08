CREATE PROC usp_EmployeesBySalaryLevel @SalaryLevel VARCHAR(10)
AS
	SELECT FirstName AS [First Name],
			LastName AS [Last Name]
		FROM Employees
		WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel