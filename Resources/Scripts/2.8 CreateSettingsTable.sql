CREATE TABLE [dbo].[Settings] (
    [Id]                       INT            IDENTITY (1, 1) NOT NULL,
    [EnrolledInstanceType_ID]  INT            DEFAULT ((1)) NOT NULL,
    [MobileServerName]         NVARCHAR (MAX) NULL,
    [RecordsServerName]        NVARCHAR (MAX) NULL,
    [ESSServerName]            NVARCHAR (MAX) NULL,
    [CADServerName]            NVARCHAR (MAX) NULL,
    [GISServername]            NVARCHAR (MAX) NULL,
    [GISInstance]              NVARCHAR (MAX) NULL,
	[Client-Installation-Path]              NVARCHAR (MAX) NULL,
    [InitialCreationDate_Time] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [Date_TimeModified]        DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

