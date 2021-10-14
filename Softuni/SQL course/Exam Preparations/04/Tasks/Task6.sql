SELECT S.FirstName, S.LastName, COUNT(ST.TeacherId) AS TeachersCount
	FROM Students S
	LEFT JOIN StudentsTeachers ST ON ST.StudentId = S.Id
	GROUP BY S.FirstName, S.LastName