DECLARE @AvgMind INT, 
	@AvgLuck INT, 
	@AvgSpeed INT

SELECT @AvgMind = AVG(Mind),
	@AvgLuck = AVG(Luck),
	@AvgSpeed = AVG(Speed)
	FROM Items I
	JOIN [Statistics] IST ON IST.Id = I.StatisticId

SELECT I.[Name],
		I.Price,
		I.MinLevel,
		IST.Strength,
		IST.Defence,
		IST.Speed,
		IST.Luck,
		IST.Mind
	FROM Items I
	JOIN [Statistics] IST ON IST.Id = I.StatisticId
	WHERE Mind > @AvgMind
		AND Luck > @AvgLuck
		AND Speed > @AvgSpeed
	ORDER BY I.[Name] ASC