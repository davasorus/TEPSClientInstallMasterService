CREATE PROCEDURE [dbo].[GetORIByName]
	@ORI nvarchar(max),
	@EnrolledInstanceType_ID int
AS
	SELECT * from dbo.ORIs
	where ORI = @ORI AND EnrolledInstanceType_ID = @EnrolledInstanceType_ID
