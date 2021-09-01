USE SoftUni

SELECT TOP(5) e.EmployeeID,
	e.FirstName,
	p.[Name] AS ProjectName
FROM Employees e
INNER JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
INNER JOIN Projects p ON p.ProjectID = ep.ProjectID 
WHERE p.StartDate > '2002-08-13' AND p.EndDate IS NULL
ORDER BY EmployeeID