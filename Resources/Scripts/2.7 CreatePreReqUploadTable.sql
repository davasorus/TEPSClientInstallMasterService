CREATE TABLE [dbo].[PreReqUpload] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [PreReq_Name]       NVARCHAR (MAX) NULL,
    [PreReq_Path]       NVARCHAR (MAX) NULL,
    [ModifiedDate_Time] NCHAR (10)     DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

