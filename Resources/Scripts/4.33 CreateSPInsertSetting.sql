CREATE PROCEDURE [dbo].[CreateInsertSettingsValue]
	@EnrolledInstanceType_ID int
as
	SET NOCOUNT ON;
	insert into dbo.Settings
	(EnrolledInstanceType_ID,InitialCreationDate_Time) 
	VALUES (@EnrolledInstanceType_ID,GETDATE())