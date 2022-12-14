SELECT * FROM RefreshTokens 
WHERE EXISTS(
	SELECT * FROM Users
	WHERE Users.Id=RefreshTokens.UserId
	AND Users.Email=@UserEmail
)