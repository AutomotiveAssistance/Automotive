CREATE TABLE [dbo].[PartnerType]
(
	PartnerTypeId       INT             IDENTITY(1,1) PRIMARY KEY,
    PartnerTypeName     NVARCHAR(100)   NOT NULL,
    IsActive            BIT             DEFAULT             1,
    CreatedAt           DATETIME2       DEFAULT          GETUTCDATE(),
    CreatedBy           NVARCHAR(50)    NULL,
    UpdatedAt           DATETIME2       DEFAULT          GETUTCDATE(),
    UpdatedBy           NVARCHAR(50)    NULL
)
