BEGIN TRANSACTION
	DECLARE @gameId INT = (SELECT Id FROM Games WHERE [Name] = 'Safflower')
	DECLARE @userId INT = (SELECT Id FROM Users WHERE FirstName = 'Stamat')
	DECLARE @userGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId)
	
	DECLARE @wantedItems TABLE(
		Id INT IDENTITY PRIMARY KEY,
		ItemId INT,
		Price MONEY
	)
	
	INSERT INTO @wantedItems (ItemId, Price)
		SELECT Id, Price
			FROM Items
			WHERE MinLevel = 11 OR MinLevel = 12 OR MinLevel BETWEEN 19 AND 21

	DECLARE @order INT = 1
	DECLARE @itemsCount INT = (SELECT COUNT(*) FROM @wantedItems)
	WHILE (@order <= @itemsCount)
	BEGIN
		DECLARE @itemPrice MONEY = (SELECT Price FROM @wantedItems WHERE Id = @order)
	    IF (SELECT Cash FROM UsersGames WHERE Id = @userGameId) >= @itemPrice
		BEGIN
			UPDATE UsersGames
				SET Cash = Cash - @itemPrice
				WHERE Id = @userGameId

			INSERT INTO UserGameItems (UserGameId, ItemId)
				VALUES (@userGameId, (SELECT ItemId FROM @wantedItems WHERE Id = @order))
		END
		SET @order = @order + 1
	END
COMMIT

SELECT i.[Name] AS [Item Name]
	FROM UserGameItems gi
	JOIN Items i ON i.Id = gi.ItemId
	WHERE gi.UserGameId = @userGameId
	ORDER BY i.[Name]