CREATE PROCEDURE [dbo].[InsertInstallHistory]
	@ClientName nvarchar(MAX),
	@EnrolledInstanceType int,
	@Action nvarchar(max)
AS
	SET NOCOUNT ON;
	insert into dbo.InstallHistory
	(ClientName,EnrolledInstanceType,Action,TransactionDate_Time)
	VALUES (@ClientName,@EnrolledInstanceType,@Action,GETDATE())
