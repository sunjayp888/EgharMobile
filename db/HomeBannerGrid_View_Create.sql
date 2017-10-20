USE [Egharpay]
GO

/****** Object:  View [dbo].[HomeBannerGrid]    Script Date: 21-10-2017 12:26:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[HomeBannerGrid]
AS 

select 
HomeBannerId,
HB.Name,
SubTitle,
Tag,
StartDateTime,
EndDateTime,
Pincode,
Link,
HB.MobileId,
MobileName=M.Name
from HomeBanner HB
inner join Mobile M on HB.MobileId=M.MobileId


GO


