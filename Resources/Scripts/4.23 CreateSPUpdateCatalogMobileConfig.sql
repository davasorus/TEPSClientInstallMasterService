CREATE PROCEDURE [dbo].[UpdateCatalogMobileConfig]
	@Client_ID int,
	@MobileAgencyConfig nvarchar(max)
AS
	update InstalledCatalog
	SET MobileAgencyConfiguration = @MobileAgencyConfig,
	ModifiedDate_time = (GETDATE())
	where Client_ID = @Client_ID