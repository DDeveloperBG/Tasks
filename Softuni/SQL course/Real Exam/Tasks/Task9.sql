SELECT CONCAT(FirstName, ' ', LastName) AS FullName, 
		Country, 
		ZIP, 
		CONCAT('$', MAX(PriceForSingleCigar)) AS CigarPrice
	FROM Clients C
	JOIN Addresses A ON A.Id = C.AddressId
	JOIN ClientsCigars CC ON CC.ClientId = C.Id
	JOIN Cigars CI ON CI.Id = CC.CigarId
	WHERE NOT(ISNUMERIC(A.ZIP) = 0)
	GROUP BY C.FirstName, C.LastName, A.Country, A.ZIP
	ORDER BY FullName ASC