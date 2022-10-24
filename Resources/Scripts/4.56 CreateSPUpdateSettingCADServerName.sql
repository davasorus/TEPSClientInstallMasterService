CREATE PROCEDURE [dbo].[UpdateSettingCADServerName]
	@CADServerName nvarchar(max),
	@EnrolledInstanceType_ID int
as
	update dbo.Settings
	set CADServerName = @CADServerName,
	Date_TimeModified = (GETDATE())
	where
	EnrolledInstanceType_ID = @EnrolledInstanceType_ID