CREATE PROC usp_GetHoldersWithBalanceHigherThan @limit Money
AS
	SELECT ah.FirstName AS [First Name],
			 ah.LastName AS [Last Name]
		FROM AccountHolders ah
		JOIN Accounts ac ON ac.AccountHolderId = ah.Id
		GROUP BY ah.Id, ah.FirstName, ah.LastName
		HAVING SUM(ac.Balance) > @limit
		ORDER BY ah.FirstName, ah.LastName