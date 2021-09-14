SELECT TOP(5) r.Id, 
		r.[Name], 
		COUNT(*) AS Commits
	FROM Repositories r
	JOIN Commits c ON c.RepositoryId = r.Id
	GROUP BY r.Id, r.[Name]
	ORDER BY Commits DESC, r.Id ASC, r.[Name] ASC