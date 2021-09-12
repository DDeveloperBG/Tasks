CREATE FUNCTION udf_GetCost(
	@jobId INT
)
RETURNS DECIMAL(12, 2)
BEGIN
	DECLARE @wholePrice DECIMAL(12, 2) = (SELECT SUM(p.Price * op.Quantity)
									FROM Orders o
									JOIN OrderParts op ON op.OrderId = o.OrderId
									JOIN Parts p ON p.PartId = op.PartId
									WHERE o.JobId = @jobId)

	IF(@wholePrice IS NULL)
		RETURN 0
	
	RETURN @wholePrice
END