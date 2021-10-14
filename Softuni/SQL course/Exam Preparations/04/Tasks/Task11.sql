CREATE FUNCTION udf_ExamGradesToUpdate(
	@StudentId INT, 
	@Grade DECIMAL(3, 2)
)
RETURNS VARCHAR(MAX)
BEGIN
	DECLARE @StudentName VARCHAR(MAX)
	SET @StudentName = (SELECT FirstName 
							FROM Students 
							WHERE Id = @StudentId)
	IF @StudentName IS NULL
		RETURN 'The student with provided id does not exist in the school!'

	IF @Grade > 6
		RETURN 'Grade cannot be above 6.00!'

	DECLARE @GradesCount INT
	SET @GradesCount = (SELECT COUNT(*)
							FROM StudentsExams
							WHERE StudentId = @StudentId AND 
								Grade BETWEEN @Grade AND @Grade + 0.5)

	RETURN CONCAT('You have to update ', 
		@GradesCount, 
		' grades for the student ', 
		@StudentName)
END