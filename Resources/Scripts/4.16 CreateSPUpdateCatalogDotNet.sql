CREATE PROCEDURE [dbo].[UpdateCatalogDotNet]
	@Client_ID int,
	@DotNet_installed bit
AS
	update InstalledCatalog
	SET DotNet_Installed = @DotNet_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID