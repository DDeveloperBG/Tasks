SELECT TOP(5) CigarName, PriceForSingleCigar, ImageURL
	FROM Cigars C
	JOIN Sizes S ON S.Id = C.SizeId
	WHERE S.Length >= 12 AND 
		(CigarName LIKE '%ci%' OR 
			(PriceForSingleCigar > 50 AND RingRange > 2.55))
	ORDER BY CigarName ASC, PriceForSingleCigar DESC
