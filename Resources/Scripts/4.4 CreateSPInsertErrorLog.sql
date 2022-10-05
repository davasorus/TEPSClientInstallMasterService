CREATE PROCEDURE [dbo].[InsertErrorLog]
	@ErrorMessage nvarchar(MAX),
	@ClientName nvarchar(MAX)

AS
	SET NOCOUNT ON;
	insert into dbo.errorLog
	(ClientName,ErrorMessage,ErrorDate_Time)
	VALUES (@ClientName,@ErrorMessage, GETDATE())