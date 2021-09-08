CREATE FUNCTION ufn_CalculateFutureValue 
	(@sum DECIMAL(12, 4), @yearlyInterestRate FLOAT, @years INT)
RETURNS DECIMAL(12, 4)
BEGIN
	RETURN ROUND(@sum * POWER(1 + @yearlyInterestRate, @years), 4)
END