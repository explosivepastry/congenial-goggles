// Decompiled with JetBrains decompiler
// Type: Monnit.EllipticCurveCryptoProvider
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Numerics;
using System.Security.Cryptography;

#nullable disable
namespace Monnit;

public class EllipticCurveCryptoProvider
{
  private readonly EllipticCurve _curves;
  private readonly EllipticCurveCalculator _calculator;

  public EllipticCurveCryptoProvider(string curveName)
  {
    this._curves = EllipticCurveFactory.Create(curveName);
    this._calculator = new EllipticCurveCalculator(this._curves);
  }

  public void MakeKeyPair(out BigInteger privateKey, out EllipticCurvePoint publicKey)
  {
    EllipticCurve curves = this._curves;
    RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
    byte[] data = new byte[(int) (curves.LengthInBits / 8U) + 1];
    cryptoServiceProvider.GetBytes(data);
    data[32 /*0x20*/] = (byte) 0;
    BigInteger bigInteger = new BigInteger(data);
    do
    {
      privateKey = bigInteger % curves.N;
    }
    while (privateKey == 0L);
    publicKey = this._calculator.ScalarMult(privateKey, curves.G);
  }

  public EllipticCurvePoint MakePublicKey(BigInteger myPrivateKey)
  {
    return this._calculator.ScalarMult(myPrivateKey, this._curves.G);
  }

  public EllipticCurvePoint DeriveSharedSecret(
    BigInteger myPrivateKey,
    EllipticCurvePoint otherPartyPublicKey)
  {
    return this._calculator.ScalarMult(myPrivateKey, otherPartyPublicKey);
  }
}
