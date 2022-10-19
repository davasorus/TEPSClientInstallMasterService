CREATE TABLE [dbo].[FDIDs]
(
	 [Id] INT            IDENTITY (1, 1) NOT NULL,
	 [FDID]              NVARCHAR(MAX)             NOT NULL,
     [EnrolledInstanceType_ID]        INT            DEFAULT ((1)) NOT NULL,
     [CreationDate_Time]       DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
	 PRIMARY KEY CLUSTERED ([Id] ASC)
)
