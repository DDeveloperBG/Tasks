SELECT g.[Name] AS Game, 
		gt.[Name] AS [Game Type], 
		u.Username AS Username, 
		ug.[Level] AS [Level], 
		ug.Cash AS Cash, 
		ch.[Name] AS [Character]
	FROM Games g
	JOIN GameTypes gt ON gt.Id = g.GameTypeId
	JOIN UsersGames ug ON ug.GameId = g.Id	
	JOIN Characters ch ON ch.Id = ug.CharacterId
	JOIN Users u ON u.Id = ug.UserId
	ORDER BY ug.[Level] DESC,
		u.Username ASC, 
		g.[Name] ASC