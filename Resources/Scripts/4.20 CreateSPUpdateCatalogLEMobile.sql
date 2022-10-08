CREATE PROCEDURE [dbo].[UpdateCatalogLEMobile]
	@Client_ID int,
	@LEMobile_installed bit
AS
	update InstalledCatalog
	SET LEMobile_Installed = @LEMobile_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID