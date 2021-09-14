SELECT Id, 
		[Name], 
		CONCAT(Size, 'KB') AS Size
	FROM Files f
	WHERE NOT EXISTS(SELECT * FROM Files c WHERE c.ParentId = f.Id)
	ORDER BY f.Id ASC, f.[Name] ASC, f.Size DESC