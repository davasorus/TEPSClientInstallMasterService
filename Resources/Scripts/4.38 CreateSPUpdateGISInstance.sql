CREATE PROCEDURE [dbo].[UpdateSettingGISInstance]
	@GISInstance nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set GISInstance = @GISInstance,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID