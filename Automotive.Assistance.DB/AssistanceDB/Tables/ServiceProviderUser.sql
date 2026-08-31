CREATE TABLE [dbo].[ServiceProviderUser]
(
    ServiceProviderUserId INT IDENTITY(1,1) PRIMARY KEY,

    ServiceProviderId     INT NOT NULL,
    UserId                INT NOT NULL,

    IsPrimaryContact      BIT NOT NULL DEFAULT 0,

    IsActive              BIT NOT NULL DEFAULT 1,

    CreatedAt             DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy             NVARCHAR(50) NULL,
    UpdatedAt             DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy             NVARCHAR(50) NULL,

    CONSTRAINT FK_ServiceProviderUser_ServiceProvider
        FOREIGN KEY (ServiceProviderId)
        REFERENCES [dbo].[ServiceProvider](ServiceProviderId),

    CONSTRAINT FK_ServiceProviderUser_User
        FOREIGN KEY (UserId)
        REFERENCES [dbo].[User](UserId),

    CONSTRAINT UQ_ServiceProviderUser UNIQUE
        (ServiceProviderId, UserId)
);