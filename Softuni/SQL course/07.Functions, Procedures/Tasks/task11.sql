CREATE FUNCTION ufn_CalculateFutureValue 
	(@sum MONEY, @yearlyInterestRate FLOAT, @years INT)
RETURNS MONEY
BEGIN
	RETURN @sum * POWER(1 + @yearlyInterestRate, @years)
END