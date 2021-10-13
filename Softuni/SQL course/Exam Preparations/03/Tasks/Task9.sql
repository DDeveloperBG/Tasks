SELECT CONCAT(P.FirstName, ' ', P.LastName) AS [Full Name], 
		PL.[Name] AS [Plane Name], 
		CONCAT(F.Origin, ' - ', F.Destination) AS Trip, 
		LT.[Type] AS [Luggage Type]
	FROM Tickets T
	JOIN Passengers P ON P.Id = T.PassengerId
	
	JOIN Flights F ON F.Id = T.FlightId
	JOIN Planes PL ON PL.Id = F.PlaneId

	JOIN Luggages L ON L.Id = T.LuggageId
	JOIN LuggageTypes LT ON LT.Id = L.LuggageTypeId
	ORDER BY [Full Name] ASC,
		[Plane Name] ASC,
		Origin ASC,
		Destination ASC,
		[Luggage Type] ASC