USE [CMMIDemo]
GO

/****** Object: SqlProcedure [dbo].[RestaurantCreate] Script Date: 12/17/2016 10:25:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RestaurantCreate]
	@restaurant VARCHAR(256),
	@cuisine smallint,
	@address VARCHAR(256),
	@address2 VARCHAR(256),
	@city VARCHAR(256),
	@state SMALLINT,
	@zip CHAR(5),
	@userId int,
	@id int output
AS
	IF EXISTS(SELECT Id FROM [dbo].[Restaurants] WHERE Restaurant = @restaurant) 
		SELECT @id = 0
	ELSE
		BEGIN
			DECLARE @location AS INT

			INSERT INTO [dbo].[Locations] ([Address], [Address2], [City], [StateId], [Zipcode], [CreatedBy], [CreatedDate])
			VALUES(@address, @address2, @city, @state, @zip, @userId, GETDATE())

			SELECT @location=@@IDENTITY

			INSERT INTO [dbo].[Restaurants] ([Restaurant], [CuisineId], [LocationId], [CreatedBy], [CreatedDate])
			VALUES(@restaurant, @cuisine, @location, @userId, GETDATE())

			SELECT @id=@@IDENTITY
 		END

RETURN 0
