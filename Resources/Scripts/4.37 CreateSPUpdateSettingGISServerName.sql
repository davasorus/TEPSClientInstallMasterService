CREATE PROCEDURE [dbo].[UpdateSettingGISServerName]
	@GISServerName nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set GISServerName = @GISServerName,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID