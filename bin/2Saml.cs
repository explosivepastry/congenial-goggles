// Decompiled with JetBrains decompiler
// Type: Saml.AuthRequest
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Xml;

#nullable disable
namespace Saml;

public class AuthRequest
{
  public string _id;
  private string _issue_instant;
  private string _issuer;
  private string _assertionConsumerServiceUrl;

  public AuthRequest(string issuer, string assertionConsumerServiceUrl)
  {
    RSAPKCS1SHA256SignatureDescription.Init();
    this._id = "_" + Guid.NewGuid().ToString();
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.ToUniversalTime();
    this._issue_instant = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ", (IFormatProvider) CultureInfo.InvariantCulture);
    this._issuer = issuer;
    this._assertionConsumerServiceUrl = assertionConsumerServiceUrl;
  }

  public string GetRequest(AuthRequest.AuthRequestFormat format)
  {
    using (StringWriter output = new StringWriter())
    {
      using (XmlWriter xmlWriter = XmlWriter.Create((TextWriter) output, new XmlWriterSettings()
      {
        OmitXmlDeclaration = true
      }))
      {
        xmlWriter.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
        xmlWriter.WriteAttributeString("ID", this._id);
        xmlWriter.WriteAttributeString("Version", "2.0");
        xmlWriter.WriteAttributeString("IssueInstant", this._issue_instant);
        xmlWriter.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
        xmlWriter.WriteAttributeString("AssertionConsumerServiceURL", this._assertionConsumerServiceUrl);
        xmlWriter.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
        xmlWriter.WriteString(this._issuer);
        xmlWriter.WriteEndElement();
        xmlWriter.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
        xmlWriter.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified");
        xmlWriter.WriteAttributeString("AllowCreate", "true");
        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndElement();
      }
      if (format != AuthRequest.AuthRequestFormat.Base64)
        return (string) null;
      MemoryStream memoryStream = new MemoryStream();
      StreamWriter streamWriter = new StreamWriter((Stream) new DeflateStream((Stream) memoryStream, CompressionMode.Compress, true), (Encoding) new UTF8Encoding(false));
      streamWriter.Write(output.ToString());
      streamWriter.Close();
      return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length, Base64FormattingOptions.None);
    }
  }

  public string GetRedirectUrl(string samlEndpoint, string relayState = null)
  {
    string str = samlEndpoint.Contains("?") ? "&" : "?";
    string redirectUrl = $"{samlEndpoint}{str}SAMLRequest={HttpUtility.UrlEncode(this.GetRequest(AuthRequest.AuthRequestFormat.Base64))}";
    if (!string.IsNullOrEmpty(relayState))
      redirectUrl = $"{redirectUrl}&RelayState={HttpUtility.UrlEncode(relayState)}";
    return redirectUrl;
  }

  public enum AuthRequestFormat
  {
    Base64 = 1,
  }
}
