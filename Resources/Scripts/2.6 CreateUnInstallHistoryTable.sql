CREATE TABLE [dbo].[UnInstallHistory]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ClientName] NVARCHAR(50) NULL, 
    [EnrolledInstanceType] BIT NULL, 
    [Action] NVARCHAR(MAX) NULL, 
    [TransactionDate_Time] DATETIME2 NOT NULL DEFAULT (GETDATE())
)
