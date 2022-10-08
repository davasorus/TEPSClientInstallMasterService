CREATE PROCEDURE [dbo].[UpdateCatalogLERMS]
	@Client_ID int,
	@LERMS_installed bit
AS
	update InstalledCatalog
	SET LERMS_Installed = @LERMS_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID