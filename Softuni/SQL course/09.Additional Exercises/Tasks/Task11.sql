SELECT CUR.CurrencyCode,
		CUR.[Description] AS Currency,
		COUNT(C.CountryCode) AS NumberOfCountries
	FROM Currencies CUR
	LEFT JOIN Countries C ON C.CurrencyCode = CUR.CurrencyCode
	GROUP BY CUR.CurrencyCode, CUR.[Description]
	ORDER BY NumberOfCountries DESC,
			Currency ASC