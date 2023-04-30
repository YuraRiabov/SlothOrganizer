SELECT * FROM VerificationCodes 
WHERE EXISTS (
	SELECT * FROM Users
	WHERE Users.Id = VerificationCodes.UserId
	AND Users.Email = @UserEmail
)