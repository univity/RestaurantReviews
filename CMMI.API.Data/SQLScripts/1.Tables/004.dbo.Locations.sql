USE [CMMIDemo]
GO

/****** Object: Table [dbo].[Locations] Script Date: 12/17/2016 3:41:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Locations] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Address]     VARCHAR (256)   NOT NULL,
    [Address2]    VARCHAR (256)   NULL,
    [City]        VARCHAR (256)   NOT NULL,
    [StateId]     SMALLINT        NOT NULL,
    [Zipcode]     CHAR (5)        NOT NULL,
    [Longitude]   DECIMAL (12, 9) NULL,
    [Latitude]    DECIMAL (12, 9) NULL,
    [CreatedBy]   INT             NOT NULL,
    [CreatedDate] DATETIME        NOT NULL
);


