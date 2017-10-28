USE [Egharpay]
GO

/****** Object:  Table [dbo].[HomeBannerDocumentDetail]    Script Date: 28-10-2017 02:03:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HomeBannerDocumentDetail](
	[HomeBannerDocumentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[HomeBannerId] [int] NOT NULL,
	[DocumentDetailId] [int] NOT NULL,
 CONSTRAINT [PK_HomeBannerDocumentDetail] PRIMARY KEY CLUSTERED 
(
	[HomeBannerDocumentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HomeBannerDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_HomeBannerDocumentDetail_DocumentDetail] FOREIGN KEY([DocumentDetailId])
REFERENCES [dbo].[DocumentDetail] ([DocumentDetailId])
GO

ALTER TABLE [dbo].[HomeBannerDocumentDetail] CHECK CONSTRAINT [FK_HomeBannerDocumentDetail_DocumentDetail]
GO

ALTER TABLE [dbo].[HomeBannerDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_HomeBannerDocumentDetail_HomeBanner] FOREIGN KEY([HomeBannerId])
REFERENCES [dbo].[HomeBanner] ([HomeBannerId])
GO

ALTER TABLE [dbo].[HomeBannerDocumentDetail] CHECK CONSTRAINT [FK_HomeBannerDocumentDetail_HomeBanner]
GO


