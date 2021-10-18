DELETE CC
	FROM Clients C
	JOIN ClientsCigars CC ON CC.ClientId = C.Id
	JOIN Addresses A ON A.Id = C.AddressId
	WHERE Country LIKE 'C%'

DELETE C
	FROM Clients C
	JOIN Addresses A ON A.Id = C.AddressId
	WHERE Country LIKE 'C%'

DELETE Addresses
	WHERE Country LIKE 'C%'