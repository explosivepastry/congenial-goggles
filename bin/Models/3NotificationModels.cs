// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ConfigureSensorNotificationModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace iMonnit.Models;

public class ConfigureSensorNotificationModel
{
  private List<SensorNoficationModel> _SensorList = new List<SensorNoficationModel>();
  private List<GatewayNoficationModel> _GatewayList = new List<GatewayNoficationModel>();

  public ConfigureSensorNotificationModel(Notification noti)
  {
    this.NotificationID = noti.NotificationID;
    AdvancedNotification advancedNotification = (AdvancedNotification) null;
    if (noti.NotificationClass == eNotificationClass.Advanced)
      advancedNotification = AdvancedNotification.Load(noti.AdvancedNotificationID);
    foreach (Sensor sensor in Sensor.LoadByAccountID(noti.AccountID))
    {
      Sensor sen = sensor;
      List<eDatumType> datumTypes = sen.GetDatumTypes();
      for (int di = 0; di < sen.GetDatumTypes().Count; di++)
      {
        if (sen.CSNetID > 0L && (noti.NotificationClass != eNotificationClass.Application || noti.eDatumType == datumTypes[di] || noti.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (s => s.sens.SensorID == sen.SensorID && s.DatumIndex == di)).Count<SensorDatum>() != 0) && (noti.NotificationClass != eNotificationClass.Low_Battery || sen.PowerSource.VoltageForZeroPercent != sen.PowerSource.VoltageForOneHundredPercent) && (noti.NotificationClass != eNotificationClass.Advanced || advancedNotification != null && (advancedNotification.HasSensorList || !advancedNotification.HasGatewayList) && (!advancedNotification.UseDatums || advancedNotification.eDatumType == datumTypes[di])))
        {
          bool flag = false;
          foreach (SensorDatum sensorDatum in noti.SensorsThatNotify)
          {
            if (sensorDatum.sens.SensorID == sen.SensorID && sensorDatum.DatumIndex == di)
            {
              flag = true;
              break;
            }
          }
          if (noti.NotificationClass == eNotificationClass.Application)
            this.SensorList.Add(new SensorNoficationModel()
            {
              Sensor = sen,
              Notify = flag,
              DatumIndex = di,
              NotificationClass = noti.NotificationClass
            });
          else if (noti.NotificationClass == eNotificationClass.Advanced && advancedNotification.UseDatums)
            this.SensorList.Add(new SensorNoficationModel()
            {
              Sensor = sen,
              Notify = flag,
              DatumIndex = di,
              NotificationClass = noti.NotificationClass
            });
          else if (!this.SensorList.Exists((Predicate<SensorNoficationModel>) (sensnof => sensnof.Sensor == sen)))
            this.SensorList.Add(new SensorNoficationModel()
            {
              Sensor = sen,
              Notify = flag,
              DatumIndex = di,
              NotificationClass = noti.NotificationClass
            });
        }
      }
      if (datumTypes.Count == 0 && sen.CSNetID > 0L && noti.NotificationClass != eNotificationClass.Application && (noti.NotificationClass != eNotificationClass.Low_Battery || sen.PowerSource.VoltageForZeroPercent != sen.PowerSource.VoltageForOneHundredPercent) && (advancedNotification == null || advancedNotification.HasSensorList || !advancedNotification.HasGatewayList))
      {
        bool flag = false;
        foreach (SensorDatum sensorDatum in noti.SensorsThatNotify)
        {
          if (sensorDatum.sens.SensorID == sen.SensorID)
          {
            flag = true;
            break;
          }
        }
        if (!this.SensorList.Exists((Predicate<SensorNoficationModel>) (sensnof => sensnof.Sensor == sen)))
          this.SensorList.Add(new SensorNoficationModel()
          {
            Sensor = sen,
            Notify = flag,
            DatumIndex = 0,
            NotificationClass = noti.NotificationClass
          });
        else
          break;
      }
    }
    if (noti.NotificationClass == eNotificationClass.Application)
      return;
    foreach (Gateway gateway1 in Gateway.LoadByAccountID(noti.AccountID))
    {
      if (gateway1.SensorID <= 0L && gateway1.GatewayTypeID != 11L && (advancedNotification == null || !advancedNotification.HasSensorList || advancedNotification.HasGatewayList) && (noti.NotificationClass != eNotificationClass.Low_Battery || gateway1.PowerSource != null && gateway1.PowerSource.VoltageForZeroPercent != gateway1.PowerSource.VoltageForOneHundredPercent))
      {
        bool flag = false;
        foreach (Gateway gateway2 in noti.GatewaysThatNotify)
        {
          if (gateway2.GatewayID == gateway1.GatewayID)
          {
            flag = true;
            break;
          }
        }
        this.GatewayList.Add(new GatewayNoficationModel()
        {
          Gateway = gateway1,
          Notify = flag
        });
      }
    }
  }

  public long NotificationID { get; set; }

  public List<SensorNoficationModel> SensorList => this._SensorList;

  public List<GatewayNoficationModel> GatewayList => this._GatewayList;
}
