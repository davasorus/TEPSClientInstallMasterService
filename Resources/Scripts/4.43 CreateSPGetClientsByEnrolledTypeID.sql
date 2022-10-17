CREATE PROCEDURE [dbo].[GetClientsByEnrolledTypeID]
	@EnrolledInstanceType_ID int
AS
	select * from dbo.Clients
	where EnrolledInstanceType_ID = @EnrolledInstanceType_ID
