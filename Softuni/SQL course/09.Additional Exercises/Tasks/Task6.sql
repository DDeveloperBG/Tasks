SELECT I.[Name] AS Item,	
		I.Price,	
		I.MinLevel,	
		GT.[Name] AS [Forbidden Game Type]
	FROM Items i
	LEFT JOIN GameTypeForbiddenItems GFI ON GFI.ItemId = I.Id
	LEFT JOIN GameTypes GT ON GT.Id = GFI.GameTypeId
	ORDER BY GT.[Name] DESC,
		I.[Name] ASC