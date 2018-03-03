USE [mumbile]
GO

/****** Object:  View [dbo].[SellerOrderGrid]    Script Date: 03-03-2018 11:01:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[SellerOrderGrid]
AS 
select
SO.OrderId,
OrderCreatedDate=O.CreatedDateTime,
BuyersName=OP.Forenames+' '+OP.Surname,
BuyerPersonnelId=O.PersonnelId,
OrderState=OS.Name,
MobileName=M.Name,
SO.SellerId,
SellerName=SP.Forenames+' '+SP.Surname,
SellerPersonnelId=S.PersonnelId,
ShopName=S.Name,
SellerContact=S.Contact1,
SellerAddress=S.Address1,
SellerPincode=S.Pincode,
SellerEmail=S.Email,
SO.OrderStateId,
OrderStateName=OS.Name
from SellerOrder SO
inner join [Order] O on SO.OrderId=O.OrderId
inner join Seller S on SO.SellerId=S.SellerId
inner join Personnel OP on O.PersonnelId=OP.PersonnelId
inner join Personnel SP on S.PersonnelId=SP.PersonnelId
inner join OrderState OS on O.OrderStateId=OS.OrderStateId
inner join Mobile M on O.MobileId=M.MobileId


GO


