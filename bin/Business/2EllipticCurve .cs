// Decompiled with JetBrains decompiler
// Type: Monnit.EllipticCurve
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Numerics;

#nullable disable
namespace Monnit;

public class EllipticCurve
{
  public string Name;
  public BigInteger P;
  public BigInteger A;
  public BigInteger B;
  public EllipticCurvePoint G;
  public BigInteger N;
  public short H;
  public uint LengthInBits;

  public EllipticCurve(
    string name,
    BigInteger p,
    BigInteger a,
    BigInteger b,
    EllipticCurvePoint g,
    BigInteger n,
    short h,
    uint length)
  {
    this.Name = name;
    this.P = p;
    this.A = a;
    this.B = b;
    this.G = g;
    this.N = n;
    this.H = h;
    this.LengthInBits = length;
  }

  public bool IsPointOnCurve(EllipticCurvePoint point)
  {
    if (point.IsInfinityPoint())
      return true;
    BigInteger y = point.Y;
    BigInteger x = point.X;
    BigInteger a = this.A;
    BigInteger b = this.B;
    BigInteger p = this.P;
    return (y * y - x * x * x - a * x - b) % p == 0L;
  }

  public void EnsureOnCurve(EllipticCurvePoint point)
  {
    if (!this.IsPointOnCurve(point))
      throw new ArgumentException($"Point1 {point} not on curve");
  }
}
