USE [mumbile]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 16-01-2018 08:11:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderGuid] [uniqueidentifier] NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[CreatedDateTime] [date] NOT NULL,
	[OrderStateId] [int] NOT NULL,
	[MobileId] [int] NOT NULL,
	[PersonnelIP] [nvarchar](max) NULL,
	[ShippingAddressId] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Mobile] FOREIGN KEY([MobileId])
REFERENCES [dbo].[Mobile] ([MobileId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Mobile]
GO


