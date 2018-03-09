USE [mumbile]
GO

/****** Object:  View [dbo].[MobileRepairGrid]    Script Date: 08/03/2018 04:23:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[MobileRepairGrid]
AS 
SELECT 

MobileRepairId,
MobileNumber,
ModelName,
Description,
CouponCode,
MobileRepairStateId,
CreatedDateTime,
Company,
CountryId,
StateId,
City,
Address1,
Address2,
ZipPostalCode,
PhoneNumber,
LandMark,
District,
AlternateNumber,
AppointmentDate,
Comment,
AppointmentTime,
ISNULL(CONVERT(varchar, MR.MobileNumber ),'')+ISNULL(MR.ModelName, '')+ISNULL(MR.CouponCode, '')
+ ISNULL(CONVERT(varchar,CreatedDateTime, 101), '') 
	  + ISNULL(CONVERT(varchar,CreatedDateTime, 103), '') 
	  + ISNULL(CONVERT(varchar,CreatedDateTime, 105), '') 
	  + ISNULL(CONVERT(varchar,CreatedDateTime, 126), '')+ISNULL(MR.Company, '')
	  +ISNULL(MR.City, '')+ ISNULL(CONVERT(varchar,AppointmentDate, 101), '') 
	  + ISNULL(CONVERT(varchar,AppointmentDate, 103), '') 
	  + ISNULL(CONVERT(varchar,AppointmentDate, 105), '') 
	  + ISNULL(CONVERT(varchar,AppointmentDate, 126), '') AS SearchField

from MobileRepair MR


GO


