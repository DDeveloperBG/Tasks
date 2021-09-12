CREATE PROC usp_TransferMoney(
	@senderId INT,
	@receiverId INT,
	@amount MONEY
)
AS
BEGIN TRANSACTION
	EXEC usp_WithdrawMoney @senderId, @amount
	EXEC usp_DepositMoney @receiverId, @amount
COMMIT