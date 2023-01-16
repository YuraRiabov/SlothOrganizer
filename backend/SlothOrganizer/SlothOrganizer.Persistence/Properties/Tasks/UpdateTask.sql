UPDATE TASKS
SET
	Title = @Title,
	[Description] = @Description
WHERE Id = @Id

SELECT * 
FROM Tasks as t
INNER JOIN TaskCompletions AS tc
ON t.Id = tc.TaskId
WHERE t.Id = @Id