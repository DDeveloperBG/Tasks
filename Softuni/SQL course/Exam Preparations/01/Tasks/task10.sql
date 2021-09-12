SELECT p.PartId,
		p.[Description],
		pn.Quantity AS [Required],
		p.StockQty AS [In Stock],
		pn.Quantity - p.StockQty - 1 AS Ordered
	FROM Jobs j
	JOIN PartsNeeded pn ON pn.JobId = j.JobId
	JOIN Parts p ON p.PartId = pn.PartId
	WHERE NOT(j.[Status] = 'Finished') AND pn.Quantity > p.StockQty