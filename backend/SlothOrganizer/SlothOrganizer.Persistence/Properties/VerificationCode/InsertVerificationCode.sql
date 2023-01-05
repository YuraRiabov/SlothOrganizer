INSERT INTO VerificationCodes(UserId, Code, ExpirationTime) 
VALUES ((SELECT TOP(1) Id FROM Users WHERE Email = @UserEmail), @Code, @ExpirationTime)
SELECT CAST(SCOPE_IDENTITY() as bigint)