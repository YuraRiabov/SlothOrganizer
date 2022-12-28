INSERT INTO TaskCompletions (TaskId, IsSuccessful, Start, End, LastEdited)
VALUES (@TaskId, @IsSuccessful, @Start, @End, @LastEdited)
SELECT CAST(SCOPE_IDENTITY() AS bigint)