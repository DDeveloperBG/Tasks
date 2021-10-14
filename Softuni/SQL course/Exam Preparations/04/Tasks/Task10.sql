SELECT [Name], AVG(Grade) AS AverageGrade
	FROM Subjects S
	JOIN StudentsSubjects SS ON SS.SubjectId = S.Id
	GROUP BY S.Id, [Name]
	ORDER BY S.Id