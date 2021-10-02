--USE SoftUni

SELECT TOP(10) e.FirstName,
		   e.LastName,
		   e.DepartmentID
	FROM Employees e
	JOIN (
			SELECT DepartmentID, AVG(Salary) AS AvgSalary
				FROM Employees
				GROUP BY DepartmentID
		) d ON d.DepartmentID = e.DepartmentID
	WHERE e.Salary > d.AvgSalary
	ORDER BY e.DepartmentID