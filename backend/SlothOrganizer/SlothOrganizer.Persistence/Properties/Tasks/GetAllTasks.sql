SELECT *
FROM Tasks as t
INNER JOIN TaskCompletions as tc
ON t.Id = tc.TaskId
WHERE t.DashboardId = @DashboardId