CREATE TABLE [dbo].[Role]
(
	Id                  INT             IDENTITY(1,1)    PRIMARY KEY,
    Name                NVARCHAR(50)    NOT NULL         UNIQUE,
    Code                NVARCHAR(20)    NOT NULL         UNIQUE,
    Description         NVARCHAR(200)   NULL,
    IsActive            BIT             DEFAULT             1,
    CreatedAt           DATETIME2       DEFAULT          GETUTCDATE(),
    CreatedBy           NVARCHAR(50)    NULL,
    UpdatedAt           DATETIME2       DEFAULT          GETUTCDATE(),
    UpdatedBy           NVARCHAR(50)    NULL
)
