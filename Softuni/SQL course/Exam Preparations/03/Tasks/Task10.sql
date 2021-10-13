SELECT P.[Name], P.Seats, COUNT(T.Id) AS [Passengers Count]
	FROM Planes P
	LEFT JOIN Flights F ON F.PlaneId = P.Id
	LEFT JOIN Tickets T ON T.FlightId = F.Id
	GROUP BY P.[Name], P.Seats
	ORDER BY [Passengers Count] DESC,
		P.[Name] ASC,
		P.Seats ASC