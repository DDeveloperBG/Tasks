--USE SoftUni

-- 1
SELECT *
	INTO ResultTable
	FROM Employees
	WHERE Salary > 30000

-- 2
DELETE FROM ResultTable 
	WHERE ManagerID = 42

-- 3
UPDATE ResultTable
	SET Salary = Salary + 5000
	WHERE DepartmentID = 1

-- 4
SELECT DepartmentID, AVG(Salary) AS AverageSalary
	FROM ResultTable
	GROUP BY DepartmentID