USE [CMMIDemo]
GO

/****** Object: Table [dbo].[Reviews] Script Date: 12/17/2016 3:42:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reviews] (
    [Id]           INT           NOT NULL,
    [RestaurantId] INT           NOT NULL,
    [UserId]       INT           NOT NULL,
    [Review]       VARCHAR (MAX) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL
);


