CREATE PROCEDURE [dbo].[GetTop50Errors]
	
AS
	begin
	SELECT TOP (50) [Id]
      ,[ClientName]
      ,[ErrorMessage]
      ,[ErrorDate_Time]
  FROM [TylerClientIMS].[dbo].[errorLog]
  order by ErrorDate_Time desc
	end
