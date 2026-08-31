CREATE TABLE [dbo].[ServiceProviderService]
(
    ServiceProviderServiceId INT IDENTITY(1,1) PRIMARY KEY,

    ServiceProviderId        INT NOT NULL,
    ServiceId                INT NOT NULL,

    BasePrice                DECIMAL(18,2) NULL,
    EstimatedDurationMinutes INT NULL,

    IsAvailable              BIT NOT NULL DEFAULT 1,

    CreatedAt                DATETIME2 DEFAULT GETUTCDATE(),
    CreatedBy                NVARCHAR(50) NULL,
    UpdatedAt                DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedBy                NVARCHAR(50) NULL,

    CONSTRAINT FK_ServiceProviderService_ServiceProvider
        FOREIGN KEY (ServiceProviderId)
        REFERENCES ServiceProvider(ServiceProviderId),

    CONSTRAINT FK_ServiceProviderService_Service
        FOREIGN KEY (ServiceId)
        REFERENCES Service(ServiceId)
);