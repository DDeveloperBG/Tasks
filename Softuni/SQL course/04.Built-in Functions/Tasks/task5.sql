--USE SoftUni;

-- SOLUTION 1:
CREATE VIEW TakeTownsNamesAndLength AS
	SELECT [Name], LEN([Name]) AS NameLen
	FROM Towns;

GO

SELECT t.[Name]
FROM TakeTownsNamesAndLength AS t
WHERE t.NameLen = 5 OR t.NameLen = 6
ORDER BY t.[Name];

--SOLUTION 2:
SELECT t.[Name]
FROM (SELECT [Name], LEN([Name]) AS NameLen
		FROM Towns) AS t
WHERE t.NameLen = 5 OR t.NameLen = 6
ORDER BY t.[Name];