CREATE TABLE [dbo].[ServiceProviderWorkflow]
(
    ServiceProviderWorkflowId INT IDENTITY(1,1) PRIMARY KEY,

    ServiceProviderId         INT NOT NULL,
    WorkflowId                INT NOT NULL,

    IsEnabled                 BIT NOT NULL DEFAULT 1,

    ExecutionOrder            INT NULL,

    ConfigurationJson         NVARCHAR(MAX) NULL,

    IsActive                  BIT NOT NULL DEFAULT 1,

    CreatedAt                 DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy                 NVARCHAR(50) NULL,
    UpdatedAt                 DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedBy                 NVARCHAR(50) NULL,

    CONSTRAINT FK_ServiceProviderWorkflow_ServiceProvider        FOREIGN KEY (ServiceProviderId)        REFERENCES [dbo].[ServiceProvider](ServiceProviderId),
    CONSTRAINT FK_ServiceProviderWorkflow_WorkflowType        FOREIGN KEY (WorkflowId)        REFERENCES [dbo].[WorkflowType](WorkflowTypeId),

    CONSTRAINT UQ_ServiceProviderWorkflow        UNIQUE (ServiceProviderId, WorkflowId)
);