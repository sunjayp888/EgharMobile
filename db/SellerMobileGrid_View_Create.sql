USE [Egharpay]
GO

/****** Object:  View [dbo].[SellerMobileGrid]    Script Date: 15-11-2017 11:06:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[SellerMobileGrid]
AS 
SELECT 

SM.SellerMobileId,
SM.MobileId,
MobileName=M.Name,
SM.SellerId,
SellerName=S.Name,
SM.Price,
SM.DiscountPrice,
SM.CreatedDateTime,
SM.EMIAvailable,
ISNULL(M.Name, '')+ISNULL(S.Name, '')+CONVERT(varchar, SM.Price )+
CONVERT(varchar, SM.DiscountPrice ) AS SearchField

from SellerMobile SM
inner join Mobile M on SM.MobileId=M.MobileId
inner join Seller S on SM.SellerId=S.SellerId






GO


