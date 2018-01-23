USE [mumbile]
GO

/****** Object:  Table [dbo].[Seller]    Script Date: 22-01-2018 09:29:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Seller](
	[SellerId] [int] IDENTITY(1,1) NOT NULL,
	[PersonnelId] [int] NULL,
	[Name] [varchar](500) NULL,
	[RegistrationNumber] [varchar](500) NULL,
	[Owner] [varchar](500) NULL,
	[Contact1] [bigint] NULL,
	[Contact2] [bigint] NULL,
	[Contact3] [bigint] NULL,
	[Address1] [varchar](500) NULL,
	[Address2] [varchar](500) NULL,
	[Address3] [varchar](500) NULL,
	[Address4] [varchar](500) NULL,
	[Pincode] [varchar](50) NOT NULL,
	[CreatedDate] [date] NULL,
	[Remarks] [varchar](max) NULL,
	[ApprovalStateId] [int] NOT NULL,
	[Email] [varchar](500) NOT NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[SellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


