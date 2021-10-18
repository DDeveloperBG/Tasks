SELECT Id, 
		CONCAT(C.FirstName, ' ', C.LastName) AS ClientName, 
		Email
	FROM Clients C
	LEFT JOIN ClientsCigars CC ON CC.ClientId = C.Id
	WHERE CC.CigarId IS NULL
	ORDER BY ClientName ASC