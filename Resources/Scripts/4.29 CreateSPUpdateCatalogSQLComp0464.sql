CREATE PROCEDURE [dbo].[UpdateCatalogSQLComp0464]
	@Client_ID int,
	@SQLCompact0464_installed bit
AS
	update InstalledCatalog
	SET SQLCompact0464_Installed = @SQLCompact0464_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
