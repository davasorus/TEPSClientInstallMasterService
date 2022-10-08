CREATE PROCEDURE [dbo].[UpdateCatalogSQLCLR200832]
	@Client_ID int,
	@SQLCLR200832_installed bit
AS
	update InstalledCatalog
	SET SQLCLR200832_Installed = @SQLCLR200832_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
