USE [CMMIDemo]
GO

/****** Object: Table [dbo].[States] Script Date: 12/17/2016 3:42:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[States] (
    [Id]          SMALLINT       NOT NULL,
    [State]       VARBINARY (50) NOT NULL,
    [Abreviation] CHAR (2)       NOT NULL
);


