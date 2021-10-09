--1
UPDATE Countries
	SET CountryName = 'Burma'
	WHERE CountryName = 'Myanmar'

--2
DECLARE @WantedCountryCode CHAR(2) = (SELECT CountryCode FROM Countries WHERE CountryName = 'Tanzania' AND IsDeleted = 0)
	
INSERT INTO Monasteries([Name], CountryCode)
	VALUES ('Hanga Abbey', @WantedCountryCode)

--3
SET @WantedCountryCode = (SELECT CountryCode FROM Countries WHERE CountryName = 'Myanmar' AND IsDeleted = 0)

INSERT INTO Monasteries([Name], CountryCode)
	VALUES ('Myin-Tin-Daik', @WantedCountryCode)

--4
SELECT C.ContinentName,	
		CTR.CountryName,
		COUNT(M.Id) AS MonasteriesCount
	FROM Continents C
	LEFT JOIN Countries CTR ON CTR.ContinentCode = C.ContinentCode
	LEFT JOIN Monasteries M ON M.CountryCode = CTR.CountryCode
	WHERE CTR.IsDeleted = 0
	GROUP BY C.ContinentName, CTR.CountryName
	ORDER BY MonasteriesCount DESC, CountryName ASC