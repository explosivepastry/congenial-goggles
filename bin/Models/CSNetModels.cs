// Decompiled with JetBrains decompiler
// Type: Data.RapidUpdate
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;

#nullable disable
namespace Data;

internal class RapidUpdate
{
  [DBMethod("RapidUpdate_AddSensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @DoneUpdatingNetworkID BIGINT = 2;\r\nDECLARE @DoneUpdatingAccountID BIGINT = 1;\r\nDECLARE @AccountID BIGINT;\r\nDECLARE @SKU VARCHAR(255);\r\n\r\n--Remove existing Sensors from Network\r\nUPDATE sensor SET CSNetID = @DoneUpdatingNetworkID, AccountID = @DoneUpdatingAccountID WHERE CSNetID = @CSNetID;\r\n\r\nSELECT @AccountID = AccountID FROM CSNet WHERE CSNetID = @CSNetID;\r\nSELECT @SKU = ISNULL(SKU,'') FROM dbo.[Sensor] WHERE SensorID = @SensorID;\r\n\r\n\r\nIF @SKU = 'RYS2-9-W2-DC-CF-L01'\r\nBEGIN\r\n\r\n\t--Dry Contact\r\n\r\n\tUPDATE sensor SET \r\n\tSendFirmwareUpdate = 1, \r\n\tSensorFirmwareID = 25623, \r\n\tFirmwareUpdateInProgress = 0, \r\n\tFirmwareDownloadComplete = 0,\r\n\tCSNetID = @CSNetID, \r\n\tAccountID=@AccountID, \r\n\tLastCommunicationdate = GetUTCDate(), \r\n\tStartDate = GetUTCDate()\r\n\tWHERE SensorID = @SensorID\r\n\r\nEND\r\n\r\nIF @SKU = 'RYS2-9-W2-OC-ST'\r\nBEGIN\r\n\r\n\t--Open Closed\r\n\r\n\tUPDATE sensor SET \r\n\tSKU = 'RYS2-9-W2-DC-CF-L01',\r\n\tSendFirmwareUpdate = 1, \r\n\tSensorFirmwareID = 25623, \r\n\tFirmwareUpdateInProgress = 0, \r\n\tFirmwareDownloadComplete = 0,\r\n\tCSNetID = @CSNetID, \r\n\tAccountID=@AccountID, \r\n\tLastCommunicationdate = GetUTCDate(), \r\n\tStartDate = GetUTCDate()\r\n\tWHERE SensorID = @SensorID\r\n\r\nEND\r\n\r\nIF @SKU = 'RYS2-9-W2-AC-TD'\r\nBEGIN\r\n\r\n\t--Tilt Detect\r\n\r\n\tUPDATE sensor SET \r\n\tSendFirmwareUpdate = 1, \r\n\t--SensorFirmwareID = {TiltDetectFirmwareID}, \r\n\tFirmwareUpdateInProgress = 0, \r\n\tFirmwareDownloadComplete = 0,\r\n\tCSNetID = @CSNetID, \r\n\tAccountID=@AccountID, \r\n\tLastCommunicationdate = GetUTCDate(), \r\n\tStartDate = GetUTCDate()\r\n\tWHERE SensorID = @SensorID\r\n\r\n\r\nEND\r\n\r\nIF @SKU = 'RYS2-9-W2-AC-GM'\r\nBEGIN\r\n\r\n\t--Accell Max/Avg\r\n\r\n\tUPDATE sensor SET \r\n\tSendFirmwareUpdate = 1, \r\n\tSensorFirmwareID = 25643, \r\n\tFirmwareUpdateInProgress = 0, \r\n\tFirmwareDownloadComplete = 0,\r\n\tCSNetID = @CSNetID, \r\n\tAccountID=@AccountID, \r\n\tLastCommunicationdate = GetUTCDate(), \r\n\tStartDate = GetUTCDate()\r\n\tWHERE SensorID = @SensorID\r\n\r\nEND\r\n\r\n")]
  internal class AddSensor : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public bool Result { get; private set; }

    public AddSensor(long sensorID, long networkID)
    {
      this.SensorID = sensorID;
      this.CSNetID = networkID;
      try
      {
        this.Execute();
        this.Result = true;
      }
      catch
      {
        this.Result = false;
      }
    }
  }
}
