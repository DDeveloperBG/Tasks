CREATE PROC usp_CancelFlights
AS
	UPDATE Flights
		SET ArrivalTime = NULL, DepartureTime = NULL
		WHERE DATEDIFF(SECOND, DepartureTime, ArrivalTime) > 0