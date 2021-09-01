USE [Geography]

SELECT c.CountryCode,
	COUNT(c.CountryCode) AS MountainRanges
FROM Countries c
JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
JOIN Mountains m ON m.Id = mc.MountainId
WHERE c.CountryName IN ('United States', 'Russia', 'Bulgaria')
GROUP BY c.CountryCode