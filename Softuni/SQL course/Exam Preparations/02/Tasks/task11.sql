CREATE FUNCTION udf_AllUserCommits(
	@username VARCHAR(30)
)
RETURNS INT
AS
BEGIN
	DECLARE @userId INT = (SELECT Id 
								FROM Users 
								WHERE Username = @username)

	RETURN (SELECT COUNT(*) 
				FROM Commits 
				WHERE ContributorId = @userId)
END