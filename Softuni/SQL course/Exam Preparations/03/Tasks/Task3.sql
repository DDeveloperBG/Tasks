UPDATE T
	SET Price = Price * 1.13
	FROM Flights F
	JOIN Tickets T ON T.FlightId = F.Id
	WHERE Destination = 'Carlsbad'