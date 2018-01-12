USE [mumbile]
GO

/****** Object:  Table [dbo].[OrderSeller]    Script Date: 12-01-2018 07:53:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderSeller](
	[OrderSellerId] [int] IDENTITY(1,1) NOT NULL,
	[SellerId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_OrderSeller] PRIMARY KEY CLUSTERED 
(
	[OrderSellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrderSeller]  WITH CHECK ADD  CONSTRAINT [FK_OrderSeller_Seller] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Seller] ([SellerId])
GO

ALTER TABLE [dbo].[OrderSeller] CHECK CONSTRAINT [FK_OrderSeller_Seller]
GO


