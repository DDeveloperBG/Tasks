USE [Geography];

WITH CTE_CountCU AS (
	SELECT ContinentCode, 
		CurrencyCode, 
		COUNT(CurrencyCode) AS CurrencyUsage
	FROM Countries
	GROUP BY ContinentCode, CurrencyCode
)
SELECT d.ContinentCode,
	d.CurrencyCode,
	d.CurrencyUsage FROM
(
	SELECT c.ContinentCode,
		cu.CurrencyCode,
		cu.CurrencyUsage,
		ROW_NUMBER() OVER(PARTITION BY c.ContinentCode ORDER BY cu.CurrencyUsage DESC) AS CurrencyRate
	FROM Continents c
	JOIN CTE_CountCU AS cu ON cu.ContinentCode = c.ContinentCode

) AS d WHERE d.CurrencyRate = 1