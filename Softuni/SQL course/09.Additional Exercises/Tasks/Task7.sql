DECLARE @WantedUser NVARCHAR(50) = 'Alex'
DECLARE @WantedGame NVARCHAR(50) = 'Edinburgh'

DECLARE @ShoppingList TABLE(
		Id INT PRIMARY KEY, 
		ItemName NVARCHAR(50) NOT NULL
	)
	
INSERT INTO @ShoppingList
	VALUES (1, 'Blackguard'), 
		(2, 'Bottomless Potion of Amplification'), 
		(3, 'Eye of Etlich (Diablo III)'),
		(4, 'Gem of Efficacious Toxin'), 
		(5, 'Golden Gorget of Leoric'), 
		(6, 'Hellfire Amulet')

BEGIN TRANSACTION
	
	DECLARE @UserId INT = (SELECT Id FROM Users WHERE Username = @WantedUser)
	DECLARE @GameId INT = (SELECT Id FROM Games WHERE [Name] = @WantedGame)
	DECLARE @UserGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @UserId AND GameId = @GameId)
	IF @UserId LIKE NULL OR 
			@GameId LIKE NULL OR 
			@UserGameId LIKE NULL
		ROLLBACK 

	DECLARE @UserMoney MONEY = (SELECT Cash FROM UsersGames WHERE Id = @UserGameId)
	
	DECLARE @Count INT = 1
	DECLARE @Limit INT = (SELECT COUNT(*) FROM @ShoppingList)
	
	WHILE @Count <= @Limit
	BEGIN
		
		DECLARE @ItemName NVARCHAR(50) = (SELECT ItemName FROM @ShoppingList WHERE Id = @Count)
		SET @Count = @Count + 1

		DECLARE @ItemId INT = (SELECT Id FROM Items WHERE [Name] = @ItemName)
		IF @ItemId LIKE NULL
			CONTINUE 

		IF EXISTS(SELECT * FROM UserGameItems WHERE UserGameId = @UserGameId AND ItemId = @ItemId)
			CONTINUE
		
		--CHECK IF ITEM IS FORBIDDEN FOR THAT GAME
		DECLARE @GameTypeId INT = (SELECT GameTypeId FROM Games WHERE Id = @UserGameId)
		IF EXISTS(SELECT * FROM GameTypeForbiddenItems WHERE GameTypeId = @GameTypeId AND ItemId = @ItemId)
			CONTINUE

		DECLARE @ItemPrice MONEY = (SELECT Price FROM Items WHERE Id = @ItemId)
		IF @UserMoney < @ItemPrice
			CONTINUE

		UPDATE UsersGames
			SET Cash = Cash - @ItemPrice
			WHERE Id = @UserGameId

		INSERT INTO UserGameItems(UserGameId, ItemId)
			VALUES (@UserGameId, @ItemId)
	END
COMMIT

SELECT U.Username,
		G.[Name],
		UG.Cash,
		I.[Name] AS [Item Name]
	FROM Users U
	JOIN UsersGames UG ON UG.UserId = U.Id
	JOIN Games G ON G.Id = UG.GameId
	JOIN UserGameItems UGI ON UGI.UserGameId = UG.Id
	JOIN Items I ON I.Id = UGI.ItemId
	WHERE G.[Name] = @WantedGame
	ORDER BY I.[Name]