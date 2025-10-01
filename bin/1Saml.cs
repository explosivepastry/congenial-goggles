// Decompiled with JetBrains decompiler
// Type: Saml.Response
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

#nullable disable
namespace Saml;

public class Response
{
  protected XmlDocument _xmlDoc;
  protected readonly X509Certificate2 _certificate;
  protected XmlNamespaceManager _xmlNameSpaceManager;

  private static byte[] StringToByteArray(string st)
  {
    byte[] byteArray = new byte[st.Length];
    for (int index = 0; index < st.Length; ++index)
      byteArray[index] = (byte) st[index];
    return byteArray;
  }

  public string Xml => this._xmlDoc.OuterXml;

  public Response(string certificateStr, string responseString)
    : this(Response.StringToByteArray(certificateStr), responseString)
  {
  }

  public Response(byte[] certificateBytes, string responseString)
    : this(certificateBytes)
  {
    this.LoadXmlFromBase64(responseString);
  }

  public Response(string certificateStr)
    : this(Response.StringToByteArray(certificateStr))
  {
  }

  public Response(byte[] certificateBytes)
  {
    RSAPKCS1SHA256SignatureDescription.Init();
    this._certificate = new X509Certificate2(certificateBytes);
  }

  public void LoadXml(string xml)
  {
    this._xmlDoc = new XmlDocument();
    this._xmlDoc.PreserveWhitespace = true;
    this._xmlDoc.XmlResolver = (XmlResolver) null;
    this._xmlDoc.LoadXml(xml);
    this._xmlNameSpaceManager = this.GetNamespaceManager();
  }

  public void LoadXmlFromBase64(string response)
  {
    this.LoadXml(new UTF8Encoding().GetString(Convert.FromBase64String(response)));
  }

  public bool IsValid()
  {
    XmlNodeList xmlNodeList = this._xmlDoc.SelectNodes("//ds:Signature", this._xmlNameSpaceManager);
    SignedXml signedXml = new SignedXml(this._xmlDoc);
    if (xmlNodeList.Count == 0)
      return false;
    signedXml.LoadXml((XmlElement) xmlNodeList[0]);
    return this.ValidateSignatureReference(signedXml) && signedXml.CheckSignature(this._certificate, true) && !this.IsExpired();
  }

  private bool ValidateSignatureReference(SignedXml signedXml)
  {
    if (signedXml.SignedInfo.References.Count != 1)
      return false;
    string idValue = ((Reference) signedXml.SignedInfo.References[0]).Uri.Substring(1);
    XmlElement idElement = signedXml.GetIdElement(this._xmlDoc, idValue);
    return idElement == this._xmlDoc.DocumentElement || this._xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion", this._xmlNameSpaceManager) as XmlElement == idElement;
  }

  private bool IsExpired()
  {
    DateTime result = DateTime.MaxValue;
    XmlNode xmlNode = this._xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion[1]/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData", this._xmlNameSpaceManager);
    if (xmlNode != null && xmlNode.Attributes["NotOnOrAfter"] != null)
      DateTime.TryParse(xmlNode.Attributes["NotOnOrAfter"].Value, out result);
    return DateTime.UtcNow > result.ToUniversalTime();
  }

  public string GetNameID()
  {
    return this._xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion[1]/saml:Subject/saml:NameID", this._xmlNameSpaceManager).InnerText;
  }

  public virtual string GetUpn()
  {
    return this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn");
  }

  public virtual string GetEmail()
  {
    return this.GetCustomAttribute("User.email") ?? this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") ?? this.GetCustomAttribute("mail");
  }

  public virtual string GetFirstName()
  {
    return this.GetCustomAttribute("first_name") ?? this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname") ?? this.GetCustomAttribute("User.FirstName") ?? this.GetCustomAttribute("givenName");
  }

  public virtual string GetLastName()
  {
    return this.GetCustomAttribute("last_name") ?? this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname") ?? this.GetCustomAttribute("User.LastName") ?? this.GetCustomAttribute("sn");
  }

  public virtual string GetDepartment()
  {
    return this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/department");
  }

  public virtual string GetPhone()
  {
    return this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone") ?? this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/telephonenumber");
  }

  public virtual string GetCompany()
  {
    return this.GetCustomAttribute("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/companyname") ?? this.GetCustomAttribute("User.CompanyName");
  }

  public string GetCustomAttribute(string attr)
  {
    XmlNode xmlNode = this._xmlDoc.SelectSingleNode($"/samlp:Response/saml:Assertion[1]/saml:AttributeStatement/saml:Attribute[@Name='{attr}']/saml:AttributeValue", this._xmlNameSpaceManager);
    return xmlNode == null ? (string) null : xmlNode.InnerText;
  }

  private XmlNamespaceManager GetNamespaceManager()
  {
    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(this._xmlDoc.NameTable);
    namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
    namespaceManager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
    namespaceManager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
    return namespaceManager;
  }
}
