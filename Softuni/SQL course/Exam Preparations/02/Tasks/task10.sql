SELECT u.Username, AVG(f.Size) AS Size
	FROM Users u
	JOIN Commits c ON c.ContributorId = u.Id
	JOIN Files f ON f.CommitId = c.Id
	GROUP BY u.Username
	ORDER BY Size DESC, u.Username ASC