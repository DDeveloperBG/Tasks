SELECT F.Id AS FlightId, 
		SUM(Price) AS Price
	FROM Flights F
	JOIN Tickets T ON T.FlightId = F.Id
	GROUP BY F.Id
	ORDER BY Price DESC, FlightId ASC