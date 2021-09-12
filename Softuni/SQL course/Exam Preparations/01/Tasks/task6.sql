SELECT CONCAT(FirstName, ' ', LastName) AS Client, 
		DATEDIFF(DAY, j.IssueDate, '2017-April-24') AS [Days going], 
		j.[Status]
	FROM Clients c
	JOIN Jobs j ON j.ClientId = c.ClientId
	WHERE NOT(j.[Status] = 'Finished')
	ORDER BY [Days going] DESC, c.ClientId