CREATE TABLE [dbo].[Vehicle]
(
    VehicleId               INT IDENTITY(1,1) PRIMARY KEY,

    CustomerId              INT NOT NULL,

    RegistrationNumber      NVARCHAR(50) NOT NULL,

    VehicleType             NVARCHAR(50) NULL,
    Make                    NVARCHAR(100) NULL,
    Model                   NVARCHAR(100) NULL,
    Variant                 NVARCHAR(100) NULL,

    ManufacturingYear       INT NULL,
    FuelType                NVARCHAR(50) NULL,

    VINNumber               NVARCHAR(100) NULL,
    EngineNumber            NVARCHAR(100) NULL,

    Color                   NVARCHAR(50) NULL,

    IsActive                BIT NOT NULL DEFAULT 1,

    CreatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy               NVARCHAR(50) NULL,
    UpdatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy               NVARCHAR(50) NULL,

    CONSTRAINT FK_Vehicle_Customer
        FOREIGN KEY (CustomerId)
        REFERENCES [dbo].[Customer](CustomerId),

    CONSTRAINT UQ_Vehicle_RegistrationNumber
        UNIQUE (RegistrationNumber)
);