USE [CMMIDemo]
GO

/****** Object: Table [dbo].[Users] Script Date: 12/17/2016 3:42:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Username]    VARCHAR (50)  NOT NULL,
    [FirstName]   VARCHAR (256) NULL,
    [LastName]    VARCHAR (256) NULL,
    [CreatedDate] DATETIME      NOT NULL
);


