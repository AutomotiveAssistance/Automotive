CREATE TABLE [dbo].[CustomerAddress]
(
    CustomerAddressId       INT IDENTITY(1,1) PRIMARY KEY,

    CustomerId              INT NOT NULL,

    AddressType             NVARCHAR(50) NULL,
    AddressLine1            NVARCHAR(250) NOT NULL,
    AddressLine2            NVARCHAR(250) NULL,

    City                    NVARCHAR(100) NULL,
    State                   NVARCHAR(100) NULL,
    Country                 NVARCHAR(100) NULL,
    Pincode                 NVARCHAR(20) NULL,

    Latitude                DECIMAL(10,7) NULL,
    Longitude               DECIMAL(10,7) NULL,

    IsDefault               BIT NOT NULL DEFAULT 0,
    IsActive                BIT NOT NULL DEFAULT 1,

    CreatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy               NVARCHAR(50) NULL,
    UpdatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy               NVARCHAR(50) NULL,

    CONSTRAINT FK_CustomerAddress_Customer
        FOREIGN KEY (CustomerId)
        REFERENCES [dbo].[Customer](CustomerId)
);