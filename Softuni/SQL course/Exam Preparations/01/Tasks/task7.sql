SELECT m.Mechanic, 
		AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) AS [Average Days]
	FROM Jobs j
	JOIN (SELECT MechanicId,
			CONCAT(FirstName, ' ', LastName) AS Mechanic
			FROM Mechanics) m ON m.MechanicId = j.MechanicId
	WHERE [Status] = 'Finished'
	GROUP BY j.MechanicId, m.Mechanic
	ORDER BY j.MechanicId