CREATE PROCEDURE [dbo].[GetTop1000Errors]
	
AS
	begin
	SELECT TOP (1000) [Id]
      ,[ClientName]
      ,[ErrorMessage]
      ,[ErrorDate_Time]
  FROM [TylerClientIMS].[dbo].[errorLog]
  order by ErrorDate_Time desc
	end