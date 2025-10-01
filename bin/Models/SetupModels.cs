// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DeviceInfoModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class DeviceInfoModel
{
  private Sensor _Sensor = (Sensor) null;
  private Gateway _Gateway = (Gateway) null;

  public DeviceInfoModel()
  {
  }

  public DeviceInfoModel(long? deviceID, string code)
  {
    this.DeviceID = deviceID;
    this.Code = code;
  }

  public long? DeviceID { get; set; }

  public string Code { get; set; }

  public Sensor Sensor
  {
    get
    {
      if (this._Sensor == null)
      {
        long? deviceId = this.DeviceID;
        long num1 = 0;
        int num2;
        if (deviceId.GetValueOrDefault() > num1 & deviceId.HasValue)
        {
          deviceId = this.DeviceID;
          num2 = MonnitUtil.ValidateCheckDigit(deviceId ?? long.MinValue, this.Code) ? 1 : 0;
        }
        else
          num2 = 0;
        if (num2 != 0)
        {
          deviceId = this.DeviceID;
          return Sensor.Load(deviceId ?? long.MinValue);
        }
      }
      return this._Sensor;
    }
  }

  public Gateway Gateway
  {
    get
    {
      if (this._Gateway == null)
      {
        long? deviceId = this.DeviceID;
        long num1 = 0;
        int num2;
        if (deviceId.GetValueOrDefault() > num1 & deviceId.HasValue)
        {
          deviceId = this.DeviceID;
          num2 = MonnitUtil.ValidateCheckDigit(deviceId ?? long.MinValue, this.Code) ? 1 : 0;
        }
        else
          num2 = 0;
        if (num2 != 0)
        {
          deviceId = this.DeviceID;
          this._Gateway = Gateway.Load(deviceId ?? long.MinValue);
        }
      }
      return this._Gateway;
    }
  }
}
