USE [Egharpay]
GO

/****** Object:  Table [dbo].[Seller]    Script Date: 28-10-2017 12:28:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Seller](
	[SellerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[RegistrationNumber] [varchar](500) NULL,
	[Owner] [varchar](500) NULL,
	[Contact1] [bigint] NOT NULL,
	[Contact2] [bigint] NULL,
	[Contact3] [bigint] NULL,
	[Address1] [varchar](500) NOT NULL,
	[Address2] [varchar](500) NOT NULL,
	[Address3] [varchar](500) NULL,
	[Address4] [varchar](500) NULL,
	[Pincode] [int] NOT NULL,
	[CreatedDate] [date] NULL,
	[Remarks] [varchar](max) NULL,
	[SellerApprovalStateId] [int] NOT NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[SellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


