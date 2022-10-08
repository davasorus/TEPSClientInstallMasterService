CREATE PROCEDURE [dbo].[InsertNewClient]
	@ClientName nvarchar(MAX)
AS
	SET NOCOUNT ON;
	insert into dbo.Clients
	(ClientName)
	VALUES (@ClientName)
