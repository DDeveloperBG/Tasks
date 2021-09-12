SELECT CONCAT(m.FirstName, ' ', m.LastName) AS Available
	FROM Mechanics m
	LEFT JOIN Jobs j ON j.MechanicId = m.MechanicId 
		AND NOT(j.[Status] = 'Finished') 
	WHERE j.IssueDate IS NULL
	ORDER BY m.MechanicId