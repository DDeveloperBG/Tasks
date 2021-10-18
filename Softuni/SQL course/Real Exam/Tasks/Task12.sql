ALTER PROC usp_SearchByTaste(
	@taste VARCHAR(20)
)
AS
	SELECT CigarName, 
			CONCAT('$', PriceForSingleCigar) AS Price, 
			TasteType, 
			BrandName, 
			CONCAT([Length], ' cm') AS CigarLength, 
			CONCAT(RingRange, ' cm') AS CigarRingRange
		FROM Tastes T
		JOIN Cigars C ON C.TastId = T.Id
		JOIN Brands B ON B.Id = C.BrandId
		JOIN Sizes S ON S.Id = C.SizeId
		WHERE T.TasteType = @taste
		ORDER BY CigarLength ASC, CigarRingRange DESC