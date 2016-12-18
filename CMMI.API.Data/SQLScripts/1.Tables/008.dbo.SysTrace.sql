USE [CMMIDemo]
GO

/****** Object: Table [dbo].[SysTrace] Script Date: 12/17/2016 10:23:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SysTrace] (
    [Id]          INT           NOT NULL,
    [Level]       INT           NULL,
    [LogType]     VARCHAR (50)  NULL,
    [Message]     VARCHAR (256) NOT NULL,
    [CreatedDate] DATETIME      NOT NULL
);


