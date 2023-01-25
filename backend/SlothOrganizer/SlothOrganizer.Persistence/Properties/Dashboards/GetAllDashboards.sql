SELECT * FROM Dashboards 
WHERE EXISTS (SELECT * FROM Users WHERE Email = @Email AND Users.Id = Dashboards.UserId)