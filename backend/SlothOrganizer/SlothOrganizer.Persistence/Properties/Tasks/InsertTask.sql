INSERT INTO Tasks (DashboardId, Title, Description)
VALUES (@DashboardId, @Title, @Description)
SELECT CAST(SCOPE_IDENTITY() AS bigint)