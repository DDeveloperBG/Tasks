CREATE PROC usp_WithdrawMoney(
	@accountId INT, 
	@moneyAmount MONEY
)
AS
BEGIN TRANSACTION
	IF NOT EXISTS(SELECT * FROM Accounts WHERE Id = @accountId)
	BEGIN
		ROLLBACK;
		THROW 50001, 'Account doesn''t exist.', 1;
	END

	IF (SELECT Balance FROM Accounts WHERE Id = @accountId) < @moneyAmount
	BEGIN
		ROLLBACK;
		THROW 50001, 'Account doesn''t exist.', 1;
	END

	UPDATE Accounts
		SET Balance = Balance - @moneyAmount
		WHERE Id = @accountId
COMMIT