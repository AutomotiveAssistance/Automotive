CREATE TABLE [dbo].[User]
(
    UserId              INT IDENTITY(1,1) PRIMARY KEY,

    FirstName           NVARCHAR(100) NOT NULL,
    LastName            NVARCHAR(100) NULL,

    MobileNumber        NVARCHAR(20) NOT NULL,
    Email               NVARCHAR(150) NULL,

    PasswordHash        NVARCHAR(max) NULL,
    PasswordSalt        NVARCHAR(max) NULL,

    IsActive            BIT NOT NULL DEFAULT 1,
    IsVerified          BIT NOT NULL DEFAULT 0,

    LastLoginAt         DATETIME2 NULL,

    CreatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy           NVARCHAR(50) NULL,
    UpdatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy           NVARCHAR(50) NULL,

    CONSTRAINT UQ_User_MobileNumber UNIQUE (MobileNumber),
    CONSTRAINT UQ_User_Email UNIQUE (Email)
);