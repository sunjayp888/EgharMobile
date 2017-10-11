USE [Egharpay]
GO

/****** Object:  View [dbo].[MobileGrid]    Script Date: 11-10-2017 11:48:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[MobileGrid]
AS 
SELECT 

M.MobileId,
M.BrandId,
M.Name,
M.Technology,
M.Network2GBands,
M.Network3GBands,
M.Network4GBands,
M.Speed,
M.Gprs,
M.Edge,
M.Announced,
M.Status,
M.Dimensions,
M.Weight,
M.Sim,
M.MiscellaneousBody,
M.DisplayType,
M.DisplaySize,
M.DisplayResolution,
M.Multitouch,
M.Protection,
M.Os,
M.Chipset,
M.Cpu,
M.Gpu,
M.CardSlot,
M.InternalMemory,
M.PrimaryCamera,
M.CameraFeatures,
M.Video,
M.SecondaryCamera,
M.AlertTypes,
M.Loudspeaker,
M.Sound3Point5MmJack,
M.MiscellaneousSound,
M.Wlan,
M.Bluetooth,
M.Gps,
M.Nfc,
M.Radio,
M.Usb,
M.Sensors,
M.Messaging,
M.Browser,
M.Java,
M.MiscellaneousFeatures,
M.MiscellaneousBattery,
M.StandBy,
M.TalkTime,
M.MusicPlay,
M.Colours,
M.Sar,
M.SarEu,
M.Price,
M.MetaSearch,
ISNULL(M.Name, '')+ISNULL(M.Technology, '')+ISNULL(M.Network2GBands, '')+ISNULL(M.Network3GBands, '')+ISNULL(M.Network4GBands, '')
+ISNULL(M.Speed, '')+ISNULL(M.Gprs, '')+ISNULL(M.Edge, '')+ISNULL(M.Announced, '')+ISNULL(M.Status, '')
+ISNULL(M.Dimensions, '')+ISNULL(M.Weight, '')+ISNULL(M.Sim, '')+ISNULL(M.MiscellaneousBody, '')+ISNULL(M.DisplayType, '')
+ISNULL(M.DisplaySize, '')+ISNULL(M.DisplayResolution, '')+ISNULL(M.Multitouch, '')+ISNULL(M.Protection, '')+ISNULL(M.Os, '')
+ISNULL(M.Chipset, '')+ISNULL(M.Cpu, '')+ISNULL(M.Gpu, '')+ISNULL(M.CardSlot, '')+ISNULL(M.InternalMemory, '')
+ISNULL(M.PrimaryCamera, '')+ISNULL(M.CameraFeatures, '')+ISNULL(M.Video, '')+ISNULL(M.SecondaryCamera, '')+ISNULL(M.AlertTypes, '')
+ISNULL(M.Loudspeaker, '')+ISNULL(M.Sound3Point5MmJack, '')+ISNULL(M.MiscellaneousSound, '')+ISNULL(M.Wlan, '')+ISNULL(M.Bluetooth, '')
+ISNULL(M.Gps, '')+ISNULL(M.Nfc, '')+ISNULL(M.Radio, '')+ISNULL(M.Usb, '')+ISNULL(M.Sensors, '')
+ISNULL(M.Messaging, '')+ISNULL(M.Browser, '')+ISNULL(M.Java, '')+ISNULL(M.MiscellaneousFeatures, '')+ISNULL(M.MiscellaneousBattery, '')
+ISNULL(M.StandBy, '')+ISNULL(M.TalkTime, '')+ISNULL(M.MusicPlay, '')+ISNULL(M.Colours, '')+ISNULL(M.Sar, '')
+ISNULL(M.SarEu, '')+ISNULL(M.Price, '')+ISNULL(B.Name, '') AS SearchField

from Mobile M
inner join Brand B on B.BrandId=M.BrandId





GO


