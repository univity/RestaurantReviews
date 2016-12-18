USE [CMMIDemo]
GO

/****** Object: View [dbo].[ViewReviews] Script Date: 12/17/2016 10:24:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewReviews]
	AS 
	SELECT 
	R.Id,
	RS.Restaurant,
	U.Username,
	R.Review,
	R.CreatedDate 
	FROM [Reviews] AS R
	INNER JOIN [Restaurants] AS RS ON R.RestaurantId = RS.Id
	INNER JOIN [Users] AS U ON R.UserId = U.Id
