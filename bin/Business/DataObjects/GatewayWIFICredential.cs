// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayWIFICredential
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("GatewayWIFICredential")]
public class GatewayWIFICredential : BaseDBObject, INotifyPropertyChanged
{
  private long _GatewayWIFICredentialID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private int _CredentialIndex = int.MinValue;
  private string _SSID = string.Empty;
  private string _PassPhrase = string.Empty;
  private eWIFI_SecurityMode _WIFISecurityMode = eWIFI_SecurityMode.OPEN;
  private bool _SuppressPropertyChangedEvent = true;
  public bool IsDirty = false;

  [DBProp("GatewayWIFICredentialID", IsPrimaryKey = true)]
  public long GatewayWIFICredentialID
  {
    get => this._GatewayWIFICredentialID;
    set => this._GatewayWIFICredentialID = value;
  }

  [DBProp("GatewayID")]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("CredentialIndex")]
  public int CredentialIndex
  {
    get => this._CredentialIndex;
    set
    {
      int credentialIndex = this._CredentialIndex;
      this._CredentialIndex = value;
      if (credentialIndex == this.CredentialIndex)
        return;
      this.OnPropertyChanged(nameof (CredentialIndex));
    }
  }

  [DBProp("SSID", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SSID
  {
    get => this._SSID;
    set
    {
      string ssid = this._SSID;
      this._SSID = value;
      if (!(ssid != this.SSID))
        return;
      this.OnPropertyChanged(nameof (SSID));
    }
  }

  [DBProp("PassPhrase", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string PassPhrase
  {
    get => this._PassPhrase;
    set
    {
      string passPhrase = this._PassPhrase;
      this._PassPhrase = value;
      if (!(passPhrase != this.PassPhrase))
        return;
      this.OnPropertyChanged(nameof (PassPhrase));
    }
  }

  [DBProp("WIFISecurityMode")]
  public eWIFI_SecurityMode WIFISecurityMode
  {
    get => this._WIFISecurityMode;
    set
    {
      eWIFI_SecurityMode wifiSecurityMode = this._WIFISecurityMode;
      this._WIFISecurityMode = value;
      if (wifiSecurityMode == this._WIFISecurityMode)
        return;
      this.OnPropertyChanged(nameof (WIFISecurityMode));
    }
  }

  public bool SuppressPropertyChangedEvent
  {
    get => this._SuppressPropertyChangedEvent;
    set => this._SuppressPropertyChangedEvent = value;
  }

  public event PropertyChangedEventHandler PropertyChanged;

  public void OnPropertyChanged(string propertyName)
  {
    if (this.SuppressPropertyChangedEvent)
      return;
    try
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    if (propertyName == "CredentialIndex")
      this.IsDirty = true;
    if (propertyName == "SSID")
      this.IsDirty = true;
    if (propertyName == "PassPhrase")
      this.IsDirty = true;
    if (!(propertyName == "WIFISecurityMode"))
      return;
    this.IsDirty = true;
  }

  public override void Save()
  {
    if (string.IsNullOrEmpty(this.SSID))
      return;
    base.Save();
  }

  public static GatewayWIFICredential Load(long id)
  {
    GatewayWIFICredential gatewayWifiCredential = BaseDBObject.Load<GatewayWIFICredential>(id);
    if (gatewayWifiCredential != null)
      gatewayWifiCredential.SuppressPropertyChangedEvent = false;
    return gatewayWifiCredential;
  }

  public static GatewayWIFICredential Load(long gatewayID, int index)
  {
    GatewayWIFICredential gatewayWifiCredential = BaseDBObject.LoadByForeignKeys<GatewayWIFICredential>(new string[2]
    {
      "GatewayID",
      "CredentialIndex"
    }, new object[2]{ (object) gatewayID, (object) index }).FirstOrDefault<GatewayWIFICredential>();
    if (gatewayWifiCredential != null)
      gatewayWifiCredential.SuppressPropertyChangedEvent = false;
    return gatewayWifiCredential;
  }
}
