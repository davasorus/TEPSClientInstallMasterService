CREATE PROCEDURE [dbo].[GetPreReqByName]
	@PreReqName nvarchar(max),
	@EnrolledInstanceType_ID int
AS
	select * from dbo.PreReqUpload
	where PreReq_Name = @PreReqName 
	AND EnrolledInstanceType = @EnrolledInstanceType_ID