USE [mumbile]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 12-12-2017 11:20:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[PersonnelId] [int] NULL,
	[CreatedDate] [date] NOT NULL,
	[RequestTypeId] [int] NULL,
	[MobileId] [int] NOT NULL,
	[SellerId] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Mobile] FOREIGN KEY([MobileId])
REFERENCES [dbo].[Mobile] ([MobileId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Mobile]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Seller] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Seller] ([SellerId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Seller]
GO


