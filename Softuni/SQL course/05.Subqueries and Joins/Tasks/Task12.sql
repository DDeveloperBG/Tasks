--USE [Geography]

SELECT c.CountryCode,
		m.MountainRange,
		p.PeakName,
		p.Elevation
	FROM Countries c
	JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
	JOIN Mountains m ON m.Id = mc.MountainId
	JOIN Peaks p ON p.MountainId = m.Id
	WHERE c.CountryName IN ('Bulgaria') AND p.Elevation > 2835
	ORDER BY p.Elevation DESC