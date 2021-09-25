--USE SoftUni;

SELECT t.[Name]
	FROM (SELECT [Name], LEN([Name]) AS NameLen
			FROM Towns) AS t
	WHERE t.NameLen = 5 OR t.NameLen = 6
	ORDER BY t.[Name]