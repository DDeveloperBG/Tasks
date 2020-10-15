--SHOW ONLY SOME OF THE COLUMNS FROM SoftUni DATABASE, task 21

USE SoftUni

SELECT [Name] FROM Towns 
ORDER BY [Name] ASC

SELECT [Name] FROM Departments
ORDER BY [Name] ASC

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC