USE [Egharpay]
GO

/****** Object:  Table [dbo].[HomeBanner]    Script Date: 23-10-2017 12:13:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[HomeBanner](
	[HomeBannerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[SubTitle] [varchar](4000) NULL,
	[Tag] [varchar](4000) NULL,
	[StartDateTime] [date] NOT NULL,
	[EndDateTime] [date] NOT NULL,
	[Pincode] [varchar](10) NULL,
	[Link] [varchar](1000) NULL,
	[MobileId] [int] NULL,
	[ImagePath] [varchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_HomeBanner] PRIMARY KEY CLUSTERED 
(
	[HomeBannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[HomeBanner]  WITH CHECK ADD  CONSTRAINT [FK_HomeBanner_Mobile] FOREIGN KEY([MobileId])
REFERENCES [dbo].[Mobile] ([MobileId])
GO

ALTER TABLE [dbo].[HomeBanner] CHECK CONSTRAINT [FK_HomeBanner_Mobile]
GO


