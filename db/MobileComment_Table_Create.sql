USE [Egharpay]
GO

/****** Object:  Table [dbo].[MobileComment]    Script Date: 15-10-2017 06:50:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MobileComment](
	[MobileCommentId] [int] IDENTITY(1,1) NOT NULL,
	[MobileId] [int] NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[Approve] [bit] NOT NULL,
 CONSTRAINT [PK_MobileComment] PRIMARY KEY CLUSTERED 
(
	[MobileCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MobileComment]  WITH CHECK ADD  CONSTRAINT [FK_MobileComment_Mobile] FOREIGN KEY([MobileId])
REFERENCES [dbo].[Mobile] ([MobileId])
GO

ALTER TABLE [dbo].[MobileComment] CHECK CONSTRAINT [FK_MobileComment_Mobile]
GO


