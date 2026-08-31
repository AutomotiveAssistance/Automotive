CREATE TABLE [dbo].[WorkflowType]
(
    WorkflowTypeId      INT IDENTITY(1,1) PRIMARY KEY,

    WorkflowTypeName    NVARCHAR(100) NOT NULL,
    WorkflowTypeCode    NVARCHAR(50) NOT NULL,

    Description         NVARCHAR(500) NULL,

    IsActive            BIT NOT NULL DEFAULT 1,

    CreatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy           NVARCHAR(50) NULL,
    UpdatedAt           DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy           NVARCHAR(50) NULL,

    CONSTRAINT UQ_WorkflowType_Name UNIQUE (WorkflowTypeName),
    CONSTRAINT UQ_WorkflowType_Code UNIQUE (WorkflowTypeCode)
);