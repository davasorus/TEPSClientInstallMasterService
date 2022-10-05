CREATE TABLE [dbo].[PreReqUpload]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [PreReq_Name] NVARCHAR(MAX) NULL, 
    [PreReq+Path] NVARCHAR(MAX) NULL, 
    [ModifiedDate_Time] NCHAR(10) NULL DEFAULT (GETDATE())
)