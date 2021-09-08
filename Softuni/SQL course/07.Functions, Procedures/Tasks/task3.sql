CREATE PROC usp_GetTownsStartingWith @StartText VARCHAR(50)
AS
	SELECT [Name] AS Town
		FROM Towns
		WHERE [Name] LIKE CONCAT(@StartText, '%')