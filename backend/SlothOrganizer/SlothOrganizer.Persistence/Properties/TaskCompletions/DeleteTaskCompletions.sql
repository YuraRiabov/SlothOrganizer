﻿DELETE FROM TaskCompletions
WHERE TaskId = @TaskId AND [End] > @EndLimit