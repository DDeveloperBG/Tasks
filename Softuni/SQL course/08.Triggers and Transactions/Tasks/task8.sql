CREATE PROC usp_AssignProject(
	@emloyeeId INT, 
	@projectID INT
)
AS
BEGIN TRANSACTION
	IF (SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId) > 2
	BEGIN
		ROLLBACK;
		THROW 50001, 'The employee has too many projects!', 1
	END

	INSERT INTO EmployeesProjects(EmployeeID, ProjectID)
		VALUES (@emloyeeId, @projectID)
COMMIT