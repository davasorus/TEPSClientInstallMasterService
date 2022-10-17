CREATE PROCEDURE [dbo].[GetUnInstallHistoryByEnrolledType]
	@EnrolledInstanceType_ID int
AS
	select * from dbo.UnInstallHistory
	where EnrolledInstanceType = @EnrolledInstanceType_ID
	order by TransactionDate_Time DESC