CREATE PROCEDURE [dbo].[UpdateCatalogScenePD]
	@Client_ID int,
	@ScenePD_installed bit
AS
	update InstalledCatalog
	SET ScenePD_Installed = @ScenePD_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
