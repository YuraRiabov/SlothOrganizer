UPDATE Users SET 
	AvatarUrl = @AvatarUrl
WHERE Id = @Id

SELECT * FROM Users WHERE Id = @Id