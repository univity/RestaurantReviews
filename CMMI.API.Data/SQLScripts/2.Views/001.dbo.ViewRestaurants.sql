USE [CMMIDemo]
GO

/****** Object: View [dbo].[ViewRestaurants] Script Date: 12/17/2016 10:24:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewRestaurants]
	AS 
	
	SELECT
	R.Id,
	R.Restaurant,
	C.Cuisine,
	L.City,
	R.CreatedBy  
	
	FROM [Restaurants] AS R
	INNER JOIN [Cuisines] AS C ON C.Id = R.CuisineId 
	INNER JOIN [Locations] AS L ON L.Id = R.LocationId
