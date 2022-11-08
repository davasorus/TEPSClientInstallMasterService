
CREATE PROCEDURE [dbo].[GetFDIDByNameByEnrolledInstanceType]
	@FDID nvarchar(max),
	@EnrolledInstanceType_ID int
AS
BEGIN
	
	SET NOCOUNT ON;

	select * from dbo.FDIDs
	where FDID = @FDID
	AND EnrolledInstanceType_ID = @EnrolledInstanceType_ID
END
GO
