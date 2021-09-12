CREATE PROC usp_PlaceOrder(
	@jobId INT,
	@partSerialNumber VARCHAR(50),
	@quantity INT
)
AS
BEGIN
	IF @quantity <= 0
		THROW 50012, 'Part quantity must be more than zero!', 1
	
	IF NOT EXISTS(SELECT * FROM Jobs WHERE JobId = @jobId)
		THROW 50013, 'Job not found!', 1

	IF EXISTS(SELECT [Status] FROM Jobs WHERE JobId = @jobId AND [Status] = 'Finished')
		THROW 50011, 'This job is not active!', 1

	DECLARE @partId INT = (SELECT PartId
								FROM Parts
								WHERE SerialNumber = @partSerialNumber)
	
	IF @partId IS NULL
		THROW 50014, 'Part not found!', 1

	DECLARE @orderId INT = (SELECT OrderId 
								FROM Orders 
								WHERE JobId = @jobId AND IssueDate IS NULL)
	
	IF @orderId IS NOT NULL
	BEGIN
		IF EXISTS(SELECT PartId 
					FROM OrderParts 
					WHERE OrderId = @orderId AND PartId = @partId)
		BEGIN
			UPDATE OrderParts
				SET Quantity = Quantity + @quantity
				WHERE OrderId = @orderId AND PartId = @partId

			RETURN
		END
	END
	ELSE BEGIN
		INSERT INTO Orders (JobId, IssueDate, Delivered)
			VALUES (@jobId, NULL, 0)

		SET @orderId = (SELECT TOP(1) OrderId 
							FROM Orders 
							WHERE JobId = @jobId AND IssueDate IS NULL
							ORDER BY OrderId DESC)
	END

	INSERT INTO OrderParts (OrderId, PartId, Quantity)
			VALUES (@orderId, @partId, @quantity)
END