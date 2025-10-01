// Decompiled with JetBrains decompiler
// Type: Monnit.AppDatum
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Reflection;

#nullable disable
namespace Monnit;

public class AppDatum
{
  public string type { get; set; }

  public eDatumType etype { get; set; }

  public DataTypeBase data { get; set; }

  public AppDatum(eDatumType t, DataTypeBase d)
  {
    this.etype = t;
    this.data = d;
    this.type = t.ToString();
  }

  public AppDatum(eDatumType t, string s, DataTypeBase d)
  {
    this.etype = t;
    this.data = d;
    this.type = s;
  }

  public AppDatum(eDatumType t, string s, long l)
  {
    this.etype = t;
    this.type = s;
    this.data = (DataTypeBase) AppDatum.getType(t).GetConstructor(new Type[1]
    {
      typeof (long)
    }).Invoke(new object[1]{ (object) l });
  }

  public AppDatum(eDatumType t, string s, double d)
  {
    this.etype = t;
    this.type = s;
    this.data = (DataTypeBase) AppDatum.getType(t).GetConstructor(new Type[1]
    {
      typeof (double)
    }).Invoke(new object[1]{ (object) d });
  }

  public AppDatum(eDatumType t, string s, int i)
  {
    this.etype = t;
    this.type = s;
    this.data = (DataTypeBase) AppDatum.getType(t).GetConstructor(new Type[1]
    {
      typeof (int)
    }).Invoke(new object[1]{ (object) i });
  }

  public AppDatum(eDatumType t, string s, bool b)
  {
    this.etype = t;
    this.type = s;
    this.data = (DataTypeBase) AppDatum.getType(t).GetConstructor(new Type[1]
    {
      typeof (bool)
    }).Invoke(new object[1]{ (object) b });
  }

  public AppDatum(eDatumType t, string s, string s2)
  {
    this.etype = t;
    this.type = s;
    this.data = (DataTypeBase) AppDatum.getType(t).GetConstructor(new Type[1]
    {
      typeof (string)
    }).Invoke(new object[1]{ (object) s2 });
  }

  public static bool HasStaticMethod(eDatumType edt, string methodname)
  {
    return AppDatum.getType(edt).GetMethod(methodname, BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public static NotificationScaleInfoModel GetScalingInfo(eDatumType edt, long i)
  {
    return AppDatum.getType(edt).CallStaticMethod(nameof (GetScalingInfo), new object[1]
    {
      (object) i
    }) as NotificationScaleInfoModel;
  }

  public static Type getType(eDatumType eType)
  {
    string str = ("Monnit.Application_Classes.DataTypeClasses." + eType.ToString()).Trim();
    Type type1 = Type.GetType(str);
    if (type1 != (Type) null)
      return type1;
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      Type type2 = assembly.GetType(str);
      if (type2 != (Type) null)
        return type2;
    }
    return (Type) null;
  }
}
