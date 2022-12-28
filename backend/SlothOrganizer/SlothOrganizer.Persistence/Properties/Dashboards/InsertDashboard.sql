IF EXISTS (SELECT * FROM Users WHERE Id = @UserId)
	INSERT INTO Dashboards (UserId, Title)
	VALUES (@UserId, @Title)
	SELECT CAST(SCOPE_IDENTITY() AS bigint)