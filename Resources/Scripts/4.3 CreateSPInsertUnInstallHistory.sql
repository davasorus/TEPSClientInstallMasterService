﻿CREATE PROCEDURE [dbo].[InsertUninstallHistory]
	@ClientName nvarchar(MAX),
	@EnrolledInstanceType bit,
	@Action nvarchar(max)
AS
	SET NOCOUNT ON;
	insert into dbo.UnInstallHistory
	(ClientName,EnrolledInstanceType,Action,TransactionDate_Time)
	VALUES (@ClientName,@EnrolledInstanceType,@Action,GETDATE())
