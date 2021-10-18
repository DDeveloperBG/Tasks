UPDATE C
	SET PriceForSingleCigar = PriceForSingleCigar * 1.20
	FROM Cigars C
	JOIN Tastes T ON T.Id = C.TastId
	WHERE TasteType = 'Spicy'

UPDATE Brands
	SET BrandDescription = 'New description'
	WHERE BrandDescription IS NULL