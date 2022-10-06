CREATE PROCEDURE [dbo].[UpdateCatalogCADObserver]
	@Client_ID int,
	@CADObserver_installed bit
AS
	update InstalledCatalog
	SET CADObserver_Installed = @CADObserver_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID