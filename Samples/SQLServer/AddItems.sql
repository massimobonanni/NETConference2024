DECLARE @i INT = 0;
WHILE @i < 10
BEGIN
    INSERT INTO [dbo].[ToDo] (Id, DueDate, ToDo, Details)
    VALUES (
        ABS(CHECKSUM(NewId())), -- Random Id
        DATEADD(day, (ABS(CHECKSUM(NEWID())) % 365), GETDATE()), -- Random DueDate within next year
        'ToDo ' + CAST(@i AS NVARCHAR(10)), -- ToDo
        'Details for ToDo ' + CAST(@i AS NVARCHAR(10)) -- Details
    )
    SET @i = @i + 1;
END
