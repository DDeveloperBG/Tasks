USE [Geography]

SELECT TOP(5) c.CountryName AS Country,	
	ISNULL(p.PeakName, '(no highest peak)') AS [Highest Peak Name],
	ISNULL(p.Elevation, 0) AS [Highest Peak Elevation],
	ISNULL(p.MountainRange, '(no mountain)') AS Mountain
FROM Countries c
LEFT JOIN (
	SELECT pk.PeakName, 
		pk.Elevation,
		m.MountainRange,
		mc.CountryCode,
		RANK() OVER (PARTITION BY mc.CountryCode ORDER BY pk.Elevation DESC) AS RankByHeight
	FROM Peaks pk
	JOIN Mountains m ON m.Id = pk.MountainId
	JOIN MountainsCountries mc ON mc.MountainId = pk.MountainId
) AS p ON p.CountryCode = c.CountryCode
WHERE p.RankByHeight = 1 OR p.CountryCode IS NULL
ORDER BY c.CountryName, p.PeakName