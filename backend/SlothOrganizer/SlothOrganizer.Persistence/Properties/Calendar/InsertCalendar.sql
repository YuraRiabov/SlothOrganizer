INSERT INTO Calendars (UserId, ConnectedCalendar, RefreshToken, Uid)
VALUES (@UserId, @ConnectedCalendar, @RefreshToken, @Uid)

SELECT CAST(SCOPE_IDENTITY() AS bigint)