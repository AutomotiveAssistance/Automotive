CREATE TABLE [dbo].[Driver]
(
    DriverId                INT IDENTITY(1,1) PRIMARY KEY,

    UserId                  INT NOT NULL UNIQUE,
    ServiceProviderId       INT NOT NULL,

    DrivingLicenseNumber    NVARCHAR(100) NULL,
    LicenseExpiryDate       DATE NULL,

    IsAvailable             BIT NOT NULL DEFAULT 1,

    CreatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy               NVARCHAR(50) NULL,
    UpdatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy               NVARCHAR(50) NULL,

    CONSTRAINT FK_Driver_User
        FOREIGN KEY (UserId)
        REFERENCES [dbo].[User](UserId),

    CONSTRAINT FK_Driver_ServiceProvider
        FOREIGN KEY (ServiceProviderId)
        REFERENCES [dbo].[ServiceProvider](ServiceProviderId)
);