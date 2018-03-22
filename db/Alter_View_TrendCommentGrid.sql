USE [mumbile]
GO

/****** Object:  View [dbo].[TrendCommentGrid]    Script Date: 22-03-2018 09:36:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[TrendCommentGrid]
AS 
select
TrendCommentId,
TrendName=T.Name,
Comment,
PersonnelId,
TC.CreatedDateTime,
TC.Approve
from TrendComment TC
inner join Trend T on TC.TrendId=T.TrendId
GO


