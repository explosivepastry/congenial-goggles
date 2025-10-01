// Decompiled with JetBrains decompiler
// Type: Monnit.SensorProfile
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

#nullable disable
namespace Monnit;

[DBClass("SensorProfile")]
public class SensorProfile : BaseDBObject
{
  private long _SensorProfileID = long.MinValue;
  private string _Name = string.Empty;
  private eInputType _InputType;
  private List<SensorProfileDatem> _Datems = (List<SensorProfileDatem>) null;

  [DBProp("SensorProfileID", IsPrimaryKey = true)]
  public long SensorProfileID
  {
    get => this._SensorProfileID;
    set => this._SensorProfileID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("InputType", AllowNull = false)]
  public eInputType InputType
  {
    get => this._InputType;
    set => this._InputType = value;
  }

  public List<SensorProfileDatem> Datems
  {
    get
    {
      if (this._Datems == null)
        this._Datems = new List<SensorProfileDatem>();
      return this._Datems;
    }
    set => this._Datems = value;
  }

  public static SensorProfile Create(long id, object inputData)
  {
    SensorProfile sensorProfile = SensorProfile.Load(id);
    switch (sensorProfile.InputType)
    {
      case eInputType._binary:
        sensorProfile = inputData is byte[] ? SensorProfile.BinaryCreate(sensorProfile, (byte[]) inputData) : throw new Exception("Not valid Byte[] format.");
        break;
      case eInputType._json:
        sensorProfile = inputData is JsonResult ? SensorProfile.JsonResultCreate(sensorProfile, (JsonResult) inputData) : throw new Exception("Not valid Json format.");
        break;
      case eInputType._xml:
        sensorProfile = inputData is XmlDocument ? SensorProfile.XmlDocumentCreate(sensorProfile, (XmlDocument) inputData) : throw new Exception("Not valid XmlDocument format.");
        break;
      case eInputType._stringval:
        sensorProfile = inputData is string ? SensorProfile.StringCreate(sensorProfile, (string) inputData) : throw new Exception("Not valid string format.");
        break;
    }
    return sensorProfile;
  }

  public static SensorProfile BinaryCreate(SensorProfile obj, byte[] binaryData)
  {
    foreach (SensorProfileIndex sensorProfileIndex in SensorProfileIndex.LoadBySensorProfileID(obj.SensorProfileID))
    {
      SensorProfileDatem datem = new SensorProfileDatem();
      datem.SensorProfileIndexID = sensorProfileIndex.SensorProfileIndexID;
      switch (sensorProfileIndex.DataType)
      {
        case eDataType._boolean:
          datem.BooleanVal = binaryData[sensorProfileIndex.StartIndex].ToBool();
          break;
        case eDataType._string:
          byte[] numArray = new byte[sensorProfileIndex.Length];
          Array.Copy((Array) binaryData, sensorProfileIndex.StartIndex, (Array) numArray, 0, sensorProfileIndex.Length);
          datem.StringVal = numArray.FormatBytesToStringArray();
          break;
        case eDataType._double:
          datem.DoubleVal = obj.EncodedType(sensorProfileIndex.EncodedType, binaryData, sensorProfileIndex.StartIndex).ToDouble();
          break;
        case eDataType._int:
          datem.IntVal = obj.EncodedType(sensorProfileIndex.EncodedType, binaryData, sensorProfileIndex.StartIndex).ToInt();
          break;
      }
      if (!string.IsNullOrEmpty(sensorProfileIndex.Math))
        obj.Math(datem, sensorProfileIndex.SensorProfileIndexID, sensorProfileIndex.Math);
      obj.Datems.Add(datem);
    }
    return obj;
  }

  public static SensorProfile JsonResultCreate(SensorProfile obj, JsonResult jsonData) => obj;

  public static SensorProfile XmlDocumentCreate(SensorProfile obj, XmlDocument xmlData) => obj;

  public static SensorProfile StringCreate(SensorProfile obj, string stringData) => obj;

  public string EncodedType(eEncodedType encodedType, byte[] binaryData, int startIndex)
  {
    string empty = string.Empty;
    switch (encodedType)
    {
      case eEncodedType._byte:
        empty = binaryData[startIndex].ToString();
        break;
      case eEncodedType._uint16:
        empty = BitConverter.ToUInt16(binaryData, startIndex).ToString();
        break;
      case eEncodedType._int16:
        empty = BitConverter.ToInt16(binaryData, startIndex).ToString();
        break;
      case eEncodedType._uint32:
        empty = BitConverter.ToUInt32(binaryData, startIndex).ToString();
        break;
      case eEncodedType._int32:
        empty = BitConverter.ToInt32(binaryData, startIndex).ToString();
        break;
      case eEncodedType._uint64:
        empty = BitConverter.ToUInt64(binaryData, startIndex).ToString();
        break;
      case eEncodedType._int64:
        empty = BitConverter.ToInt64(binaryData, startIndex).ToString();
        break;
    }
    return empty;
  }

  public SensorProfileDatem Math(SensorProfileDatem datem, long id, string math)
  {
    SensorProfileIndex sensorProfileIndex = SensorProfileIndex.Load(id);
    if (datem.DoubleVal > 0.0)
    {
      double num = Convert.ToDouble(new DataTable().Compute(string.Format(sensorProfileIndex.Math, (object) datem.DoubleVal), (string) null));
      datem.DoubleVal = num;
    }
    else if (datem.IntVal > 0)
    {
      double o = Convert.ToDouble(new DataTable().Compute(string.Format(sensorProfileIndex.Math, (object) datem.IntVal), (string) null));
      datem.IntVal = o.ToInt();
    }
    return datem;
  }

  public static SensorProfile Load(long id) => BaseDBObject.Load<SensorProfile>(id);

  public string Serialize()
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    List<string> stringList = new List<string>();
    List<SensorProfileIndex> sensorProfileIndexList = SensorProfileIndex.LoadBySensorProfileID(this.SensorProfileID);
    for (int index = 0; index < this.Datems.Count; ++index)
    {
      switch (sensorProfileIndexList[index].DataType)
      {
        case eDataType._boolean:
          empty2 = this.Datems[index].BooleanVal.ToString();
          break;
        case eDataType._string:
          empty2 = this.Datems[index].StringVal.ToString();
          break;
        case eDataType._double:
          empty2 = this.Datems[index].DoubleVal.ToString();
          break;
        case eDataType._int:
          empty2 = this.Datems[index].IntVal.ToString();
          break;
      }
      stringList.Add(empty2);
    }
    return string.Join("|", stringList.ToArray());
  }

  public static SensorProfile Deserialize(string serialize, long id)
  {
    SensorProfile sensorProfile = new SensorProfile();
    string[] strArray = serialize.Split('|');
    List<SensorProfileIndex> list = SensorProfileIndex.LoadBySensorProfileID(id).OrderBy<SensorProfileIndex, int>((System.Func<SensorProfileIndex, int>) (s => s.StartIndex)).ToList<SensorProfileIndex>();
    for (int index = 0; index < list.Count; ++index)
    {
      SensorProfileDatem sensorProfileDatem = new SensorProfileDatem();
      switch (list[index].DataType)
      {
        case eDataType._boolean:
          sensorProfileDatem.BooleanVal = strArray[index].ToBool();
          break;
        case eDataType._string:
          sensorProfileDatem.StringVal = strArray[index].ToString();
          break;
        case eDataType._double:
          sensorProfileDatem.DoubleVal = strArray[index].ToDouble();
          break;
        case eDataType._int:
          sensorProfileDatem.IntVal = strArray[index].ToInt();
          break;
      }
      sensorProfile.Datems.Add(sensorProfileDatem);
    }
    return sensorProfile;
  }
}
