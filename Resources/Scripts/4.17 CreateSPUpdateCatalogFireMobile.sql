CREATE PROCEDURE [dbo].[UpdateCatalogFireMobile]
	@Client_ID int,
	@FireMobile_installed bit
AS
	update InstalledCatalog
	SET FireMobile_Installed = @FireMobile_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID