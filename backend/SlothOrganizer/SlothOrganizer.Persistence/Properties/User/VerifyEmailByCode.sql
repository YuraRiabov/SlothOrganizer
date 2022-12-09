UPDATE Users 
SET EmailVerified=1
WHERE Email=@Email AND EXISTS 
	(SELECT * FROM VerificationCodes AS vc WHERE 
		vc.UserId=Users.Id AND
		vc.Code=@Code AND
		vc.ExpirationTime>GETDATE())
IF @@ROWCOUNT > 0
	SELECT TOP(1) * FROM Users WHERE Email=@Email