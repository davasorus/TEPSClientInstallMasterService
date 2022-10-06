CREATE PROCEDURE [dbo].[GetClientByID]
	@id int
AS
	begin
	select * from TylerClientIMS.dbo.Clients
	where Id = @id
	end
