USE [mumbile]
GO

/****** Object:  Table [dbo].[PersonnelAddress]    Script Date: 16-01-2018 08:11:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PersonnelAddress](
	[PersonnelAddressId] [int] IDENTITY(1,1) NOT NULL,
	[AddressId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
 CONSTRAINT [PK_PersonnelAddress] PRIMARY KEY CLUSTERED 
(
	[PersonnelAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


