USE [mumbile]
GO

/****** Object:  Table [dbo].[MobileRepairMobileFault]    Script Date: 15-05-2018 10:49:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MobileRepairMobileFault](
	[MobileRepairMobileFaultId] [int] IDENTITY(1,1) NOT NULL,
	[MobileRepairId] [int] NOT NULL,
	[MobileFaultId] [int] NOT NULL,
 CONSTRAINT [PK_MobileRepairMobileFault] PRIMARY KEY CLUSTERED 
(
	[MobileRepairMobileFaultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MobileRepairMobileFault]  WITH CHECK ADD  CONSTRAINT [FK_MobileRepairMobileFault_MobileFault] FOREIGN KEY([MobileFaultId])
REFERENCES [dbo].[MobileFault] ([MobileFaultId])
GO

ALTER TABLE [dbo].[MobileRepairMobileFault] CHECK CONSTRAINT [FK_MobileRepairMobileFault_MobileFault]
GO

ALTER TABLE [dbo].[MobileRepairMobileFault]  WITH CHECK ADD  CONSTRAINT [FK_MobileRepairMobileFault_MobileRepair] FOREIGN KEY([MobileRepairId])
REFERENCES [dbo].[MobileRepair] ([MobileRepairId])
GO

ALTER TABLE [dbo].[MobileRepairMobileFault] CHECK CONSTRAINT [FK_MobileRepairMobileFault_MobileRepair]
GO


