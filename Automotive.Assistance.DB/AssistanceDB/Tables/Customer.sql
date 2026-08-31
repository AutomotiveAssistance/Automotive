CREATE TABLE [dbo].[Customer]
(
    CustomerId              INT IDENTITY(1,1) PRIMARY KEY,

    UserId                  INT NOT NULL UNIQUE,

    CustomerCode            NVARCHAR(50) NULL,

    DateOfBirth             DATE NULL,
    Gender                  NVARCHAR(20) NULL,

    EmergencyContactName    NVARCHAR(150) NULL,
    EmergencyContactNumber  NVARCHAR(20) NULL,

    ProfileImageUrl         NVARCHAR(500) NULL,

    IsActive                BIT NOT NULL DEFAULT 1,

    CreatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy               NVARCHAR(50) NULL,
    UpdatedAt               DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy               NVARCHAR(50) NULL,

    CONSTRAINT FK_Customer_User
        FOREIGN KEY (UserId)
        REFERENCES [dbo].[User](UserId),

    CONSTRAINT UQ_Customer_CustomerCode
        UNIQUE (CustomerCode)
);