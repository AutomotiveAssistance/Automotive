CREATE TABLE [dbo].[UserRole]
(
    UserRoleId          INT IDENTITY(1,1) PRIMARY KEY,

    UserId              INT NOT NULL,
    RoleId              INT NOT NULL,

    IsActive            BIT NOT NULL DEFAULT 1,

    CreatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy           NVARCHAR(50) NULL,
    UpdatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy           NVARCHAR(50) NULL,

    CONSTRAINT FK_UserRole_User        FOREIGN KEY (UserId)        REFERENCES [dbo].[User](UserId),

    CONSTRAINT FK_UserRole_Role        FOREIGN KEY (RoleId)        REFERENCES [dbo].[Role](Id),

    CONSTRAINT UQ_UserRole UNIQUE (UserId, RoleId)
);