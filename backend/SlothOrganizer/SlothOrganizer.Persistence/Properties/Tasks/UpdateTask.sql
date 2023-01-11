﻿UPDATE TASKS
SET
	Title = @Title,
	[Description] = @Description
WHERE Id = @Id

SELECT TOP 1 * 
FROM Tasks as t
INNER JOIN TaskCompletions AS tc
ON t.Id = tc.TaskId
WHERE Id = @Id