USE [CMMIDemo]
GO

/****** Object: Table [dbo].[SysErrors] Script Date: 12/17/2016 10:22:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SysErrors] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [ParentId]    INT           NULL,
    [Exception]   VARCHAR (256) NULL,
    [Message]     VARCHAR (256) NULL,
    [Stack]       VARCHAR (MAX) NOT NULL,
    [Details]     VARCHAR (MAX) NOT NULL,
    [CreatedDate] DATETIME      NULL
);


