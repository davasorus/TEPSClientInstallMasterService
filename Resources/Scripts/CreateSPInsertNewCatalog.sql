CREATE PROCEDURE [dbo].[InsertNewCatalog]
	@client_ID int
AS
	SET NOCOUNT ON;
	insert into dbo.InstalledCatalog
	(Client_ID)
	VALUES (@client_ID)
