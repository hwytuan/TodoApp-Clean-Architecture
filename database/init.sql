USE master;
GO

IF DB_ID(N'TodoAppDB') IS NULL
    EXEC(N'CREATE DATABASE [TodoAppDB]');
GO

USE TodoAppDB;
GO

IF DB_NAME() <> N'TodoAppDB'
    THROW 50001, 'Select TodoAppDB before creating TodoItems.', 1;

SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.TodoItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TodoItems
    (
        Id          int IDENTITY(1, 1) NOT NULL CONSTRAINT PK_TodoItems PRIMARY KEY,
        Title       nvarchar(200)  NOT NULL,
        Description nvarchar(1000) NULL,
        IsCompleted bit            NOT NULL CONSTRAINT DF_TodoItems_IsCompleted DEFAULT (0),
        CreatedAt   datetime2(3)   NOT NULL CONSTRAINT DF_TodoItems_CreatedAt DEFAULT (SYSUTCDATETIME()),
        DueDate     date           NULL,

        CONSTRAINT CK_TodoItems_Title_NotBlank
            CHECK (LEN(TRIM(N' ' + NCHAR(9) + NCHAR(10) + NCHAR(13) FROM Title)) > 0)
    );

    -- Seed only when the table is first created.
    INSERT INTO dbo.TodoItems (Title, Description)
    VALUES (N'Thiết kế cơ sở dữ liệu', N'Xác định các cột của bảng TodoItems.');

    INSERT INTO dbo.TodoItems (Title, IsCompleted)
    VALUES (N'Đọc yêu cầu của Todo App', 1);
END;

COMMIT TRANSACTION;
GO
