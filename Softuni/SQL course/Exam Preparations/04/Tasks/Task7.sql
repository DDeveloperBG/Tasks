SELECT CONCAT(S.FirstName, ' ', S.LastName) AS [Full Name]
	FROM Students S
	LEFT JOIN StudentsExams SE ON SE.StudentId = S.Id
	WHERE SE.ExamId IS NULL
	ORDER BY [Full Name]