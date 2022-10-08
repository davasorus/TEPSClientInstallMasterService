CREATE PROCEDURE [dbo].[UpdateCatalogSQLComp3564]
	@Client_ID int,
	@SQLCompact35364_installed bit
AS
	update InstalledCatalog
	SET SQLCompact3564_Installed = @SQLCompact3564_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
