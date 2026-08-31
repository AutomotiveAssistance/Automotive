CREATE TABLE [dbo].[ServiceProvider]
(
	ServiceProviderId       INT IDENTITY(1,1)   PRIMARY KEY,
    ServiceProviderName     NVARCHAR(200)       NOT NULL,
    ServiceProviderCode     NVARCHAR(50)        UNIQUE,
    ContactPersonName       NVARCHAR(150)       NULL,
    ContactNumber           NVARCHAR(20)        NULL,
    Email                   NVARCHAR(150)       NULL,
    GSTNumber               NVARCHAR(50)        NULL,
    RegistrationNumber      NVARCHAR(100)       NULL,
    AddressLine1            NVARCHAR(250)       NULL,
    AddressLine2            NVARCHAR(250)       NULL,
    City                    NVARCHAR(100)       NULL,
    State                   NVARCHAR(100)       NULL,
    Pincode                 NVARCHAR(20)        NULL,
    Latitude                NVARCHAR(100)       NULL,
    Longitude               NVARCHAR(100)       NULL,
    IsVerified              BIT                 NOT NULL DEFAULT 0,
    IsActive                BIT                 DEFAULT             1,
    CreatedAt               DATETIME2           DEFAULT          GETUTCDATE(),
    CreatedBy               NVARCHAR(50)        NULL,
    UpdatedAt               DATETIME2           DEFAULT          GETUTCDATE(),
    UpdatedBy               NVARCHAR(50)        NULL
)
