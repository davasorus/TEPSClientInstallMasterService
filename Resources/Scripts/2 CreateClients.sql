CREATE TABLE [dbo].[Clients]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ClientName] NVARCHAR(MAX) NOT NULL, 
    [PassedHealthCheck] BIT NOT NULL DEFAULT 0, 
    [MostRecentHealthCheckDate_Time] DATETIME2 NULL, 
    [InstalledCatalog_ID] INT NULL, 
    [MostRecentInstallDate_Time] DATETIME2 NULL, 
    [EnrolledInstanceType_ID] INT NOT NULL DEFAULT 1, 
    [InitialCreationDate_Time] DATETIME2 NOT NULL DEFAULT (GETDATE()), 
    [Date_TimeModified] DATETIME2 NULL
)
