CREATE TABLE [dbo].[Mechanic]
(
    MechanicId              INT IDENTITY(1,1) PRIMARY KEY,

    UserId                  INT NOT NULL UNIQUE,
    ServiceProviderId       INT NOT NULL,

    EmployeeCode            NVARCHAR(50) NULL,
    ExperienceYears         INT NULL,

    IsAvailable             BIT NOT NULL DEFAULT 1,
    IsVerified              BIT NOT NULL DEFAULT 0,

    CreatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy               NVARCHAR(50) NULL,
    UpdatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy               NVARCHAR(50) NULL,

    CONSTRAINT FK_Mechanic_User
        FOREIGN KEY (UserId)
        REFERENCES [dbo].[User](UserId),

    CONSTRAINT FK_Mechanic_ServiceProvider
        FOREIGN KEY (ServiceProviderId)
        REFERENCES [dbo].[ServiceProvider](ServiceProviderId)
);