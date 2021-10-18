SELECT LastName, 
		AVG(S.[Length]) AS CiagrLength, 
		CEILING(AVG(S.RingRange)) AS CiagrRingRange
	FROM Clients C
	JOIN ClientsCigars CC ON CC.ClientId = C.Id
	JOIN Cigars CI ON CI.Id = CC.CigarId
	JOIN Sizes S ON S.Id = CI.SizeId
	GROUP BY C.LastName
	ORDER BY CiagrLength DESC