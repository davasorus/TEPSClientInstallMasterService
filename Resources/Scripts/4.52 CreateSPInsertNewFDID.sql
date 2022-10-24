CREATE PROCEDURE [dbo].[InsertNewFDID]
	@EnrolledInstanceType_ID int,
	@FDID nvarchar(max)
AS
	SET NOCOUNT ON;
	insert into dbo.FDIDs
	(FDID, EnrolledInstanceType_ID)
	VAlues (@FDID, @EnrolledInstanceType_ID)
