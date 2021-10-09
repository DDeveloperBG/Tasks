SELECT U.Username,
		G.[Name] AS Game,
		COUNT(*) AS [Items Count],		
		SUM(I.Price) AS [Items Price]
	FROM Games G
	JOIN UsersGames UG ON UG.GameId = G.Id
	JOIN Users U ON U.Id = UG.UserId
	JOIN UserGameItems UGI ON UGI.UserGameId = UG.Id
	JOIN Items I ON I.Id = UGI.ItemId
	GROUP BY U.Username, G.[Name]
	HAVING COUNT(*) >= 10
	ORDER BY [Items Count] DESC,
		[Items Price] DESC,
		U.Username ASC