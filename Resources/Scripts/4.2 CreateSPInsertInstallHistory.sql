CREATE PROCEDURE [dbo].[InsertInstallHistory]
	@ClientName nvarchar(MAX),
	@EnrolledInstanceType bit,
	@Action nvarchar(max),
	@DateTime DateTime2
AS
	SET NOCOUNT ON;
	insert into dbo.InstallHistory
	(ClientName,EnrolledInstanceType,Action,TransactionDate_Time)
	VALUES (@ClientName,@EnrolledInstanceType,@Action,GETDATE())
