SELECT C.ContinentName,
		SUM(CR.AreaInSqKm) AS CountriesArea,
		SUM(CAST(CR.[Population] AS BIGINT)) AS CountriesPopulation
	FROM Continents C
	JOIN Countries CR ON CR.ContinentCode = C.ContinentCode
	GROUP BY C.ContinentName
	ORDER BY CountriesPopulation DESC