USE SoftUni

UPDATE Employees
	SET Salary = Salary + (Salary * 0.12)
	WHERE 
		(SELECT [Name] 
			FROM Departments 
			WHERE Departments.DepartmentID = Employees.DepartmentID) 
		IN ('Engineering', 'Tool Design', 'Marketing', 'Information Services')

SELECT Salary
	FROM Employees