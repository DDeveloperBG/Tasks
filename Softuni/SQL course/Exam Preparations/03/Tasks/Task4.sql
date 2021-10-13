DELETE T
	FROM Flights F
	JOIN Tickets T ON T.FlightId = F.Id
	WHERE Destination = 'Ayn Halagim'

DELETE Flights
	WHERE Destination = 'Ayn Halagim'