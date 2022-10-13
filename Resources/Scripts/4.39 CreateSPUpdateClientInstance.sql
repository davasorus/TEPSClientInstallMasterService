CREATE PROCEDURE [dbo].[UpdateClientInstance]
	@EnrolledInstanceType_ID int,
	@client_ID int
as
	update dbo.Clients
	set EnrolledInstanceType_ID = @EnrolledInstanceType_ID,
	Date_TimeModified = GETDATE()
	where 
	Id = @client_ID