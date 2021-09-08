CREATE PROC usp_DeleteEmployeesFromDepartment @departmentId INT
AS
BEGIN
	
	DECLARE @managerId INT = (SELECT ManagerId 
								FROM Departments 
								WHERE DepartmentID = @departmentId) 
		
	ALTER TABLE Departments
		ALTER COLUMN ManagerId INT

	UPDATE Departments
		SET ManagerId = NULL
		WHERE DepartmentID = @departmentId

	UPDATE Employees
		SET ManagerId = NULL
		WHERE ManagerID = @managerId

	DELETE ep
		FROM EmployeesProjects ep
		INNER JOIN Employees e ON e.EmployeeID = ep.EmployeeID
		WHERE e.DepartmentID = @departmentId

	DELETE 
		FROM Employees
		WHERE EmployeeID = @managerId

	DELETE
		FROM Employees
		WHERE DepartmentID = @departmentId

	DELETE 
		FROM Departments
		WHERE DepartmentID = @departmentId

	SELECT COUNT(*)
		FROM Employees
		WHERE DepartmentID = @departmentId
END