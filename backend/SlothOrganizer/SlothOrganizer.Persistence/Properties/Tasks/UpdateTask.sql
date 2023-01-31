UPDATE TASKS
SET
	Title = @Title,
	[Description] = @Description
WHERE Id = @Id

SELECT TOP(1) *, (
	SELECT * 
	FROM TaskCompletions
	WHERE TaskId = @Id
	FOR JSON AUTO
) AS TaskCompletions
FROM Tasks
WHERE Id = @Id