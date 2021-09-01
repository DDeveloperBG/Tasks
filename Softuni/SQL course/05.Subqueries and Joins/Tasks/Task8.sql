USE SoftUni

SELECT e.EmployeeID,
	e.FirstName,
	p.[Name] AS ProjectName
FROM Employees e
INNER JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
LEFT JOIN Projects p ON p.ProjectID = ep.ProjectID AND DATEPART(YEAR, p.StartDate) < 2005
WHERE e.EmployeeID = 24