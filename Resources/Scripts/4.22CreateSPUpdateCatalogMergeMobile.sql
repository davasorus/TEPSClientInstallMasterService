CREATE PROCEDURE [dbo].[UpdateCatalogMergeMobile]
	@Client_ID int,
	@MobileMerge_installed bit
AS
	update InstalledCatalog
	SET MobileMerge_Installed = @MobileMerge_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID