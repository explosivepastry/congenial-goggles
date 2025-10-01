// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AvailableNotificationBySensorModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace iMonnit.Models;

public class AvailableNotificationBySensorModel
{
  private List<AvailableNotificationBySensorDetailsModel> _NotificationList = new List<AvailableNotificationBySensorDetailsModel>();

  public Notification Notification { get; set; }

  public List<AvailableNotificationBySensorDetailsModel> DetailsList
  {
    get => this._NotificationList;
    set => this._NotificationList = value;
  }

  public static List<AvailableNotificationBySensorModel> Load(Sensor sens)
  {
    if (sens == null)
      return (List<AvailableNotificationBySensorModel>) null;
    List<AvailableNotificationBySensorModel> source = new List<AvailableNotificationBySensorModel>();
    foreach (Notification notification in Notification.LoadByAccountID(sens.AccountID))
    {
      switch (notification.NotificationClass)
      {
        case eNotificationClass.Application:
          AvailableNotificationBySensorModel notificationBySensorModel1 = new AvailableNotificationBySensorModel();
          notificationBySensorModel1.Notification = notification;
          for (int di = 0; di < sens.GetDatumTypes().Count; di++)
          {
            if (notification.eDatumType == sens.getDatumType(di))
            {
              SensorDatum sensorDatum = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == di)).FirstOrDefault<SensorDatum>();
              notificationBySensorModel1.DetailsList.Add(new AvailableNotificationBySensorDetailsModel()
              {
                DatumIndex = di,
                SensorNotificationID = sensorDatum != null ? sensorDatum.SensorNotificationID : long.MinValue
              });
            }
            else if (notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (s => s.sens.SensorID == sens.SensorID && s.DatumIndex == di)).Count<SensorDatum>() > 0)
            {
              SensorDatum sensorDatum = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == di)).FirstOrDefault<SensorDatum>();
              notificationBySensorModel1.DetailsList.Add(new AvailableNotificationBySensorDetailsModel()
              {
                DatumIndex = di,
                SensorNotificationID = sensorDatum != null ? sensorDatum.SensorNotificationID : long.MinValue
              });
            }
          }
          if (notificationBySensorModel1.DetailsList.Count > 0)
          {
            source.Add(notificationBySensorModel1);
            break;
          }
          break;
        case eNotificationClass.Inactivity:
          SensorDatum sensorDatum1 = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == 0)).FirstOrDefault<SensorDatum>();
          source.Add(new AvailableNotificationBySensorModel()
          {
            Notification = notification,
            DetailsList = {
              new AvailableNotificationBySensorDetailsModel()
              {
                DatumIndex = 0,
                SensorNotificationID = sensorDatum1 != null ? sensorDatum1.SensorNotificationID : long.MinValue
              }
            }
          });
          break;
        case eNotificationClass.Low_Battery:
          if (sens.PowerSource.VoltageForZeroPercent != sens.PowerSource.VoltageForOneHundredPercent)
          {
            SensorDatum sensorDatum2 = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == 0)).FirstOrDefault<SensorDatum>();
            source.Add(new AvailableNotificationBySensorModel()
            {
              Notification = notification,
              DetailsList = {
                new AvailableNotificationBySensorDetailsModel()
                {
                  DatumIndex = 0,
                  SensorNotificationID = sensorDatum2 != null ? sensorDatum2.SensorNotificationID : long.MinValue
                }
              }
            });
            break;
          }
          break;
        case eNotificationClass.Advanced:
          AdvancedNotification advancedNotification = AdvancedNotification.Load(notification.AdvancedNotificationID);
          AvailableNotificationBySensorModel notificationBySensorModel2 = new AvailableNotificationBySensorModel();
          if (advancedNotification.CanAdd((object) sens))
          {
            SensorDatum sensorDatum3 = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == 0)).FirstOrDefault<SensorDatum>();
            notificationBySensorModel2.DetailsList.Add(new AvailableNotificationBySensorDetailsModel()
            {
              DatumIndex = 0,
              SensorNotificationID = sensorDatum3 != null ? sensorDatum3.SensorNotificationID : long.MinValue
            });
          }
          else
          {
            int datumIndex = 0;
            foreach (AppDatum appDatum in MonnitApplicationBase.GetAppDatums(sens.ApplicationID))
            {
              datumIndex++;
              if (advancedNotification.CanAdd((object) appDatum))
              {
                SensorDatum sensorDatum4 = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == datumIndex)).FirstOrDefault<SensorDatum>();
                notificationBySensorModel2.DetailsList.Add(new AvailableNotificationBySensorDetailsModel()
                {
                  DatumIndex = datumIndex,
                  SensorNotificationID = sensorDatum4 != null ? sensorDatum4.SensorNotificationID : long.MinValue
                });
              }
            }
          }
          notificationBySensorModel2.Notification = notification;
          source.Add(notificationBySensorModel2);
          break;
        case eNotificationClass.Timed:
          SensorDatum sensorDatum5 = notification.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID && m.DatumIndex == 0)).FirstOrDefault<SensorDatum>();
          source.Add(new AvailableNotificationBySensorModel()
          {
            Notification = notification,
            DetailsList = {
              new AvailableNotificationBySensorDetailsModel()
              {
                DatumIndex = 0,
                SensorNotificationID = sensorDatum5 != null ? sensorDatum5.SensorNotificationID : long.MinValue
              }
            }
          });
          break;
      }
    }
    return source.OrderByDescending<AvailableNotificationBySensorModel, int>((Func<AvailableNotificationBySensorModel, int>) (m => m.DetailsList.Where<AvailableNotificationBySensorDetailsModel>((Func<AvailableNotificationBySensorDetailsModel, bool>) (i => i.SensorNotificationID > 0L)).Count<AvailableNotificationBySensorDetailsModel>())).ThenBy<AvailableNotificationBySensorModel, string>((Func<AvailableNotificationBySensorModel, string>) (m2 => m2.Notification.Name)).ToList<AvailableNotificationBySensorModel>();
  }
}
