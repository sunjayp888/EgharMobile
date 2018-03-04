USE [mumbile]
GO

/****** Object:  Table [dbo].[SellerOrder]    Script Date: 19-02-2018 11:03:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SellerOrder](
	[SellerOrderId] [int] IDENTITY(1,1) NOT NULL,
	[SellerId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[OrderStateId] [int] NOT NULL,
 CONSTRAINT [PK_OrderSeller] PRIMARY KEY CLUSTERED 
(
	[SellerOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SellerOrder]  WITH CHECK ADD  CONSTRAINT [FK_SellerOrder_OrderState] FOREIGN KEY([OrderStateId])
REFERENCES [dbo].[OrderState] ([OrderStateId])
GO

ALTER TABLE [dbo].[SellerOrder] CHECK CONSTRAINT [FK_SellerOrder_OrderState]
GO

ALTER TABLE [dbo].[SellerOrder]  WITH CHECK ADD  CONSTRAINT [FK_SellerOrder_Seller] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Seller] ([SellerId])
GO

ALTER TABLE [dbo].[SellerOrder] CHECK CONSTRAINT [FK_SellerOrder_Seller]
GO


