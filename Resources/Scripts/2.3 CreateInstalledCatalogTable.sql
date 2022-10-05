CREATE TABLE [dbo].[InstalledCatalog]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Client_ID] INT NOT NULL, 
    [SQLCompact3532_Installed] BIT NOT NULL DEFAULT 0, 
    [SQLCompact3564_Installed] BIT NOT NULL DEFAULT 0, 
    [SQLCompact0464_Installed] BIT NOT NULL DEFAULT 0, 
    [SQLCLR200832_Installed] BIT NOT NULL DEFAULT 0, 
    [SQLCLR200864_Installed] BIT NOT NULL DEFAULT 0, 
    [ScenePD_Installed] BIT NOT NULL DEFAULT 0, 
    [Updater_Installed] BIT NOT NULL DEFAULT 0, 
    [GISComponents32_Installed] BIT NOT NULL DEFAULT 0, 
    [GISComponents64_Installed] BIT NOT NULL DEFAULT 0, 
    [DotNet_Installed] BIT NOT NULL DEFAULT 0, 
    [SQLCLR201232_Installed ] BIT NOT NULL DEFAULT 0, 
    [SQLCLR201264_Installed] BIT NOT NULL DEFAULT 0, 
    [DBProvider_Installed] BIT NOT NULL DEFAULT 0, 
    [LERMS_Installed] BIT NOT NULL DEFAULT 0, 
    [CAD_Installed] BIT NOT NULL DEFAULT 0, 
    [CADObserver_Installed] BIT NOT NULL DEFAULT 0, 
    [FireMobile_Installed] BIT NOT NULL DEFAULT 0, 
    [LEMobile_Installed] BIT NOT NULL DEFAULT 0, 
    [MobileMerge_Installed] BIT NOT NULL DEFAULT 0, 
    [MobileAgencyConfiguration] NVARCHAR(MAX) NULL, 
    [InitialInstallDate_Time] DATETIME2 NOT NULL DEFAULT (GETDATE()), 
    [ModifiedDate_time] DATETIME2 NULL
)