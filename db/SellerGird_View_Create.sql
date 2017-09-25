USE [Egharpay]
GO

/****** Object:  View [dbo].[SellerGrid]    Script Date: 25-09-2017 10:55:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[SellerGrid]
as
select
S.SellerId,
S.Name,
S.RegistrationNumber,
S.[Owner],
S.Contact1,
S.Contact2,
S.Contact3,
S.Address1,
S.Address2,
S.Address3,
S.Address4,
S.Pincode,
S.CreatedDate,

isnull(S.Name,'')+isnull(S.RegistrationNumber,'')+isnull(S.[Owner],'')+
CONVERT(varchar, S.Contact1 )+
isnull(S.Address1,'')+isnull(S.Address2,'')+isnull(S.Address3,'')+isnull(S.Address4,'')+CONVERT(varchar, S.Pincode )
+ISNULL(CONVERT(varchar,S.CreatedDate, 101), '') + ISNULL(CONVERT(varchar,S.CreatedDate, 103), '') 
	  + ISNULL(CONVERT(varchar,S.CreatedDate, 105), '') + ISNULL(CONVERT(varchar,S.CreatedDate, 126), '') as SearchField

from Seller S 

Group By
S.SellerId,
S.Name,
S.RegistrationNumber,
S.[Owner],
S.Contact1,
S.Contact2,
S.Contact3,
S.Address1,
S.Address2,
S.Address3,
S.Address4,
S.Pincode,
S.CreatedDate





GO


