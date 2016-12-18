USE [CMMIDemo]
GO

/****** Object: Table [dbo].[Restaurants] Script Date: 12/17/2016 3:06:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Restaurants] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Restaurant]  VARCHAR (50) NOT NULL,
    [LocationId]  INT          NOT NULL,
    [CuisineId]   SMALLINT     NOT NULL,
    [CreatedBy]   INT          NOT NULL,
    [CreatedDate] DATETIME     NOT NULL, 
    CONSTRAINT [FK_Restaurants_Locations] FOREIGN KEY ([LocationId]) REFERENCES [Locations]([Id]), 
    CONSTRAINT [FK_Restaurants_Cuisines] FOREIGN KEY ([CuisineId]) REFERENCES [Cuisines]([Id])
);


