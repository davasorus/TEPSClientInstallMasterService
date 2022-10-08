CREATE PROCEDURE [dbo].[UpdateCatalogGISComp64]
	@Client_ID int,
	@GISComponents64_installed bit
AS
	update InstalledCatalog
	SET GISComponents64_Installed = @GISComponents64_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
