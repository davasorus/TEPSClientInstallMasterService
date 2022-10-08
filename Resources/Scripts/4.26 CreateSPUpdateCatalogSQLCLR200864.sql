CREATE PROCEDURE [dbo].[UpdateCatalogSQLCLR200864]
	@Client_ID int,
	@SQLCLR200864_installed bit
AS
	update InstalledCatalog
	SET SQLCLR200864_Installed = @SQLCLR200864_installed,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID
