CREATE PROCEDURE [dbo].[GetSettingByInstance]
	@EnrolledInstanceType_ID int
AS
	SET NOCOUNT ON;
	Select * from dbo.Settings
	where EnrolledInstanceType_ID = @EnrolledInstanceType_ID
