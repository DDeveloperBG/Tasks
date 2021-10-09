SELECT P.PeakName,	
		M.MountainRange AS Mountain,
		P.Elevation
	FROM Peaks P
	JOIN Mountains M ON M.Id = P.MountainId
	ORDER BY P.Elevation DESC,
		P.PeakName ASC