	CREATE PROCEDURE [dbo].[GetInstalledCatalogByID]
	@Client_ID int
AS
	SET NOCOUNT ON;
	Select * from dbo.InstalledCatalog
	where Client_ID = @Client_ID