// Decompiled with JetBrains decompiler
// Type: Monnit.EllipticCurveFactory
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Globalization;
using System.Numerics;

#nullable disable
namespace Monnit;

public static class EllipticCurveFactory
{
  private static EllipticCurve GetEllipticCurveSecp256K1()
  {
    return new EllipticCurve("Secp256K1", BigInteger.Parse("00fffffffffffffffffffffffffffffffffffffffffffffffffffffffefffffc2f", NumberStyles.HexNumber), (BigInteger) 0, (BigInteger) 7, new EllipticCurvePoint(BigInteger.Parse("79be667ef9dcbbac55a06295ce870b07029bfcdb2dce28d959f2815b16f81798", NumberStyles.HexNumber), BigInteger.Parse("483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8", NumberStyles.HexNumber)), BigInteger.Parse("00fffffffffffffffffffffffffffffffebaaedce6af48a03bbfd25e8cd0364141", NumberStyles.HexNumber), (short) 1, 256U /*0x0100*/);
  }

  private static EllipticCurve GetEllipticCurveSecp256R1()
  {
    return new EllipticCurve("Secp256R1", BigInteger.Parse("00FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF", NumberStyles.HexNumber), BigInteger.Parse("00FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC", NumberStyles.HexNumber), BigInteger.Parse("005AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B", NumberStyles.HexNumber), new EllipticCurvePoint(BigInteger.Parse("006B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C296", NumberStyles.HexNumber), BigInteger.Parse("004FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5", NumberStyles.HexNumber)), BigInteger.Parse("00FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551", NumberStyles.HexNumber), (short) 1, 256U /*0x0100*/);
  }

  public static EllipticCurve Create(string curveName)
  {
    switch (curveName)
    {
      case "Secp256K1":
        return EllipticCurveFactory.GetEllipticCurveSecp256K1();
      case "Secp256R1":
        return EllipticCurveFactory.GetEllipticCurveSecp256R1();
      default:
        throw new NotSupportedException($"{curveName} not supported");
    }
  }
}
