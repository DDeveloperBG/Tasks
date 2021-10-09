SELECT P.PeakName,	
		M.MountainRange AS Mountain,	
		C.CountryName,
		CN.ContinentName
	FROM Peaks P
	JOIN Mountains M ON M.Id = P.MountainId	
	JOIN MountainsCountries MC ON MC.MountainId = M.Id	
	JOIN Countries C ON C.CountryCode = MC.CountryCode
	JOIN Continents CN ON CN.ContinentCode = C.ContinentCode
	ORDER BY P.PeakName, C.CountryName