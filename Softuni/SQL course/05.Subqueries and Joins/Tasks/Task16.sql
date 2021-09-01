USE [Geography]

SELECT COUNT(c.CountryCode)
FROM Countries c
LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL
GROUP BY mc.MountainId