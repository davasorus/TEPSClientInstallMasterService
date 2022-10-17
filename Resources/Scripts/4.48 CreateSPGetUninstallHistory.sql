CREATE PROCEDURE [dbo].[GetUnInstallHistory]
	
AS
	select * from dbo.UnInstallHistory
	order by TransactionDate_Time DESC