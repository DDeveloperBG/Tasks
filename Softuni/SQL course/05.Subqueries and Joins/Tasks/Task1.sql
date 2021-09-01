USE SoftUni;

SELECT TOP(5) e.EmployeeId,
	   e.JobTitle,
	   e.AddressId,
	   a.AddressText
FROM Employees e
INNER JOIN Addresses a ON a.AddressID = e.AddressID
ORDER BY a.AddressId ASC;
