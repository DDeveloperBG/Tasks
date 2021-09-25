--USE SoftUni

SELECT e.EmployeeID,
		e.FirstName,
		e.LastName,
		d.[Name] AS DepartmentName
	FROM Employees e
	JOIN Departments d ON d.DepartmentID = e.DepartmentID 
	WHERE d.[Name] = 'Sales'
	ORDER BY EmployeeID