CREATE PROCEDURE [dbo].[GetClientByName]
	@ClientName nvarchar (MAX)
AS
	begin
	select * from TylerClientIMS.dbo.Clients
	where ClientName = @ClientName
	end