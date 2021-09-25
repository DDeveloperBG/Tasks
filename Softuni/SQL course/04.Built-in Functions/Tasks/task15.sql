--USE Diablo

SELECT [Username], 
		RIGHT(Email, LEN(Email) - CHARINDEX('@', Email)) 
		AS [Internet Provider]
	FROM Users
	ORDER BY [Internet Provider], [Username]