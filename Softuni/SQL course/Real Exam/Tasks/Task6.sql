SELECT C.Id, CigarName, PriceForSingleCigar, TasteType, TasteStrength
	FROM Cigars C
	JOIN Tastes T ON T.Id = C.TastId
	WHERE TasteType IN ('Earthy', 'Woody')
	ORDER BY PriceForSingleCigar DESC