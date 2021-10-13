SELECT Id, [Name], Seats, [Range]
	FROM Planes
	WHERE [Name] LIKE '%TR%'
	ORDER BY Id ASC,
		[Name] ASC,
		Seats ASC,
		[Range] ASC