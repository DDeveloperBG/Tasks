-- INCREASE EMPLOYEES SALARY BY 10% FROM SoftUni DATABASE, task 22

USE SoftUni

UPDATE Employees
SET Salary += Salary * 0.10

SELECT Salary 
FROM Employees