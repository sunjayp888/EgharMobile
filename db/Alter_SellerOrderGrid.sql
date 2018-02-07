USE [mumbile]
GO

/****** Object:  View [dbo].[SellerOrderGrid]    Script Date: 04-02-2018 11:00:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[SellerOrderGrid]
AS 
select
OS.OrderId,
OS.SellerId,
SellerName=Case when P1.Title is null then ' ' else P1.Title end +' '+ Case when P1.Forenames is null then ' ' else P1.Forenames end +' '+ case when P1.Surname is null then ' ' else P1.Surname end,
CustomerId=O.PersonnelId,
CustomerName=Case when P2.Title is null then ' ' else P2.Title end +' '+ Case when P2.Forenames is null then ' ' else P2.Forenames end +' '+ case when P2.Surname is null then ' ' else P2.Surname end,
O.MobileId,
MobileName=M.Name,
O.OrderStateId,
RequestTypeName=OST.Name,
OrderCreatedDate=O.CreatedDateTime
from OrderSeller OS
left join [Order] O on OS.OrderId=O.OrderId
left join Seller S on OS.SellerId=S.SellerId
left join Personnel P1 on S.PersonnelId=P1.PersonnelId
left join Personnel P2 on O.PersonnelId=P2.PersonnelId
left join Mobile M on O.MobileId=M.MobileId
left join OrderState OST on O.OrderStateId=OST.OrderStateId


GO


