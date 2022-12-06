INSERT INTO RefreshTokens(UserId, Token, ExpirationTime)
VALUES (
	(SELECT TOP(1) Id FROM Users WHERE Email=@UserEmail),
	@Token,
	@ExpirationTime
)