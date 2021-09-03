USE Gringotts

SELECT SUM([Difference])
FROM (
		SELECT DepositAmount - (LEAD(DepositAmount) OVER (ORDER BY Id)) AS [Difference]
		FROM WizzardDeposits
		GROUP BY Id, DepositAmount
	) AS d
WHERE [Difference] IS NOT NULL