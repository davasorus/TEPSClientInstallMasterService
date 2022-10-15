CREATE PROCEDURE [dbo].[UpdateSettingESSServerName]
	@EssServerName nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set ESSServerName = @ESSServerName,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID