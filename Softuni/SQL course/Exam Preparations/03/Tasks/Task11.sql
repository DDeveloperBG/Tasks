CREATE FUNCTION udf_CalculateTickets(
	@Origin VARCHAR(50), 
	@Destination VARCHAR(50), 
	@PeopleCount INT
)
RETURNS VARCHAR(50)
BEGIN
	IF @PeopleCount <= 0
		RETURN 'Invalid people count!'

	DECLARE @FlightId INT;
	SET @FlightId = (SELECT Id 
						FROM Flights 
						WHERE Origin = @Origin AND
							Destination = @Destination)
	IF @flightId IS NULL
		RETURN 'Invalid flight!'

	DECLARE @TicketPrice DECIMAL(18, 2); 
	SET @TicketPrice = (SELECT TOP(1) Price
							FROM Tickets
							WHERE FlightId = @flightId)

	RETURN 'Total price ' + CAST(@TicketPrice * @PeopleCount AS CHAR)
END