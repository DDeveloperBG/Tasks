--USE SoftUni

SELECT MIN(avgS.AverageSalary)
FROM (
		SELECT AVG(e.Salary) AS AverageSalary
			FROM Employees e
			JOIN Departments d ON d.DepartmentID = e.DepartmentID 
			GROUP BY d.[Name]
	) AS avgS