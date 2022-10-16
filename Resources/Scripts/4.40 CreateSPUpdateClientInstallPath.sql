CREATE PROCEDURE [dbo].[UpdateSettingClientInstallPath]
	@ClientInstallPath nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set [Client-Installation-Path] = @ClientInstallPath,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID