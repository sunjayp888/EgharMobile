USE [mumbile]
GO

/****** Object:  Table [dbo].[MobileRepairPayment]    Script Date: 27-02-2018 23:56:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MobileRepairPayment](
	[MobileRepairPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[MobileRepairId] [int] NOT NULL,
	[RecievedBy] [varchar](256) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[Otp] [int] NOT NULL,
 CONSTRAINT [PK_MobileRepairPayment] PRIMARY KEY CLUSTERED 
(
	[MobileRepairPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MobileRepairPayment]  WITH CHECK ADD  CONSTRAINT [FK_MobileRepairPayment_MobileRepair] FOREIGN KEY([MobileRepairId])
REFERENCES [dbo].[MobileRepair] ([MobileRepairId])
GO

ALTER TABLE [dbo].[MobileRepairPayment] CHECK CONSTRAINT [FK_MobileRepairPayment_MobileRepair]
GO


