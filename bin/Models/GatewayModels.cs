// Decompiled with JetBrains decompiler
// Type: Data.GatewayTestingModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Data;

internal class GatewayTestingModel : BaseDBObject
{
  [DBMethod("DeviceTestingModel_LoadByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--local parameters\r\nDECLARE @SQL VARCHAR(8000),\r\n        @CSNetID BIGINT,\r\n        @_GatewayID BIGINT;\r\n\r\n--parameter sniffing resolution\r\nSET @_GatewayID = @GatewayID;\r\n\r\nSET @CSNetID = (SELECT CSNetID FROM dbo.[Gateway] g  WITH (NOLOCK) WHERE GatewayID = @_GatewayID);\r\n\r\n--GatewayMessages\r\nSET @SQL = \r\n'SELECT TOP 25\r\n  *\r\nFROM dbo.[GatewayMessage] g WITH (NOLOCK)\r\nWHERE g.GatewayID = '+CONVERT(VARCHAR(30), @_GatewayID)+'\r\n  AND ReceivedDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' AND '''+CONVERT(VARCHAR(30), @ToDate,120)+'''\r\nORDER BY ReceivedDate DESC;'\r\n\r\nEXEC (@SQL);\r\n\r\n--DataMessages from Gateway\r\nSET @SQL = \r\n'SELECT TOP 25\r\n  d.*\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nINNER JOIN dbo.[DataMessage] d  WITH (NOLOCK) on s.SensorID = d.SensorID\r\nWHERE s.CSNetID = '+CONVERT(VARCHAR(30), @CSNetID)+'\r\n  AND d.MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' AND '''+CONVERT(VARCHAR(30), @ToDate,120)+'''\r\n  AND d.GatewayID = '+CONVERT(VARCHAR(30), @_GatewayID)+'\r\nORDER BY MessageDate DESC;'\r\n\r\nEXEC (@SQL);\r\n\r\n--LocationMessages from Gateway\r\nSET @SQL = \r\n'SELECT TOP 25\r\n  *\r\nFROM dbo.[LocationMessage] g WITH (NOLOCK)\r\nWHERE g.DeviceID = '+CONVERT(VARCHAR(30), @_GatewayID)+'\r\nAND g.LocationDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' AND '''+CONVERT(VARCHAR(30), @ToDate,120)+'''\r\nORDER BY LocationDate DESC;'\r\n\r\nEXEC (@SQL);\r\n")]
  internal class LoadByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public iMonnit.Models.GatewayTestingModel Result { get; private set; }

    public LoadByGatewayID(long gatewayID, DateTime fromDate, DateTime toDate)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      iMonnit.Models.GatewayTestingModel gatewayTestingModel = new iMonnit.Models.GatewayTestingModel();
      List<GatewayTestingMessage> source = new List<GatewayTestingMessage>();
      DataSet dataSet = this.ToDataSet();
      List<GatewayMessage> gatewayMessageList = BaseDBObject.Load<GatewayMessage>(dataSet.Tables[0]);
      List<DataMessage> dataMessageList = BaseDBObject.Load<DataMessage>(dataSet.Tables[1]);
      List<LocationMessage> locationMessageList = BaseDBObject.Load<LocationMessage>(dataSet.Tables[2]);
      foreach (GatewayMessage gatewayMessage in gatewayMessageList)
      {
        GatewayTestingMessage gatewayTestingMessage1 = new GatewayTestingMessage();
        gatewayTestingMessage1.Guid = gatewayMessage.GatewayMessageGUID;
        gatewayTestingMessage1.IconString = "gateway";
        gatewayTestingMessage1.MessageDate = gatewayMessage.ReceivedDate;
        gatewayTestingMessage1.DeviceID = gatewayMessage.GatewayID;
        GatewayTestingMessage gatewayTestingMessage2 = gatewayTestingMessage1;
        string[] strArray = new string[7];
        int num = gatewayMessage.SignalStrength;
        strArray[0] = num.ToString();
        strArray[1] = "|";
        num = gatewayMessage.Power;
        strArray[2] = num.ToString();
        strArray[3] = "|";
        num = gatewayMessage.Battery;
        strArray[4] = num.ToString();
        strArray[5] = "|";
        num = gatewayMessage.MessageCount;
        strArray[6] = num.ToString();
        string str = string.Concat(strArray);
        gatewayTestingMessage2.Content = str;
        source.Add(gatewayTestingMessage1);
      }
      foreach (DataMessage dataMessage in dataMessageList)
        source.Add(new GatewayTestingMessage()
        {
          Guid = dataMessage.DataMessageGUID,
          IconString = "sensor",
          MessageDate = dataMessage.MessageDate,
          DeviceID = dataMessage.SensorID,
          Content = $"{dataMessage.SignalStrength.ToString()}|{dataMessage.Data}"
        });
      foreach (LocationMessage locationMessage in locationMessageList)
      {
        GatewayTestingMessage gatewayTestingMessage3 = new GatewayTestingMessage();
        gatewayTestingMessage3.Guid = locationMessage.LocationMessageGUID;
        gatewayTestingMessage3.IconString = "location";
        gatewayTestingMessage3.MessageDate = locationMessage.LocationDate;
        gatewayTestingMessage3.DeviceID = locationMessage.DeviceID;
        GatewayTestingMessage gatewayTestingMessage4 = gatewayTestingMessage3;
        double num = locationMessage.Latitude;
        string str1 = num.ToString();
        num = locationMessage.Longitude;
        string str2 = num.ToString();
        string str3 = $"{str1}|{str2}";
        gatewayTestingMessage4.Content = str3;
        source.Add(gatewayTestingMessage3);
      }
      gatewayTestingModel.Messages = source.OrderByDescending<GatewayTestingMessage, DateTime>((System.Func<GatewayTestingMessage, DateTime>) (m => m.MessageDate)).ToList<GatewayTestingMessage>();
      this.Result = gatewayTestingModel;
    }
  }
}
