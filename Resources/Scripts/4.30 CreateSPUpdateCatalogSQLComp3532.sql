CREATE PROCEDURE [dbo].[UpdateCatalogSQLComp3532]
	@Client_ID int,
	@SQLCompact3532_installed bit
AS
	update InstalledCatalog
	SET SQLCompact3532_Installed = @SQLCompact3532_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
