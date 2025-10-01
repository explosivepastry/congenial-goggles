USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CellGatewayExport]    Script Date: 6/24/2024 2:46:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CellGatewayExport]
AS

SELECT
  GatewayID,
  LastCommunicationDate,
  AccountNumber,
  TopLevelAccount,
  Name,
  GatewayTypeID,
  CellID
FROM Admin.dbo.vwGatewayMacAddress WITH (NOLOCK)
WHERE GatewayTypeID IN (
17,18,22,23,24,25,
26,27,30,32)

GO


