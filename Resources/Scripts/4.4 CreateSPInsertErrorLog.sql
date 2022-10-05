CREATE PROCEDURE [dbo].[InsertErrorLog]
	@ErrorMessage nvarchar(MAX)

AS
	SET NOCOUNT ON;
	insert into dbo.errorLog
	(ErrorMessage,ErrorDate_Time)
	VALUES (@ErrorMessage, GETDATE())