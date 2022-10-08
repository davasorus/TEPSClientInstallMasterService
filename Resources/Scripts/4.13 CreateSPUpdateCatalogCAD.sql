CREATE PROCEDURE [dbo].[UpdateCatalogCAD]
	@Client_ID int,
	@CAD_installed bit
AS
	update InstalledCatalog
	SET CAD_Installed = @CAD_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID