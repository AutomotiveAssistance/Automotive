CREATE TABLE [dbo].[VehicleInsurancePolicy]
(
    VehicleInsurancePolicyId    INT IDENTITY(1,1) PRIMARY KEY,

    VehicleId                   INT NOT NULL,

    InsuranceCompanyName        NVARCHAR(200) NOT NULL,

    PolicyNumber                NVARCHAR(100) NOT NULL,

    PolicyType                  NVARCHAR(100) NULL,

    PolicyStartDate             DATE NOT NULL,
    PolicyExpiryDate            DATE NOT NULL,

    IDVAmount                   DECIMAL(18,2) NULL,

    PremiumAmount               DECIMAL(18,2) NULL,

    PolicyDocumentUrl           NVARCHAR(500) NULL,

    IsActive                    BIT NOT NULL DEFAULT 1,

    CreatedAt                   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy                   NVARCHAR(50) NULL,
    UpdatedAt                   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy                   NVARCHAR(50) NULL,

    CONSTRAINT FK_VehicleInsurancePolicy_Vehicle
        FOREIGN KEY (VehicleId)
        REFERENCES [dbo].[Vehicle](VehicleId),

    CONSTRAINT UQ_Insurance_PolicyNumber
        UNIQUE (PolicyNumber)
);