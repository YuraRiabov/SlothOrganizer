INSERT INTO VerificationCodes(UserId, Code, ExpirationTime) 
VALUES (@UserId, @Code, @ExpirationTime)
SELECT CAST(SCOPE_IDENTITY() as bigint)