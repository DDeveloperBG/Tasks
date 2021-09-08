CREATE FUNCTION ufn_GetSalaryLevel (@Salary DECIMAL(18, 4))
RETURNS VARCHAR(10)
BEGIN
	RETURN (CASE
			WHEN @Salary < 30000 THEN 'Low'
			WHEN @Salary <= 50000 THEN 'Average'
			ELSE 'High' 
		END)
END