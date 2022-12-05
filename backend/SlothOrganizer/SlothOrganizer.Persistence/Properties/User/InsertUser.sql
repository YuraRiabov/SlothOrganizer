INSERT INTO Users (FirstName, LastName, Email, Password, Salt, EmailVerified)
VALUES (@FirstName, @LastName, @Email, @Password, @Salt, @EmailVerified)
SELECT CAST(SCOPE_IDENTITY() as bigint)