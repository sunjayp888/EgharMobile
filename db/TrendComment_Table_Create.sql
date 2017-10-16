USE [Egharpay]
GO

/****** Object:  Table [dbo].[TrendComment]    Script Date: 15-10-2017 06:50:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TrendComment](
	[TrendCommentId] [int] NOT NULL,
	[TrendId] [int] NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[Approve] [bit] NOT NULL,
 CONSTRAINT [PK_TrendComment] PRIMARY KEY CLUSTERED 
(
	[TrendCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TrendComment]  WITH CHECK ADD  CONSTRAINT [FK_TrendComment_Trend] FOREIGN KEY([TrendId])
REFERENCES [dbo].[Trend] ([TrendId])
GO

ALTER TABLE [dbo].[TrendComment] CHECK CONSTRAINT [FK_TrendComment_Trend]
GO


