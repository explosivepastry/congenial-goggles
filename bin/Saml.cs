// Decompiled with JetBrains decompiler
// Type: Saml.RSAPKCS1SHA256SignatureDescription
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Security.Cryptography;

#nullable disable
namespace Saml;

public sealed class RSAPKCS1SHA256SignatureDescription : SignatureDescription
{
  private static bool _initialized;

  public RSAPKCS1SHA256SignatureDescription()
  {
    this.KeyAlgorithm = typeof (RSACryptoServiceProvider).FullName;
    this.DigestAlgorithm = typeof (SHA256Managed).FullName;
    this.FormatterAlgorithm = typeof (RSAPKCS1SignatureFormatter).FullName;
    this.DeformatterAlgorithm = typeof (RSAPKCS1SignatureDeformatter).FullName;
  }

  public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
  {
    RSAPKCS1SignatureDeformatter deformatter = key != null ? new RSAPKCS1SignatureDeformatter(key) : throw new ArgumentNullException(nameof (key));
    deformatter.SetHashAlgorithm("SHA256");
    return (AsymmetricSignatureDeformatter) deformatter;
  }

  public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
  {
    RSAPKCS1SignatureFormatter formatter = key != null ? new RSAPKCS1SignatureFormatter(key) : throw new ArgumentNullException(nameof (key));
    formatter.SetHashAlgorithm("SHA256");
    return (AsymmetricSignatureFormatter) formatter;
  }

  public static void Init()
  {
    if (!RSAPKCS1SHA256SignatureDescription._initialized)
      CryptoConfig.AddAlgorithm(typeof (RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
    RSAPKCS1SHA256SignatureDescription._initialized = true;
  }
}
