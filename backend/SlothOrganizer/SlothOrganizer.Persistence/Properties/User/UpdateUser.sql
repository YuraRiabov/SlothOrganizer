UPDATE Users SET 
	FirstName=@FirstName, 
	LastName=@LastName, 
	Email=@Email,
	Password=@Password,
	Salt=@Salt, EmailVerified=@EmailVerified
	WHERE Id=@Id