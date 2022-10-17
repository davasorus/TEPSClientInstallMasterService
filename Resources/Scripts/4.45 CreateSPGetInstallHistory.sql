CREATE PROCEDURE [dbo].[GetInstallHistory]
	
AS
	select * from dbo.InstallHistory
	order by TransactionDate_Time DESC
