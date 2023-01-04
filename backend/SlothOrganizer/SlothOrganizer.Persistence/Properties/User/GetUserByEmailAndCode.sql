SELECT TOP(1) * FROM Users
WHERE Email = @Email AND
EXISTS (
	SELECT * FROM VerificationCodes
	WHERE Code = @Code AND
	Users.Id = VerificationCodes.UserId
)