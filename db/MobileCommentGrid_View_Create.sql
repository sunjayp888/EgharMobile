USE [Egharpay]
GO

/****** Object:  View [dbo].[MobileCommentGrid]    Script Date: 15-10-2017 06:47:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











Create VIEW [dbo].[MobileCommentGrid]
AS 
select
MobileCommentId,
MC.MobileId,
MobileName=m.Name,
Comment,
UserId,
MC.CreatedDateTime,
Approve
from MobileComment MC
inner join Mobile M on MC.MobileId=M.MobileId










GO


