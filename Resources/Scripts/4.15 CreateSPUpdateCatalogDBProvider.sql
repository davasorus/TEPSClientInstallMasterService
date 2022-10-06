CREATE PROCEDURE [dbo].[UpdateCatalogDBProvider]
	@Client_ID int,
	@DBProvider_installed bit
AS
	update InstalledCatalog
	SET DBProvider_Installed = @DBProvider_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID