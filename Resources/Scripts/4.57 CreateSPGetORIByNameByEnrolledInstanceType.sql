
CREATE PROCEDURE [dbo].[GetORIByNameByEnrolledInstanceType]
	@ORI nvarchar(max),
	@EnrolledInstanceType_ID int
AS
BEGIN
	
	SET NOCOUNT ON;

	select * from dbo.ORIs
	where ORI = @ORI
	AND EnrolledInstanceType_ID = @EnrolledInstanceType_ID
END
GO
