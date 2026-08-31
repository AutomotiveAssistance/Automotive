CREATE TABLE [dbo].[Service]
(
    ServiceId           INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName         NVARCHAR(150) NOT NULL,
    ServiceCode         NVARCHAR(50) UNIQUE NULL,
    Description         NVARCHAR(500) NULL,

    IsActive            BIT NOT NULL DEFAULT 1,

    CreatedAt           DATETIME2 DEFAULT GETUTCDATE(),
    CreatedBy           NVARCHAR(50) NULL,
    UpdatedAt           DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedBy           NVARCHAR(50) NULL
);