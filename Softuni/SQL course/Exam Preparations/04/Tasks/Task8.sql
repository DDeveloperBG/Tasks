SELECT TOP(10) 
		FirstName AS [First Name], 
		LastName AS [Last Name], 
		CAST(AVG(Grade) AS DECIMAL(3, 2)) AS Grade
	FROM Students S
	JOIN StudentsExams SE ON SE.StudentId = S.Id
	GROUP BY FirstName, LastName
	ORDER BY Grade DESC, FirstName ASC, LastName ASC