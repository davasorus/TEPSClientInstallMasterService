CREATE PROCEDURE [dbo].[GetFDIDByNameByEnrolledInstanceType]
	@FDID nvarchar(max),
	@EnrolledInstanceType_ID int
AS
	SELECT * from dbo.FDIDs
	where FDID = @FDID AND EnrolledInstanceType_ID = @EnrolledInstanceType_ID
