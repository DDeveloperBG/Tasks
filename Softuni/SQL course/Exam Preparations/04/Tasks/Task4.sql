DELETE ST
	FROM StudentsTeachers ST
	JOIN Teachers T ON T.Id = ST.TeacherId
	WHERE T.Phone LIKE '%72%'

DELETE
	FROM Teachers
	WHERE Phone LIKE '%72%'