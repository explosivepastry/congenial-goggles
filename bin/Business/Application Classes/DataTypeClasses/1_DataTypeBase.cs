// Decompiled with JetBrains decompiler
// Type: Monnit.eDatumStruct
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public struct eDatumStruct
{
  private string _name;
  private string _val;
  private int _datumindex;
  private string _customname;

  public string name
  {
    get => this._name;
    set => this._name = value;
  }

  public string val
  {
    get => this._val;
    set => this._val = value;
  }

  public int datumindex
  {
    get => this._datumindex;
    set => this._datumindex = value;
  }

  public string customname
  {
    get => this._customname;
    set => this._customname = value;
  }

  public eDatumStruct(eDatumType ed, int di = 0)
  {
    this._name = ed.ToString();
    this._customname = ed.ToString();
    this._val = $"{ed.ToInt().ToString()}&{di.ToString()}";
    this._datumindex = di;
  }

  public eDatumStruct(Sensor sens, int di = 0)
  {
    List<eDatumType> datumTypes = sens.GetDatumTypes();
    this._name = sens.GetDatumDefaultName(di);
    this._customname = sens.GetDatumName(di);
    this._datumindex = di;
    this._val = $"{datumTypes[di].ToInt().ToString()}&{di.ToString()}";
  }

  public eDatumStruct(string name, string customname, int di, string val)
  {
    this._name = name;
    this._customname = customname;
    this._datumindex = di;
    this._val = val;
  }
}
