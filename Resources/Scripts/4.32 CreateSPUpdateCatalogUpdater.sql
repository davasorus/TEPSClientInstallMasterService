CREATE PROCEDURE [dbo].[UpdateCatalogUpdater]
	@Client_ID int,
	@Updater_installed bit
AS
	update InstalledCatalog
	SET Updater_Installed = @Updater_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
