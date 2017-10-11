USE [Egharpay]
GO

/****** Object:  Table [dbo].[Mobile]    Script Date: 11-10-2017 11:49:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Mobile](
	[MobileId] [int] IDENTITY(1,1) NOT NULL,
	[BrandId] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[ReleasedDate] [varchar](500) NULL,
	[BodyDimension] [varchar](500) NULL,
	[OS] [varchar](500) NULL,
	[Storage] [varchar](500) NULL,
	[DisplayResolution] [varchar](500) NULL,
	[CameraPixel] [varchar](10) NULL,
	[RAM] [varchar](10) NULL,
	[Chipset] [varchar](100) NULL,
	[BatterySize] [varchar](10) NULL,
	[BatteryType] [varchar](10) NULL,
	[Technology] [varchar](500) NULL,
	[Network2GBands] [varchar](500) NULL,
	[Network3GBands] [varchar](500) NULL,
	[Network4GBands] [varchar](500) NULL,
	[Speed] [varchar](500) NULL,
	[Gprs] [varchar](500) NULL,
	[Edge] [varchar](500) NULL,
	[Announced] [varchar](500) NULL,
	[Status] [varchar](500) NULL,
	[Dimensions] [varchar](500) NULL,
	[Weight] [varchar](500) NULL,
	[Sim] [varchar](500) NULL,
	[MiscellaneousBody] [varchar](500) NULL,
	[DisplayType] [varchar](500) NULL,
	[DisplaySize] [varchar](500) NULL,
	[Multitouch] [varchar](500) NULL,
	[Protection] [varchar](500) NULL,
	[Cpu] [varchar](500) NULL,
	[Gpu] [varchar](500) NULL,
	[CardSlot] [varchar](500) NULL,
	[InternalMemory] [varchar](500) NULL,
	[PrimaryCamera] [varchar](500) NULL,
	[CameraFeatures] [varchar](500) NULL,
	[Video] [varchar](500) NULL,
	[SecondaryCamera] [varchar](500) NULL,
	[AlertTypes] [varchar](500) NULL,
	[Loudspeaker] [varchar](500) NULL,
	[Sound3Point5MmJack] [varchar](500) NULL,
	[MiscellaneousSound] [varchar](500) NULL,
	[Wlan] [varchar](500) NULL,
	[Bluetooth] [varchar](500) NULL,
	[Gps] [varchar](500) NULL,
	[Nfc] [varchar](500) NULL,
	[Radio] [varchar](500) NULL,
	[Usb] [varchar](500) NULL,
	[Sensors] [varchar](500) NULL,
	[Messaging] [varchar](500) NULL,
	[Browser] [varchar](500) NULL,
	[Java] [varchar](500) NULL,
	[MiscellaneousFeatures] [varchar](500) NULL,
	[MiscellaneousBattery] [varchar](500) NULL,
	[StandBy] [varchar](500) NULL,
	[TalkTime] [varchar](500) NULL,
	[MusicPlay] [varchar](500) NULL,
	[Colours] [varchar](500) NULL,
	[Sar] [varchar](500) NULL,
	[SarEu] [varchar](500) NULL,
	[Price] [varchar](500) NULL,
	[BatteryTalkTime] [varchar](500) NULL,
	[BatteryMusicPlay] [varchar](500) NULL,
	[VideoPixel] [varchar](100) NULL,
	[MetaSearch] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mobile] PRIMARY KEY CLUSTERED 
(
	[MobileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Mobile]  WITH CHECK ADD  CONSTRAINT [FK_Mobile_Brand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brand] ([BrandId])
GO

ALTER TABLE [dbo].[Mobile] CHECK CONSTRAINT [FK_Mobile_Brand]
GO


