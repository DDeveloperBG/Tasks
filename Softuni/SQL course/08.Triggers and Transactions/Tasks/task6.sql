--1---------------------------------------------------------
CREATE TRIGGER TR_UserGameItems_AfterInsert
ON UserGameItems AFTER INSERT
AS
	DECLARE @itemId INT = (SELECT i.ItemId FROM inserted i)
	DECLARE @userGameId INT = (SELECT i.UserGameId FROM inserted i)

	DECLARE @minLevel INT = (SELECT MinLevel 
								FROM Items
								WHERE Id = @itemId)
	
	DECLARE @gameLevel INT = (SELECT [Level] 
								FROM UsersGames
								WHERE Id = @userGameId)

	IF @minLevel > @gameLevel
		DELETE 
			FROM UserGameItems
			WHERE ItemId = @itemId AND UserGameId = @userGameId
GO

--2---------------------------------------------------------
DECLARE @gameId INT = (SELECT Id FROM Games WHERE [Name] = 'Bali')
DECLARE @usersIds TABLE (Id INT) 

INSERT INTO @usersIds 
	SELECT Id
		FROM Users
		WHERE Username IN (
			'baleremuda', 
			'loosenoise', 
			'inguinalself', 
			'buildingdeltoid', 
			'monoxidecos'
		)

UPDATE UsersGames
	SET Cash = Cash + 50000
	WHERE GameId = @gameId AND UserId IN (SELECT Id FROM @usersIds)

--3---------------------------------------------------------
CREATE TYPE WantedUsersGames AS TABLE
(
    Id INT IDENTITY PRIMARY KEY, 
	UserGameId INT, 
	GameId INT, 
	Cash MONEY
)

GO

CREATE PROC usp_BuyItemsFromRange(
	@wantedUsersGames WantedUsersGames READONLY,
	@startId INT,
	@endId INT
)
AS
	DECLARE @userGameId INT = 1
	DECLARE @userGamesCount INT = (SELECT COUNT(*) FROM @wantedUsersGames)
	WHILE (@userGameId <= @endId)
	BEGIN
		DECLARE @itemId INT = @startId
		WHILE (@itemId <= @endId)
		BEGIN TRANSACTION
			DECLARE @itemPrice MONEY = (SELECT Price FROM Items WHERE Id = @itemId)
		    IF (SELECT Cash FROM @wantedUsersGames WHERE Id = @userGameId) >= @itemPrice
			BEGIN
				UPDATE UsersGames
					SET Cash = Cash - @itemPrice
					WHERE Id = (SELECT GameId 
									FROM @wantedUsersGames 
									WHERE Id = @userGameId)

				INSERT INTO UserGameItems (UserGameId, ItemId)
					VALUES (@userGameId, @itemId)
			END
			SET @itemId += 1
		COMMIT
		SET @userGameId += 1
	END
GO

DECLARE @gameId INT = (SELECT Id FROM Games WHERE [Name] = 'Bali')
DECLARE @usersIds TABLE (Id INT) 

INSERT INTO @usersIds 
	SELECT Id
		FROM Users
		WHERE Username IN (
			'baleremuda', 
			'loosenoise', 
			'inguinalself', 
			'buildingdeltoid', 
			'monoxidecos'
		)

DECLARE @wantedUsersGames WantedUsersGames

INSERT INTO @wantedUsersGames
	SELECT Id, GameId, Cash
		FROM UsersGames
		WHERE GameId = @gameId AND UserId IN (SELECT Id FROM @usersIds)

EXEC usp_BuyItemsFromRange @wantedUsersGames, 251, 299
EXEC usp_BuyItemsFromRange @wantedUsersGames, 501, 539

--4
SELECT u.Username, g.[Name], ug.Cash, i.[Name] AS [Item Name]
	FROM UsersGames ug
	JOIN Games g ON g.Id = ug.GameId
	JOIN Users u ON u.Id = ug.UserId
	JOIN UserGameItems gi ON gi.UserGameId = ug.Id
	JOIN Items i ON i.Id = gi.ItemId
	WHERE g.[Name] = 'Bali'
	ORDER BY u.Username, i.[Name]