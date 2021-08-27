USE [Geography];

SELECT p.PeakName, r.RiverName, LOWER(p.PeakName + SUBSTRING(r.RiverName, 2, LEN(r.RiverName))) AS Mix
FROM (SELECT PeakName, RIGHT(PeakName, 1) AS LastLetter FROM Peaks) AS p
JOIN (SELECT RiverName, LEFT(RiverName, 1) AS FirstLetter FROM Rivers) AS r 
	ON r.FirstLetter = p.LastLetter
ORDER BY Mix;