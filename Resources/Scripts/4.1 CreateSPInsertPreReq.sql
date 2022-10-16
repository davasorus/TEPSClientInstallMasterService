CREATE PROCEDURE [dbo].[InsertPreReq]
	@PreReq_Name nvarchar(MAX),
	@PreReq_Path nvarchar(max),
	@EnrolledInstanceType int
AS
	SET NOCOUNT ON;
	insert into dbo.PreReqUpload
	(EnrolledInstanceType ,PreReq_Name,PreReq_Path,ModifiedDate_Time)
	VALUES (@EnrolledInstanceType ,@PreReq_Name,@PreReq_Path, GETDATE())
