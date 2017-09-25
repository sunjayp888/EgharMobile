USE [Egharpay]
GO

/****** Object:  Table [dbo].[Advertise]    Script Date: 25-09-2017 09:40:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Advertise](
	[AdvertiseId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Image1] [varchar](500) NOT NULL,
	[Image2] [varchar](500) NULL,
	[Image3] [varchar](500) NULL,
	[Image4] [varchar](500) NULL,
	[Image5] [varchar](500) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Tag] [varchar](500) NOT NULL,
	[Detail] [varchar](500) NOT NULL,
	[Link] [varchar](500) NULL,
 CONSTRAINT [PK_Advertise] PRIMARY KEY CLUSTERED 
(
	[AdvertiseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


