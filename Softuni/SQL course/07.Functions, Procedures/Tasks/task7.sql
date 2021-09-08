CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(50), @word VARCHAR(50))
RETURNS BIT
BEGIN
	DECLARE @oneCharacter VARCHAR(MAX) = CONCAT('[', @setOfLetters, ']')
	DECLARE @pattern VARCHAR(MAX) = REPLICATE(@oneCharacter, LEN(@word))

	IF @word LIKE @pattern
		RETURN 1

	RETURN 0
END