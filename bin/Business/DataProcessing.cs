// Decompiled with JetBrains decompiler
// Type: Monnit.DataProcessing
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

public class DataProcessing
{
  public static void ProcessNotifications(DataMessage msg, PacketCache packetCache)
  {
    try
    {
      Sensor sens = packetCache.LoadSensor(msg.SensorID);
      DateTime notificationDate = sens.resumeNotificationDate;
      if (false)
      {
        sens.resumeNotificationDate = new DateTime(2010, 1, 1);
        sens.Save();
      }
      if (!sens.IsActive || sens.IsDeleted || sens.CSNetID < 0L || sens.isPaused() || !sens.SendNotification)
        return;
      sens.IsSleeping = false;
      if (msg.DataMessageGUID == Guid.Empty)
        msg.Save();
      foreach (Notification sensorNotification1 in packetCache.LoadSensorNotifications(msg.SensorID))
      {
        if (sensorNotification1.IsActive)
        {
          bool flag1 = sensorNotification1.IsInNotificationWindow(TimeZone.GetLocalTimeById(DateTime.UtcNow, Account.Load(sens.AccountID).TimeZoneID));
          SensorNotification sensorNotification2 = SensorNotification.LoadBySensorIDAndNotificationID(sens.SensorID, sensorNotification1.NotificationID);
          try
          {
            switch (sensorNotification1.NotificationClass)
            {
              case eNotificationClass.Application:
                if (sensorNotification1.eDatumType < eDatumType.BooleanData && sensorNotification1.ApplicationID >= 0L)
                {
                  MonnitApplication.Load(sensorNotification1.ApplicationID);
                  sensorNotification1.eDatumType = sens.GetDatumTypes()[0];
                  sensorNotification1.Save();
                }
                MonnitApplicationBase monnitApplicationBase1 = MonnitApplicationBase.LoadMonnitApplication(sens.FirmwareVersion, msg.Data, sens.ApplicationID, sens.SensorID);
                MonnitApplicationBase monnitApplicationBase2 = MonnitApplicationBase.LoadMonnitApplication(sensorNotification1.Version, sensorNotification1.CompareValue, monnitApplicationBase1.ApplicationID);
                bool flag2 = false;
                List<SensorDatum> sensorDatumList = new List<SensorDatum>(SensorNotification.LoadByNotificationID(sensorNotification1.NotificationID, sens.SensorID, sensorNotification1.ApplySnoozeByTriggerDevice).Where<SensorDatum>((Func<SensorDatum, bool>) (sdat => sdat.sens.SensorID == sens.SensorID)));
                if (monnitApplicationBase1.IsValid && sensorDatumList.Count > 0 && monnitApplicationBase2.Datums.Count > sensorDatumList[0].DatumIndex)
                {
                  DataTypeBase data1 = monnitApplicationBase2.Datums[sensorDatumList[0].DatumIndex].data;
                  using (List<SensorDatum>.Enumerator enumerator = sensorDatumList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      SensorDatum current = enumerator.Current;
                      flag2 = false;
                      int datumIndex = current.DatumIndex;
                      DataTypeBase data2 = monnitApplicationBase1.Datums[datumIndex].data;
                      if (data2.datatype != data1.datatype)
                      {
                        new Exception($"DataProcessing.ProcessNotifications DatumType Verification SensorDatum: {monnitApplicationBase1.Datums[datumIndex].etype.ToString()} Does not match Notifiation Datum: {monnitApplicationBase2.Datums[sensorDatumList[0].DatumIndex].etype.ToString()} SensorNotifiationID: {current.SensorNotificationID}").Log();
                      }
                      else
                      {
                        bool flag3;
                        switch (sensorNotification1.CompareType)
                        {
                          case eCompareType.Equal:
                            flag3 = data2.Equals(data1);
                            break;
                          case eCompareType.Not_Equal:
                            flag3 = !data2.Equals(data1);
                            break;
                          case eCompareType.Greater_Than:
                            flag3 = data2 > data1;
                            break;
                          case eCompareType.Less_Than:
                            flag3 = data2 < data1;
                            break;
                          case eCompareType.Greater_Than_or_Equal:
                            flag3 = data2 >= data1;
                            break;
                          case eCompareType.Less_Than_or_Equal:
                            flag3 = data2 <= data1;
                            break;
                          default:
                            flag3 = false;
                            break;
                        }
                        SensorNotification sensorNotification3 = BaseDBObject.Load<SensorNotification>(current.SensorNotificationID);
                        if (flag3)
                        {
                          try
                          {
                            if (!packetCache.NotifyingOn.ContainsKey(current.SensorNotificationID))
                              packetCache.NotifyingOn.Add(current.SensorNotificationID, sens.GetDatumName(datumIndex));
                            if (!msg.MeetsNotificationRequirement)
                            {
                              msg.MeetsNotificationRequirement = true;
                              msg.Save();
                            }
                            if (flag1)
                            {
                              sensorNotification3.TriggerDate = DateTime.UtcNow;
                              sensorNotification3.Save();
                              sensorNotification1.RecordNotification(packetCache, false, monnitApplicationBase1.NotificationString, DateTime.UtcNow, sens, (Gateway) null, current.SensorNotificationID, long.MinValue, msg);
                            }
                          }
                          catch (Exception ex)
                          {
                            ex.Log($"DataProcessing.ProcessDataMessage Notification failed to record notificationID: {sensorNotification1.NotificationID} ");
                          }
                        }
                        else
                        {
                          try
                          {
                            foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(sensorNotification1.NotificationID))
                            {
                              try
                              {
                                if (notificationTriggered.SensorNotificationID == sensorNotification3.SensorNotificationID)
                                {
                                  if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                                  {
                                    notificationTriggered.AcknowledgedBy = long.MinValue;
                                    notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                                    notificationTriggered.AcknowledgeMethod = "System_Auto";
                                  }
                                  if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                                    notificationTriggered.resetTime = DateTime.UtcNow;
                                  notificationTriggered.Save();
                                }
                              }
                              catch (Exception ex)
                              {
                                ex.Log($"DataProcessing.ProcessDataMessage.ApplicationNotification Condition not met. Reset Failed , NotificationTriggeredID: {notificationTriggered.NotificationTriggeredID} ");
                              }
                            }
                            sensorNotification3.TriggerDate = DateTime.MinValue;
                            sensorNotification3.Save();
                          }
                          catch (Exception ex)
                          {
                            ex.Log($"DataProcessing.ProcessDataMessage.ApplicationNotification.SensorNotification Save failed , SensorNotificationID: {sensorNotification3.SensorNotificationID} ");
                          }
                        }
                      }
                    }
                    break;
                  }
                }
                break;
              case eNotificationClass.Inactivity:
                foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(sensorNotification1.NotificationID))
                {
                  if (notificationTriggered.SensorNotificationID == sensorNotification2.SensorNotificationID)
                  {
                    if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                    {
                      notificationTriggered.AcknowledgedBy = long.MinValue;
                      notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                      notificationTriggered.AcknowledgeMethod = "System_Auto";
                    }
                    if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                      notificationTriggered.resetTime = DateTime.UtcNow;
                    notificationTriggered.Save();
                  }
                }
                sensorNotification2.TriggerDate = DateTime.MinValue;
                sensorNotification2.Save();
                break;
              case eNotificationClass.Low_Battery:
                if ((double) msg.Battery < sensorNotification1.CompareValue.ToDouble())
                {
                  try
                  {
                    if (!msg.MeetsNotificationRequirement)
                    {
                      msg.MeetsNotificationRequirement = true;
                      msg.Save();
                    }
                    packetCache.NotifyingOn.Add(sensorNotification1.NotificationID, "Low Battery");
                    if (flag1)
                    {
                      sensorNotification2.TriggerDate = DateTime.UtcNow;
                      sensorNotification2.Save();
                      sensorNotification1.RecordNotification(packetCache, false, "Battery Level: " + msg.Battery.ToString(), msg.MessageDate, sens, (Gateway) null, sensorNotification2.SensorNotificationID, long.MinValue, msg);
                      break;
                    }
                    break;
                  }
                  catch
                  {
                    break;
                  }
                }
                else
                {
                  foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(sensorNotification1.NotificationID))
                  {
                    if (notificationTriggered.SensorNotificationID == sensorNotification2.SensorNotificationID)
                    {
                      if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                      {
                        notificationTriggered.AcknowledgedBy = long.MinValue;
                        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                        notificationTriggered.AcknowledgeMethod = "System_Auto";
                      }
                      if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                        notificationTriggered.resetTime = DateTime.UtcNow;
                      notificationTriggered.Save();
                    }
                  }
                  sensorNotification2.TriggerDate = DateTime.MinValue;
                  sensorNotification2.Save();
                  break;
                }
              case eNotificationClass.Advanced:
                if (sensorNotification1.AdvancedNotificationID > long.MinValue)
                {
                  AdvancedNotification advancedNotification = AdvancedNotification.Load(sensorNotification1.AdvancedNotificationID);
                  if (advancedNotification.AdvancedNotificationType != eAdvancedNotificationType.AutomatedSchedule)
                  {
                    string AdvancedNotificationString = string.Empty;
                    if (advancedNotification.Evaluate(sensorNotification1, sens.SensorID, msg.DataMessageGUID, out AdvancedNotificationString))
                    {
                      try
                      {
                        if (!msg.MeetsNotificationRequirement)
                        {
                          msg.MeetsNotificationRequirement = true;
                          msg.Save();
                        }
                        string reading = $"{AdvancedNotificationString} <br />Current Reading: {msg.DisplayData}";
                        if (flag1)
                        {
                          sensorNotification2.TriggerDate = DateTime.UtcNow;
                          sensorNotification2.Save();
                          sensorNotification1.RecordNotification(packetCache, false, reading, msg.MessageDate, sens, (Gateway) null, sensorNotification2.SensorNotificationID, long.MinValue, msg);
                        }
                      }
                      catch
                      {
                      }
                    }
                    else
                    {
                      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(sensorNotification1.NotificationID))
                      {
                        if (notificationTriggered.SensorNotificationID == sensorNotification2.SensorNotificationID)
                        {
                          if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                          {
                            notificationTriggered.AcknowledgedBy = long.MinValue;
                            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                            notificationTriggered.AcknowledgeMethod = "System_Auto";
                          }
                          if (sensorNotification1.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                            notificationTriggered.resetTime = DateTime.UtcNow;
                          notificationTriggered.Save();
                        }
                      }
                      sensorNotification2.TriggerDate = DateTime.MinValue;
                      sensorNotification2.Save();
                    }
                  }
                  break;
                }
                break;
            }
          }
          catch (TimeoutException ex)
          {
            ex.Log($"iMonnitServer.DataProcessing.ProcessDataMessage NotificationID: {sensorNotification1.NotificationID} SensorID: {msg.SensorID} ");
          }
          catch (Exception ex)
          {
            ex.Log($"iMonnitServer.DataProcessing.ProcessDataMessage NotificationID: {sensorNotification1.NotificationID} SensorID: {msg.SensorID}  Failed notification! ");
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("iMonnitServer.DataProcessing.ProcessDataMessage");
    }
  }

  public static void CheckNotifierDataSubscription(DataMessage msg, Sensor sens)
  {
    foreach (NotifierToSensorData notifierToSensorData in NotifierToSensorData.GetAllNotifiersBySensorID(sens.SensorID))
    {
      try
      {
        Sensor Device = Sensor.Load(notifierToSensorData.NotifierID);
        Notification.RecordAVDisplay(msg.DisplayData, sens, (Gateway) null, Device);
      }
      catch (Exception ex)
      {
        ex.Log("CheckNotifierDataSubscription Failed SensorID: " + sens.SensorID.ToString());
      }
    }
  }

  public static void SensorFirmrwareUpdated(Sensor sensor)
  {
    try
    {
      sensor.SendFirmwareUpdate = false;
      sensor.FirmwareUpdateInProgress = true;
      sensor.FirmwareDownloadComplete = false;
      sensor.Save();
      foreach (OTARequestSensor otaRequestSensor in OTARequestSensor.LoadActiveBySensorID(sensor.SensorID))
      {
        otaRequestSensor.Status = eOTAStatus.Processing;
        otaRequestSensor.StartDate = DateTime.UtcNow;
        ++otaRequestSensor.AttemptCount;
        otaRequestSensor.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log("MonitorService.SensorFirmrwareUpdated");
    }
  }

  public static void BSNFirmrwareUpdated(Gateway gate)
  {
    try
    {
      gate.UpdateRadioFirmware = false;
      gate.RadioFirmwareUpdateInProgress = true;
      gate.Save();
    }
    catch (Exception ex)
    {
      ex.Log("MonitorService.BSNFirmrwareUpdated failed to set update radio firmware to false and save");
    }
  }

  public static void SensorFirmrwareTransferStatus(Sensor sensor, int status)
  {
    if (status != 0)
      return;
    sensor.SendFirmwareUpdate = false;
    sensor.FirmwareUpdateInProgress = true;
    sensor.FirmwareDownloadComplete = true;
    sensor.Save();
    foreach (OTARequestSensor otaRequestSensor in OTARequestSensor.LoadActiveBySensorID(sensor.SensorID))
    {
      otaRequestSensor.Status = eOTAStatus.Processing;
      if (otaRequestSensor.StartDate == DateTime.MinValue)
        otaRequestSensor.StartDate = DateTime.UtcNow;
      otaRequestSensor.DownloadedDate = DateTime.UtcNow;
      otaRequestSensor.Save();
    }
  }

  public static void SensorHasBeenUpdated(Sensor sensor, int configSection)
  {
    try
    {
      if (sensor == null)
        return;
      sensor.IsSleeping = false;
      switch (configSection)
      {
        case 12:
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.Save();
          break;
        case 14:
          sensor.PendingActionControlCommand = false;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = false;
          sensor.Save();
          break;
        case 24:
          sensor.GeneralConfigDirty = false;
          sensor.Save();
          break;
        case 25:
          sensor.GeneralConfig2Dirty = false;
          sensor.Save();
          break;
        case 27:
          sensor.GeneralConfig3Dirty = false;
          sensor.Save();
          break;
        case 28:
          sensor.PendingActionControlCommand = false;
          sensor.ProfileConfigDirty = false;
          sensor.Save();
          break;
        case 29:
          sensor.PendingActionControlCommand = false;
          sensor.ProfileConfig2Dirty = false;
          sensor.Save();
          break;
        default:
          sensor.MarkClean(false);
          sensor.Save();
          break;
      }
    }
    catch (Exception ex)
    {
      ex.Log("MonitorService.SensorHasBeenUpdated");
    }
  }

  public static void UploadConfigSection(
    Sensor sensor,
    int configSection,
    byte[] configData,
    long? CableID)
  {
    try
    {
      if (sensor == null)
        return;
      sensor.IsSleeping = false;
      if (new Version(sensor.FirmwareVersion) < new Version("2.0") && !sensor.IsWiFiSensor && !sensor.GenerationType.ToUpper().Contains("GEN2"))
      {
        if (configSection == 14)
        {
          sensor.FillProfileConfig(configData);
          sensor.ProfileConfigDirty = false;
          sensor.PendingActionControlCommand = false;
          sensor.ReadProfileConfig1 = false;
          sensor.ReadProfileConfig2 = false;
          sensor.Save();
          if (sensor.ProfileConfigDirty)
          {
            sensor.ProfileConfigDirty = false;
            sensor.Save();
          }
        }
      }
      else
      {
        int num;
        if (CableID.HasValue)
        {
          long cableId = sensor.CableID;
          long? nullable = CableID;
          long valueOrDefault = nullable.GetValueOrDefault();
          num = cableId == valueOrDefault & nullable.HasValue ? 1 : 0;
        }
        else
          num = 1;
        if (num != 0)
        {
          switch (configSection)
          {
            case 24:
              sensor.FillGeneralConfig1(configData);
              sensor.GeneralConfigDirty = false;
              sensor.ReadGeneralConfig1 = false;
              sensor.Save();
              if (sensor.GeneralConfigDirty)
              {
                sensor.GeneralConfigDirty = false;
                sensor.Save();
                break;
              }
              break;
            case 25:
              sensor.FillGeneralConfig2(configData);
              sensor.GeneralConfig2Dirty = false;
              sensor.ReadGeneralConfig2 = false;
              sensor.Save();
              if (sensor.GeneralConfig2Dirty)
              {
                sensor.GeneralConfig2Dirty = false;
                sensor.Save();
                break;
              }
              break;
            case 27:
              sensor.FillGeneralConfig3(configData);
              sensor.GeneralConfig3Dirty = false;
              sensor.ReadGeneralConfig3 = false;
              sensor.Save();
              if (sensor.GeneralConfig3Dirty)
              {
                sensor.GeneralConfig3Dirty = false;
                sensor.Save();
                break;
              }
              break;
            case 28:
              sensor.FillProfileConfig1(configData);
              sensor.ProfileConfigDirty = false;
              sensor.PendingActionControlCommand = false;
              sensor.ReadProfileConfig1 = false;
              sensor.Save();
              if (sensor.ProfileConfigDirty)
              {
                sensor.ProfileConfigDirty = false;
                sensor.Save();
                break;
              }
              break;
            case 29:
              sensor.FillProfileConfig2(configData);
              sensor.ProfileConfig2Dirty = false;
              sensor.PendingActionControlCommand = false;
              sensor.ReadProfileConfig2 = false;
              sensor.Save();
              if (sensor.ProfileConfig2Dirty)
              {
                sensor.ProfileConfig2Dirty = false;
                sensor.Save();
                break;
              }
              break;
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("MonitorService.UploadConfigSection");
    }
  }

  public static void ActionControlComplete(
    Sensor sensor,
    long? CableID,
    int actionCommand,
    bool success,
    byte[] data)
  {
    if (sensor == null)
      return;
    if (actionCommand == (int) byte.MaxValue)
    {
      sensor.IsSleeping = true;
      sensor.Save();
    }
    else
    {
      sensor.IsSleeping = false;
      if (actionCommand == 101)
      {
        sensor.SensorPrintDirty = false;
        sensor.Save();
      }
      else
      {
        int num;
        if (CableID.HasValue)
        {
          long cableId = sensor.CableID;
          long? nullable = CableID;
          long valueOrDefault = nullable.GetValueOrDefault();
          num = cableId == valueOrDefault & nullable.HasValue ? 1 : 0;
        }
        else
          num = 1;
        if (num != 0)
        {
          MonnitApplicationBase.ClearPendingActionControlCommand(sensor, (string) null, actionCommand, success, data);
          sensor.Save();
        }
      }
    }
  }

  public static void SensorStatusMessage(
    Sensor sensor,
    ushort sensorType,
    int status,
    long cableID,
    int cableMajorRevision,
    int cableMinorRevision)
  {
    if (sensor == null || !sensor.IsCableEnabled)
      return;
    bool flag = false;
    if (sensorType != ushort.MaxValue && sensor.ApplicationID != (long) sensorType)
    {
      sensor.ApplicationID = (long) sensorType;
      sensor.SetDefaults(false);
      sensor.SetCustomCompanyDefaults(false);
      flag = true;
    }
    if (cableID >= 0L)
    {
      Cable cable = Cable.Load(cableID);
      if (cable == null)
      {
        cable = MonnitUtil.LookUpCable(cableID);
        cable?.ForceInsert();
      }
      if (cable != null && sensor.CableID != cableID)
      {
        sensor.UpdateCable(cableID);
        sensor.ProfileConfigDirty = true;
        sensor.ProfileConfig2Dirty = true;
        flag = true;
      }
    }
    if (flag)
      sensor.Save();
  }

  public static void ParentMessage(Sensor sensor, long parentID, byte[] payload)
  {
    if (sensor != null)
    {
      if (payload.Length == 4 && new Version(sensor.FirmwareVersion) >= new Version(2, 5, 0, 0))
        sensor.FirmwareVersion = $"{(ValueType) (uint) payload[0]}.{(ValueType) (uint) payload[1]}.{(ValueType) (uint) payload[2]}.{(ValueType) (uint) payload[3]}";
      if (payload.Length >= 12)
      {
        sensor.ApplicationID = (long) BitConverter.ToUInt32(payload, 0);
        bool flag = false;
        string str = $"{BitConverter.ToUInt16(payload, 4)}.{BitConverter.ToUInt16(payload, 6)}.{BitConverter.ToUInt16(payload, 8)}.{BitConverter.ToUInt16(payload, 10)}";
        if (sensor.FirmwareVersion != str)
        {
          sensor.FirmwareVersion = str;
          sensor.GeneralConfigDirty = true;
          sensor.GeneralConfig2Dirty = true;
          sensor.GeneralConfig3Dirty = true;
          sensor.ProfileConfigDirty = true;
          sensor.ProfileConfig2Dirty = true;
          flag = true;
        }
        foreach (OTARequestSensor otaRequestSensor in OTARequestSensor.LoadActiveBySensorID(sensor.SensorID))
        {
          if (flag)
          {
            otaRequestSensor.Status = eOTAStatus.Completed;
            otaRequestSensor.CompletedDate = DateTime.UtcNow;
          }
          else if (otaRequestSensor.AttemptCount > 3)
          {
            otaRequestSensor.Status = eOTAStatus.Failed;
            otaRequestSensor.CompletedDate = DateTime.UtcNow;
          }
          otaRequestSensor.Save();
        }
      }
      sensor.FirmwareUpdateInProgress = false;
      sensor.IsSleeping = false;
      sensor.ParentID = parentID;
      sensor.Save();
    }
    else
      ExceptionLog.Log(new Exception("DataProcessing.ParentMessage(Sensor sensor, long parentID) sensor is null and parentID is: " + parentID.ToString()));
  }

  public static void ParentMessage(Gateway gateway, long parentID, byte[] payload)
  {
    if (gateway != null)
    {
      if (payload.Length == 4)
        gateway.APNFirmwareVersion = $"{(ValueType) (uint) payload[0]}.{(ValueType) (uint) payload[1]}.{(ValueType) (uint) payload[2]}.{(ValueType) (uint) payload[3]}";
      if (payload.Length >= 12)
        gateway.APNFirmwareVersion = $"{BitConverter.ToUInt16(payload, 4)}.{BitConverter.ToUInt16(payload, 6)}.{BitConverter.ToUInt16(payload, 8)}.{BitConverter.ToUInt16(payload, 10)}";
      gateway.RadioFirmwareUpdateInProgress = false;
      gateway.Save();
    }
    else
      ExceptionLog.Log(new Exception("DataProcessing.ParentMessage(Gateway gateway, long parentID) gateway is null and parentID is: " + parentID.ToString()));
  }

  public static void CheckExternalDataSubscription(PacketCache packet)
  {
    Dictionary<long, Account> subscriptionsByCsNet = ExternalDataSubscription.AccountsWithExternalSubscriptionsByCSNet;
    Account account1 = (Account) null;
    if (subscriptionsByCsNet.ContainsKey(packet.Gateway.CSNetID))
      account1 = subscriptionsByCsNet[packet.Gateway.CSNetID];
    if (account1 == null)
      return;
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadByCSNetID(packet.Gateway.CSNetID);
    if (dataSubscription == null)
      return;
    try
    {
      DateTime premiumValidUntil1 = account1.PremiumValidUntil;
      DateTime utcNow = DateTime.UtcNow;
      DateTime dateTime1 = utcNow.AddDays(-15.0);
      if (premiumValidUntil1 < dateTime1 || dataSubscription.IsDeleted || account1.HideData)
      {
        Account account2 = Account.Load(dataSubscription.AccountID);
        DateTime premiumValidUntil2 = account2.PremiumValidUntil;
        utcNow = DateTime.UtcNow;
        DateTime dateTime2 = utcNow.AddDays(-15.0);
        if (premiumValidUntil2 < dateTime2 || dataSubscription.IsDeleted || account2.HideData)
        {
          dataSubscription.DoSend = false;
          dataSubscription.DoRetry = false;
          dataSubscription.Save();
          return;
        }
        subscriptionsByCsNet[packet.Gateway.CSNetID] = account2;
      }
      if (packet.DataMessages.Count == 0 && !dataSubscription.SendWithoutDataMessage)
        return;
      ExternalDataSubscriptionAttempt attempt = dataSubscription.Record(packet);
      if (attempt != null && ExternalDataSubscription.FirstExternalSubscriptionAttemptImmediate)
        dataSubscription.Send(attempt);
    }
    catch (Exception ex)
    {
      if (ExternalDataSubscription.LogExternalSubscriptionAsException)
        ex.Log("CheckExternalDataSubscription(PacketCache): Gateway:" + packet.Gateway.GatewayID.ToString());
    }
  }
}
