CREATE PROCEDURE [dbo].[UpdateSettingRecordsServerName]
	@RecordsServerName nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set RecordsServerName = @RecordsServerName,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID