CREATE PROC usp_SearchForFiles(
	@fileExtension VARCHAR(15)
)
AS
	SELECT Id, 
			[Name],
			CONCAT(Size, 'KB') AS Size
		FROM Files
		WHERE [Name] LIKE CONCAT('%.', @fileExtension)
		ORDER BY Id ASC, [Name] ASC, Size DESC