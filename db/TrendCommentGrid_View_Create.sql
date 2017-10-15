USE [Egharpay]
GO

/****** Object:  View [dbo].[TrendCommentGrid]    Script Date: 15-10-2017 06:48:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











CREATE VIEW [dbo].[TrendCommentGrid]
AS 
select
TrendCommentId,
TrendName=T.Name,
Comment,
UserId,
TC.CreatedDateTime,
TC.Approve
from TrendComment TC
inner join Trend T on TC.TrendId=T.TrendId










GO


