SELECT C.CountryName,	
		CN.ContinentName,	
		COUNT(R.[Length]) AS RiversCount,
		ISNULL(SUM(R.[Length]), 0) AS TotalLength
	FROM Countries C
	JOIN Continents CN ON CN.ContinentCode = C.ContinentCode
	
	LEFT JOIN CountriesRivers CR ON CR.CountryCode = C.CountryCode
	LEFT JOIN Rivers R ON R.Id = CR.RiverId

	GROUP BY C.CountryName, CN.ContinentName
	ORDER BY RiversCount DESC, 
			TotalLength DESC,
			C.CountryName ASC