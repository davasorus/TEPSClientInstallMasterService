CREATE PROCEDURE [dbo].[InsertPreReq]
	@PreReq_Name nvarchar(MAX),
	@PreReq_Path nvarchar(max)
AS
	SET NOCOUNT ON;
	insert into dbo.PreReqUpload
	(PreReq_Name,PreReq_Path,ModifiedDate_Time)
	VALUES (@PreReq_Name,@PreReq_Path, GETDATE())
