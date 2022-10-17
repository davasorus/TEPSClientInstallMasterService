CREATE PROCEDURE [dbo].[GetInstallHistoryByEnrolledType]
	@EnrolledInstanceType_ID int
AS
	select * from dbo.InstallHistory
	where EnrolledInstanceType = @EnrolledInstanceType_ID
	order by TransactionDate_Time DESC
