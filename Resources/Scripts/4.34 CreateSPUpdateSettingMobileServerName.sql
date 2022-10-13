CREATE PROCEDURE [dbo].[UpdateSettingMobileServerName]
	@MobileServername nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set MobileServerName = @MobileServername,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID