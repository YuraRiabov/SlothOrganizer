UPDATE TaskCompletions
SET  
	IsSuccessful = @IsSuccessful,
	[Start] = @Start,
	[End] = @End,
	LastEdited = SYSDATETIMEOFFSET()
WHERE Id=@Id
SELECT TOP 1 * FROM TaskCompletions
WHERE Id=@Id