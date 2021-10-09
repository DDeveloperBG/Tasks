CREATE TABLE Monasteries(
	Id INT IDENTITY PRIMARY KEY, 
	[Name] NVARCHAR(50) NOT NULL, 
	CountryCode CHAR(2) NOT NULL REFERENCES Countries(CountryCode)
)

INSERT INTO Monasteries([Name], CountryCode) 
	VALUES ('Rila Monastery “St. Ivan of Rila”', 'BG'), 
		('Bachkovo Monastery “Virgin Mary”', 'BG'),
		('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
		('Kopan Monastery', 'NP'),
		('Thrangu Tashi Yangtse Monastery', 'NP'),
		('Shechen Tennyi Dargyeling Monastery', 'NP'),
		('Benchen Monastery', 'NP'),
		('Southern Shaolin Monastery', 'CN'),
		('Dabei Monastery', 'CN'),
		('Wa Sau Toi', 'CN'),
		('Lhunshigyia Monastery', 'CN'),
		('Rakya Monastery', 'CN'),
		('Monasteries of Meteora', 'GR'),
		('The Holy Monastery of Stavronikita', 'GR'),
		('Taung Kalat Monastery', 'MM'),
		('Pa-Auk Forest Monastery', 'MM'),
		('Taktsang Palphug Monastery', 'BT'),
		('Sümela Monastery', 'TR')

ALTER TABLE Countries
	ADD IsDeleted BIT NOT NULL DEFAULT 0

GO

UPDATE Countries
	SET IsDeleted = 1
	FROM Countries C
	JOIN (
		SELECT CT.CountryCode, COUNT(R.Id) AS RiversCount
			FROM Countries CT
			JOIN CountriesRivers CR ON CR.CountryCode = CT.CountryCode
			JOIN Rivers R ON R.Id = CR.RiverId
			GROUP BY CT.CountryCode
		) AS D ON D.CountryCode = C.CountryCode
	WHERE D.RiversCount > 3

SELECT M.[Name] AS Monastery,
		C.CountryName AS Country
	FROM Monasteries M
	JOIN Countries C ON C.CountryCode = M.CountryCode
	WHERE C.IsDeleted = 0
	ORDER BY M.[Name]