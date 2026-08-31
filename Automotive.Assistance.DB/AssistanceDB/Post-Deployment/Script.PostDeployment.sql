/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



-- MERGE script matching by PartnerTypeName instead of ID
MERGE INTO [dbo].[PartnerType] AS target
USING (
    VALUES
        ('Garage', 1, 'SYSTEM', 'SYSTEM'),
        ('Workshop', 1, 'SYSTEM', 'SYSTEM'),
        ('Roadside Assistance', 1, 'SYSTEM', 'SYSTEM'),
        ('Towing Service', 1, 'SYSTEM', 'SYSTEM'),
        ('Battery Service', 1, 'SYSTEM', 'SYSTEM'),
        ('Tyre Service', 1, 'SYSTEM', 'SYSTEM'),
        ('Fuel Delivery', 1, 'SYSTEM', 'SYSTEM'),
        ('Car Service Center', 1, 'SYSTEM', 'SYSTEM')
) AS source (PartnerTypeName, IsActive, CreatedBy, UpdatedBy)
ON target.PartnerTypeName = source.PartnerTypeName
WHEN MATCHED THEN
    UPDATE SET 
        target.IsActive = source.IsActive,
        target.UpdatedBy = source.UpdatedBy,
        target.UpdatedAt = GETUTCDATE()
WHEN NOT MATCHED THEN
    INSERT (PartnerTypeName, IsActive, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy)
    VALUES (
        source.PartnerTypeName, 
        source.IsActive, 
        GETUTCDATE(), 
        source.CreatedBy, 
        GETUTCDATE(), 
        source.UpdatedBy
    );

 -- MERGE script matching by Service
    MERGE INTO [dbo].[Service] AS target
USING (
    VALUES
        ('General Vehicle Repair', 'GENERAL_REPAIR', 'General vehicle repair and maintenance service', 1, 'SYSTEM', 'SYSTEM'),
        ('Emergency Breakdown Assistance', 'BREAKDOWN_ASSISTANCE', 'Emergency roadside breakdown assistance', 1, 'SYSTEM', 'SYSTEM'),
        ('Towing Service', 'TOWING', 'Vehicle towing service', 1, 'SYSTEM', 'SYSTEM'),
        ('Battery Jump Start', 'BATTERY_JUMPSTART', 'Jump start service for discharged battery', 1, 'SYSTEM', 'SYSTEM'),
        ('Battery Replacement', 'BATTERY_REPLACEMENT', 'Vehicle battery replacement service', 1, 'SYSTEM', 'SYSTEM'),
        ('Tyre Puncture Repair', 'TYRE_PUNCTURE', 'Tyre puncture repair service', 1, 'SYSTEM', 'SYSTEM'),
        ('Tyre Replacement', 'TYRE_REPLACEMENT', 'Vehicle tyre replacement service', 1, 'SYSTEM', 'SYSTEM'),
        ('Fuel Delivery', 'FUEL_DELIVERY', 'Emergency fuel delivery service', 1, 'SYSTEM', 'SYSTEM'),
        ('Engine Repair', 'ENGINE_REPAIR', 'Engine diagnosis and repair service', 1, 'SYSTEM', 'SYSTEM'),
        ('Electrical Repair', 'ELECTRICAL_REPAIR', 'Vehicle electrical system repair', 1, 'SYSTEM', 'SYSTEM'),
        ('AC Repair', 'AC_REPAIR', 'Vehicle air conditioning repair', 1, 'SYSTEM', 'SYSTEM'),
        ('Brake Repair', 'BRAKE_REPAIR', 'Vehicle brake system repair', 1, 'SYSTEM', 'SYSTEM'),
        ('Clutch Repair', 'CLUTCH_REPAIR', 'Vehicle clutch repair service', 1, 'SYSTEM', 'SYSTEM'),
        ('Oil Change', 'OIL_CHANGE', 'Engine oil replacement service', 1, 'SYSTEM', 'SYSTEM'),
        ('Accident Assistance', 'ACCIDENT_ASSISTANCE', 'Emergency assistance after vehicle accident', 1, 'SYSTEM', 'SYSTEM'),
        ('Lockout Assistance', 'LOCKOUT_ASSISTANCE', 'Vehicle lockout and key assistance', 1, 'SYSTEM', 'SYSTEM')
) AS source
(
    ServiceName,
    ServiceCode,
    Description,
    IsActive,
    CreatedBy,
    UpdatedBy
)

ON target.ServiceName = source.ServiceName

WHEN MATCHED THEN
    UPDATE SET
        target.ServiceCode = source.ServiceCode,
        target.Description = source.Description,
        target.IsActive = source.IsActive,
        target.UpdatedBy = source.UpdatedBy,
        target.UpdatedAt = GETUTCDATE()

WHEN NOT MATCHED THEN
    INSERT
    (
        ServiceName,
        ServiceCode,
        Description,
        IsActive,
        CreatedAt,
        CreatedBy,
        UpdatedAt,
        UpdatedBy
    )
    VALUES
    (
        source.ServiceName,
        source.ServiceCode,
        source.Description,
        source.IsActive,
        GETUTCDATE(),
        source.CreatedBy,
        GETUTCDATE(),
        source.UpdatedBy
    );


    -- MERGE script for WorkflowType table
    MERGE INTO [dbo].[WorkflowType] AS target
USING
(
    VALUES
        ('Manual', 'MANUAL', 'Workflow requires manual action by a user.', 1, 'SYSTEM', 'SYSTEM'),
        ('Automatic', 'AUTOMATIC', 'Workflow action is executed automatically by the system.', 1, 'SYSTEM', 'SYSTEM'),
        ('Conditional', 'CONDITIONAL', 'Workflow action is executed based on configured conditions.', 1, 'SYSTEM', 'SYSTEM')
)
AS source
(
    WorkflowTypeName,
    WorkflowTypeCode,
    Description,
    IsActive,
    CreatedBy,
    UpdatedBy
)
ON target.WorkflowTypeCode = source.WorkflowTypeCode

WHEN MATCHED THEN
    UPDATE SET
        target.WorkflowTypeName = source.WorkflowTypeName,
        target.Description = source.Description,
        target.IsActive = source.IsActive,
        target.UpdatedBy = source.UpdatedBy,
        target.UpdatedAt = GETUTCDATE()

WHEN NOT MATCHED THEN
    INSERT
    (
        WorkflowTypeName,
        WorkflowTypeCode,
        Description,
        IsActive,
        CreatedAt,
        CreatedBy,
        UpdatedAt,
        UpdatedBy
    )
    VALUES
    (
        source.WorkflowTypeName,
        source.WorkflowTypeCode,
        source.Description,
        source.IsActive,
        GETUTCDATE(),
        source.CreatedBy,
        GETUTCDATE(),
        source.UpdatedBy
    );