USE [Egharpay]
GO

/****** Object:  View [dbo].[SellerOrderGrid]    Script Date: 26-11-2017 12:52:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create view [dbo].[SellerOrderGrid]
as
select
O.OrderId,
O.SellerId,
SellerName=S.Name,
O.MobileId,
MobileName=M.Name,
O.CustomerId,
CustomerName=P.Forenames+' '+P.Surname,
O.RequestTypeId,
RequestTypeName=R.Name,
O.CreatedDate
From 
[Order] O left join Seller S with (nolock)
on O.SellerId=S.SellerId left join Mobile M with(nolock)
on O.MobileId=M.MobileId left join Personnel P with(nolock)
on O.CustomerId=P.PersonnelId left join RequestType R with(nolock)
on O.RequestTypeId=R.RequestTypeId

group by
O.OrderId,
O.SellerId,
S.Name,
O.MobileId,
M.Name,
O.CustomerId,
P.Forenames,
P.Surname,
O.RequestTypeId,
R.Name,
O.CreatedDate


GO


