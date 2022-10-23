CREATE PROCEDURE [dbo].[GetUnInstallHistory]
	
AS
	select Distinct id, ClientName,EnrolledInstanceType,Action, TransactionDate_Time 
	from dbo.UnInstallHistory
	order by TransactionDate_Time DESC