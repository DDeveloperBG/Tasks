-- SELECT ALL RECORDS FROM Towns, Departments AND Employees AND ORDER THEM BY CRITERIAS FROM SoftUni DATABASE, task 20

USE SoftUni

SELECT * FROM Towns 
ORDER BY [Name] ASC

SELECT * FROM Departments
ORDER BY [Name] ASC

SELECT * FROM Employees
ORDER BY Salary DESC