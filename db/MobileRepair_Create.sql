USE [mumbile]
GO

/****** Object:  Table [dbo].[MobileRepair]    Script Date: 03-03-2018 10:17:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MobileRepair](
	[MobileRepairId] [int] IDENTITY(1,1) NOT NULL,
	[MobileNumber] [decimal](18, 0) NOT NULL,
	[ModelName] [varchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[CouponCode] [varchar](50) NULL,
	[MobileRepairStateId] [int] NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[Company] [varchar](100) NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[City] [varchar](100) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[ZipPostalCode] [varchar](100) NULL,
	[PhoneNumber] [varchar](100) NULL,
	[LandMark] [varchar](100) NULL,
	[District] [varchar](100) NULL,
	[AlternateNumber] [decimal](18, 0) NULL,
	[AppointmentDate] [datetime] NULL,
	[Comment] [varchar](max) NULL,
 CONSTRAINT [PK_MobileRepair] PRIMARY KEY CLUSTERED 
(
	[MobileRepairId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


