// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ConfigureNotifierSensorDataModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class ConfigureNotifierSensorDataModel
{
  private List<NotifySensorDataModel> _SensorList = new List<NotifySensorDataModel>();

  public ConfigureNotifierSensorDataModel(Sensor noti)
  {
    this.SensorID = noti.SensorID;
    List<Sensor> sensorList = Sensor.LoadByAccountID(noti.AccountID);
    List<NotifierToSensorData> sensorByNotifier = NotifierToSensorData.GetAllSensorByNotifier(noti.SensorID);
    foreach (Sensor sensor in sensorList)
    {
      bool flag = false;
      foreach (NotifierToSensorData notifierToSensorData in sensorByNotifier)
      {
        if (sensor.SensorID == notifierToSensorData.SensorID)
        {
          flag = true;
          break;
        }
      }
      this._SensorList.Add(new NotifySensorDataModel()
      {
        sens = sensor,
        isSendingSensorData = flag
      });
    }
  }

  public long SensorID { get; set; }

  public List<NotifySensorDataModel> SensorList => this._SensorList;
}
