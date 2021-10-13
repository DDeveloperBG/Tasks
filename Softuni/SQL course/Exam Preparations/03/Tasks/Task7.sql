SELECT CONCAT(P.FirstName, ' ', P.LastName) AS [Full Name], 
		Origin, 
		Destination
	FROM Tickets T
	JOIN Flights F ON F.Id = T.FlightId
	JOIN Passengers P ON P.Id = T.PassengerId
	ORDER BY [Full Name] ASC, 
			Origin ASC, 
			Destination ASC