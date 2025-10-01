// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class SensorNotification
{
  [DBMethod("SensorNotification_AddSensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SensorNotification]\r\nWHERE NotificationID = @NotificationID\r\n  AND SensorID       = @SensorID\r\n  AND DatumIndex     = @DatumIndex;\r\n  \r\nINSERT INTO dbo.[SensorNotification] (SensorID, NotificationID, DatumIndex)\r\nVALUES (@SensorID, @NotificationID, @DatumIndex);\r\n\r\n")]
  [DBMethodBody(DBMS.SQLite, "DELETE FROM SensorNotification WHERE NotificationID = @NotificationID AND SensorID = @SensorID AND DatumIndex = @DatumIndex; INSERT INTO SensorNotification Values (@SensorID, @NotificationID, @DatumIndex)")]
  internal class AddSensor : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DatumIndex", typeof (int), DefaultValue = 0)]
    public int DatumIndex { get; private set; }

    public AddSensor(long notificationID, long sensorID, int datumindex = 0)
    {
      this.NotificationID = notificationID;
      this.SensorID = sensorID;
      this.DatumIndex = datumindex;
      this.Execute();
    }
  }

  [DBMethod("SensorNotification_RemoveSensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SensorNotification]\r\nWHERE NotificationID = @NotificationID\r\n  AND SensorID       = @SensorID\r\n  AND DatumIndex     = @DatumIndex;\r\n  \r\nUPDATE dbo.[Notification]\r\n  SET SensorID = NULL\r\nWHERE NotificationID = @NotificationID\r\n  AND SensorID       = @SensorID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "DELETE FROM SensorNotification WHERE NotificationID = @NotificationID AND SensorID = @SensorID AND DatumIndex = @DatumIndex")]
  internal class RemoveSensor : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DatumIndex", typeof (int), DefaultValue = 0)]
    public int DatumIndex { get; private set; }

    public RemoveSensor(long notificationID, long sensorID, int datumindex = 0)
    {
      this.NotificationID = notificationID;
      this.SensorID = sensorID;
      this.DatumIndex = datumindex;
      this.Execute();
    }
  }

  [DBMethod("SensorNotification_LoadByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @SnoozeTrigger IS NULL\r\n  SET @SnoozeTrigger = 0\r\n\r\nDECLARE @DatumIndex  INT\r\n\r\nIF @SnoozeTrigger = 1\r\nBEGIN \r\n\r\n  SELECT\r\n    *\r\n  FROM dbo.[Sensor] s WITH (NOLOCK)\r\n  JOIN dbo.[SensorNotification] sn  WITH (NOLOCK) ON sn.SensorID = s.SensorID\r\n  WHERE s.SensorID = @SensorID\r\n    AND sn.NotificationID = @NotificationID\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nJOIN dbo.[SensorNotification] sn  WITH (NOLOCK) ON sn.SensorID = s.SensorID\r\nWHERE NotificationID=@NotificationID AND sn.SensorID IN \r\n                                                      ( SELECT \r\n                                                          SensorID\r\n                                                        FROM dbo.[SensorNotification]  WITH (NOLOCK) \r\n                                                        WHERE NotificationID = @NotificationID\r\n                                                        UNION\r\n                                                        SELECT\r\n                                                          SensorID\r\n                                                        FROM dbo.[Notification]  WITH (NOLOCK) \r\n                                                        WHERE SensorID IS NOT NULL\r\n                                                          AND NotificationID = @NotificationID)\r\n  AND sn.DatumIndex IN (\r\n                        SELECT\r\n                          DatumIndex\r\n                        FROM dbo.[SensorNotification]  WITH (NOLOCK) \r\n                        WHERE SensorID=s.SensorID\r\n                          AND NotificationID=@NotificationID);\r\n\r\n\r\nEND\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT s.* FROM Notification n INNER JOIN Sensor s on s.SensorID = n.SensorID WHERE n.NotificationID = '@NotificationID'")]
  internal class LoadByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("SensorID", typeof (long), DefaultValue = null)]
    public long SensorID { get; private set; }

    [DBMethodParam("SnoozeTrigger", typeof (bool), DefaultValue = null)]
    public bool SnoozeTrigger { get; private set; }

    public List<SensorDatum> Result { get; private set; }

    public LoadByNotificationID(long notificationID, long sensorID, bool snoozeTrigger)
    {
      this.NotificationID = notificationID;
      this.SensorID = sensorID;
      this.SnoozeTrigger = snoozeTrigger;
      this.Result = new List<SensorDatum>();
      foreach (DataRow row in (InternalDataCollectionBase) this.ToDataTable().Rows)
        this.Result.Add(new SensorDatum(Monnit.Sensor.Load(row[nameof (SensorID)].ToLong()), row["DatumIndex"].ToInt(), row["SensorNotificationID"].ToLong()));
    }
  }

  [DBMethod("SensorNotification_DatumsForSensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  DatumIndex\r\nFROM dbo.[SensorNotification]\r\nWHERE NotificationID  = @NotificationID\r\n  AND SensorID        = @SensorID;\r\n")]
  internal class DatumsForSensor : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<int> Result { get; private set; }

    public DatumsForSensor(long notificationID, long sensorID)
    {
      this.NotificationID = notificationID;
      this.SensorID = sensorID;
      this.Result = new List<int>(this.ToDataTable().Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (row => row["DatumIndex"].ToInt())));
    }
  }
}
