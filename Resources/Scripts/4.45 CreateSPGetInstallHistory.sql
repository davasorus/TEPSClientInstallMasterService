CREATE PROCEDURE [dbo].[GetInstallHistory]
	
AS
	select Distinct id, ClientName,EnrolledInstanceType,Action, TransactionDate_Time 
  from dbo.InstallHistory
  order by TransactionDate_Time DESC
