CREATE FUNCTION udf_ClientWithCigars(
	@name NVARCHAR(30)
) 
RETURNS INT
BEGIN
	RETURN 
		(SELECT COUNT(*)
			FROM Clients C
			LEFT JOIN ClientsCigars CC ON CC.ClientId = C.Id
			LEFT JOIN Cigars CI ON CI.Id = CC.CigarId
			WHERE FirstName = @name)
END