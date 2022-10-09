CREATE PROCEDURE [dbo].[UpdateCatalogSQLCLR201232]
	@Client_ID int,
	@SQLCLR201232_installed bit
AS
	update InstalledCatalog
	SET [SQLCLR201232_Installed] = @SQLCLR201232_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID