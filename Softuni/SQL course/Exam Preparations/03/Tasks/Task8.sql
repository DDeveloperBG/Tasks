SELECT P.FirstName AS [First Name], 
		P.LastName AS [Last Name], 
		Age
	FROM Tickets T
	RIGHT JOIN Passengers P ON P.Id = T.PassengerId
	WHERE T.Id IS NULL
	ORDER BY Age DESC, 
		FirstName ASC, 
		LastName ASC