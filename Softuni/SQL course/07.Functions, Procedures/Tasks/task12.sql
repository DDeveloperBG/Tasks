CREATE PROC usp_CalculateFutureValueForAccount 
	@accountId INT, @interestRate FLOAT
AS
	SELECT ac.Id AS [Account Id],
			ah.FirstName AS [First Name],
			ah.LastName AS [Last Name],
			ac.Balance AS [Current Balance],
			dbo.ufn_CalculateFutureValue(ac.Balance, @interestRate, 5) AS [Balance in 5 years]
		FROM Accounts ac
		JOIN AccountHolders ah ON ah.Id = ac.AccountHolderId
		WHERE ac.Id = @accountId