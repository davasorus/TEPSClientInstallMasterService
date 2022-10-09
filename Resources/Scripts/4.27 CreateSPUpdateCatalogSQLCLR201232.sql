CREATE PROCEDURE [dbo].[UpdateCatalogSQLCLR201232]
	@Client_ID int,
	@SQLCLR201264_installed bit
AS
	update InstalledCatalog
	SET [SQLCLR201264_Installed] = @SQLCLR201264_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID