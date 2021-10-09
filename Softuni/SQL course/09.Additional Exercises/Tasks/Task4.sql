SELECT U.Username,
		G.[Name] AS Game,	
		MAX(CH.[Name]) AS [Character],
		SUM(IST.Strength) + MAX(CHST.Strength) + MAX(GST.Strength) AS Strength,
		SUM(IST.Defence) + MAX(CHST.Defence) + MAX(GST.Defence) AS Defence,
		SUM(IST.Speed) + MAX(CHST.Speed) + MAX(GST.Speed) AS Speed,
		SUM(IST.Mind) + MAX(CHST.Mind) + MAX(GST.Mind) AS Mind,
		SUM(IST.Luck) + MAX(CHST.Luck) + MAX(GST.Luck) AS Luck
	FROM Users U
	JOIN UsersGames UG ON UG.UserId = U.Id
	
	JOIN Characters CH ON CH.Id = UG.CharacterId
	JOIN [Statistics] CHST ON CHST.Id = CH.StatisticId	

	JOIN Games G ON G.Id = UG.GameId	
	JOIN GameTypes GT ON GT.Id = G.GameTypeId
	JOIN [Statistics] GST ON GST.Id = GT.BonusStatsId

	JOIN UserGameItems UGI ON UGI.UserGameId = UG.Id	
	JOIN Items I ON I.Id = UGI.ItemId
	JOIN [Statistics] IST ON IST.Id = I.StatisticId

	GROUP BY U.Username, G.[Name]
	ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC