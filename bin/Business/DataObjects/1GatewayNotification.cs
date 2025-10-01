// Decompiled with JetBrains decompiler
// Type: Monnit.Data.GatewayNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class GatewayNotification
{
  [DBMethod("GatewayNotification_AddGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[GatewayNotification]\r\nWHERE NotificationID = @NotificationID\r\n  AND GatewayID      = @GatewayID;\r\n\r\nINSERT INTO [GatewayNotification] (GatewayID,NotificationID) \r\nVALUES (@GatewayID, @NotificationID);\r\n")]
  internal class AddGateway : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public AddGateway(long notificationID, long gatewayID)
    {
      this.NotificationID = notificationID;
      this.GatewayID = gatewayID;
      this.Execute();
    }
  }

  [DBMethod("GatewayNotification_RemoveGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[GatewayNotification]\r\nWHERE NotificationID = @NotificationID\r\n  AND GatewayID      = @GatewayID;\r\n")]
  internal class RemoveGateway : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public RemoveGateway(long notificationID, long gatewayID)
    {
      this.NotificationID = notificationID;
      this.GatewayID = gatewayID;
      this.Execute();
    }
  }

  [DBMethod("GatewayNotification_LoadByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.*\r\nFROM dbo.[GatewayNotification] n\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = n.GatewayID\r\nWHERE n.NotificationID = @NotificationID;\r\n")]
  internal class LoadByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public List<Monnit.Gateway> Result { get; private set; }

    public LoadByNotificationID(long notificationID)
    {
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable());
    }
  }

  [DBMethod("GatewayNotification_LoadByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  n.*\r\nFROM dbo.[GatewayNotification] gn\r\nINNER JOIN dbo.[Notification] n ON n.NotificationID = gn.NotificationID\r\nWHERE gn.GatewayID = @GatewayID;\r\n")]
  internal class LoadByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public LoadByGatewayID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }
}
