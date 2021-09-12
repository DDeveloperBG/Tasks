ALTER PROC usp_DepositMoney(
	@accountId INT,
	@moneyAmount MONEY
) 
AS
BEGIN TRANSACTION
	IF NOT EXISTS(SELECT * FROM Accounts WHERE Id = @accountId)
	BEGIN
		ROLLBACK;
		THROW 50001, 'Account does not exist.', 1;
	END
		
	UPDATE Accounts
		SET Balance = Balance + @moneyAmount
		WHERE Id = @accountId
COMMIT