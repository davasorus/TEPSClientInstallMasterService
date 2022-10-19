CREATE PROCEDURE [dbo].[InsertNewORI]
	@EnrolledInstanceType_ID int,
	@ORI nvarchar(max)
AS
	SET NOCOUNT ON;
	insert into dbo.ORIs
	(ORI, EnrolledInstanceType_ID)
	VAlues (@ORI, @EnrolledInstanceType_ID)
