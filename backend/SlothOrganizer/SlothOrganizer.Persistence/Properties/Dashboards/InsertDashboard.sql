INSERT INTO Dashboards (UserId, Title)
VALUES (
	(SELECT TOP(1) Id FROM Users WHERE Email = @Email),
	@Title
)

SELECT * FROM Dashboards WHERE Id = CAST(SCOPE_IDENTITY() AS bigint)