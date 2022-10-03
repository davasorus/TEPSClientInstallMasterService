CREATE PROCEDURE [dbo].[GetTop50Errors]
	
AS
	begin
	SELECT TOP (50) [Id]
      ,[ErrorMessage]
      ,[ErrorDate_Time]
  FROM [TylerClientIMS].[dbo].[errorLog]
	end
