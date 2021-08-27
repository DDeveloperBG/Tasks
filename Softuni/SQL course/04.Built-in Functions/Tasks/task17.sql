USE Diablo;

SELECT 
	g.[Name] AS Game, 
	(CASE
	    WHEN g.[Hours] BETWEEN 0 AND 11 THEN 'Morning'
	    WHEN g.[Hours] BETWEEN 12 AND 17 THEN 'Afternoon'
	    WHEN g.[Hours] BETWEEN 18 AND 23 THEN 'Evening'
	END) AS [Part of the Day], 
	(CASE
	    WHEN g.Duration BETWEEN 0 AND 3 THEN 'Extra Short'
	    WHEN g.Duration BETWEEN 4 AND 6 THEN 'Short'
	    WHEN g.Duration > 6 THEN 'Long'
	    ELSE 'Extra Long'
	END) AS Duration 

FROM 
	(SELECT [Name], 
			DATEPART(HOUR, [Start]) AS [Hours],
			Duration
		FROM Games) AS g 

ORDER BY g.[Name], Duration