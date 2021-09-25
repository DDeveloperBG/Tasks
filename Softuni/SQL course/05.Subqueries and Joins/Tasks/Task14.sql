--USE [Geography]

SELECT TOP(5) c.CountryName,
		r.RiverName
	FROM Countries c
	JOIN Continents ct ON ct.ContinentCode = c.ContinentCode
	LEFT JOIN CountriesRivers cr ON cr.CountryCode = c.CountryCode 
	LEFT JOIN Rivers r ON r.Id = cr.RiverId
	WHERE ct.ContinentName IN ('Africa')
	ORDER BY CountryName