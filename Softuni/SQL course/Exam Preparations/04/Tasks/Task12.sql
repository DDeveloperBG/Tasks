CREATE PROC usp_ExcludeFromSchool(
	@StudentId INT
)
AS
	IF NOT EXISTS(SELECT * FROM Students WHERE Id = @StudentId)
		THROW 51000, 'This school has no student with the provided id!', 1

	DELETE
		FROM StudentsExams
		WHERE StudentId = @StudentId

	DELETE
		FROM StudentsSubjects
		WHERE StudentId = @StudentId

	DELETE
		FROM StudentsTeachers
		WHERE StudentId = @StudentId

	DELETE
		FROM Students
		WHERE Id = @StudentId