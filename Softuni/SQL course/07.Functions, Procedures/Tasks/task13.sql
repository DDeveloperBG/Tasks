CREATE FUNCTION ufn_CashInUsersGames(
	@gameName VARCHAR(50)
)
RETURNS @result TABLE (
	SumCash Money NOT NULL
)
BEGIN
	INSERT INTO @result
		SELECT SUM(ug.Cash)
			FROM UsersGames ug
			JOIN (SELECT ug.Id, 
						ROW_NUMBER() OVER (ORDER BY ug.Cash DESC) AS [Rank]
					FROM UsersGames ug
					JOIN Games g ON g.Id = ug.GameId
					WHERE g.[Name] = @gameName) r ON r.Id = ug.Id
			WHERE r.[Rank] % 2 = 1
RETURN
END