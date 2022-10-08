CREATE PROCEDURE [dbo].[UpdateCatalogGISComp32]
	@Client_ID int,
	@GISComponents32_installed bit
AS
	update InstalledCatalog
	SET GISComponents32_Installed = @GISComponents32_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
