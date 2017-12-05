USE [Egharpay]
GO

/****** Object:  View [dbo].[MobileGrid]    Script Date: 05-12-2017 09:46:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER VIEW [dbo].[MobileGrid]
AS 
SELECT 
M.MobileId,
M.BrandId,
BrandName=B.Name,
M.Name,
M.ReleasedDate,
M.BodyDimension,
M.OS,
M.Storage,
M.DisplayResolution,
M.CameraPixel,
M.RAM,
M.Chipset,
M.BatterySize,
M.BatteryType,
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
M.Multitouch,
M.Protection,
M.Cpu,
M.Gpu,
M.CardSlot,
M.InternalMemory,
M.PrimaryCameraDescription,
M.CameraFeatures,
M.Video,
M.SecondaryCameraDescription,
M.AlertTypes,
M.Loudspeaker,
M.Sound3Point5MmJack,
MiscellaneousSound,
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
M.BatteryTalkTime,
M.BatteryMusicPlay,
M.VideoPixel,
M.AllImage,
M.MetaSearch,
M.IsLatest,
M.CreatedDateTime,
M.ProfileImagePath,
M.IsDeviceInStore,
M.PrimaryCamera,
M.SecondaryCamera,
ShortDescription=ISNULL(M.Chipset,'')+' '+ISNULL(M.Cpu,'')+', Internal Memory : '+ISNULL(M.InternalMemory,'')+', Primary Camera : '+ISNULL(CONVERT(varchar, M.PrimaryCamera),'')+', Secondary Camera :'+ISNULL(CONVERT(varchar, M.PrimaryCamera),''),
ISNULL(M.Name, '')+ISNULL(M.Technology, '')+ISNULL(M.Network2GBands, '')+ISNULL(M.Network3GBands, '')+ISNULL(M.Network4GBands, '')
+ISNULL(M.Speed, '')+ISNULL(M.Gprs, '')+ISNULL(M.Edge, '')+ISNULL(M.Announced, '')+ISNULL(M.Status, '')
+ISNULL(M.Dimensions, '')+ISNULL(M.Weight, '')+ISNULL(M.Sim, '')+ISNULL(M.MiscellaneousBody, '')+ISNULL(M.DisplayType, '')
+ISNULL(M.DisplaySize, '')+ISNULL(M.DisplayResolution, '')+ISNULL(M.Multitouch, '')+ISNULL(M.Protection, '')+ISNULL(M.Os, '')
+ISNULL(M.Chipset, '')+ISNULL(M.Cpu, '')+ISNULL(M.Gpu, '')+ISNULL(M.CardSlot, '')+ISNULL(M.InternalMemory, '')
+ISNULL(CONVERT(varchar, M.PrimaryCamera ),'')+ISNULL(M.CameraFeatures, '')+ISNULL(M.Video, '')+ISNULL(CONVERT(varchar, M.SecondaryCamera ),'')+ISNULL(M.AlertTypes, '')
+ISNULL(M.Loudspeaker, '')+ISNULL(M.Sound3Point5MmJack, '')+ISNULL(M.MiscellaneousSound, '')+ISNULL(M.Wlan, '')+ISNULL(M.Bluetooth, '')
+ISNULL(M.Gps, '')+ISNULL(M.Nfc, '')+ISNULL(M.Radio, '')+ISNULL(M.Usb, '')+ISNULL(M.Sensors, '')
+ISNULL(M.Messaging, '')+ISNULL(M.Browser, '')+ISNULL(M.Java, '')+ISNULL(M.MiscellaneousFeatures, '')+ISNULL(M.MiscellaneousBattery, '')
+ISNULL(M.StandBy, '')+ISNULL(M.TalkTime, '')+ISNULL(M.MusicPlay, '')+ISNULL(M.Colours, '')+ISNULL(M.Sar, '')
+ISNULL(M.SarEu, '')+ISNULL(M.Price, '')+ISNULL(B.Name, '') AS SearchField

from Mobile M
inner join Brand B on B.BrandId=M.BrandId









GO


