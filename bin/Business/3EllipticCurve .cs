// Decompiled with JetBrains decompiler
// Type: Monnit.EllipticCurveCalculator
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Numerics;

#nullable disable
namespace Monnit;

public class EllipticCurveCalculator
{
  private EllipticCurve _curve;

  public EllipticCurveCalculator(EllipticCurve curve) => this._curve = curve;

  public BigInteger InverseMod(BigInteger k, BigInteger p)
  {
    if (k == 0L)
      throw new DivideByZeroException(nameof (k));
    if (k < 0L)
      return p - this.InverseMod(-k, p);
    BigInteger bigInteger1 = (BigInteger) 0;
    BigInteger bigInteger2 = (BigInteger) 1;
    BigInteger bigInteger3 = p;
    BigInteger bigInteger4 = k;
    while (bigInteger3 != 0L)
    {
      BigInteger bigInteger5 = bigInteger4 / bigInteger3;
      BigInteger bigInteger6 = bigInteger3;
      bigInteger3 = bigInteger4 - bigInteger5 * bigInteger6;
      bigInteger4 = bigInteger6;
      BigInteger bigInteger7 = bigInteger1;
      bigInteger1 = bigInteger2 - bigInteger5 * bigInteger7;
      bigInteger2 = bigInteger7;
    }
    BigInteger bigInteger8 = bigInteger4;
    BigInteger k1 = bigInteger2;
    if (bigInteger8 != 1L)
      throw new NotSupportedException($"gcd != 1, {bigInteger8}");
    BigInteger bigInteger9 = this.Modular(k * k1, p);
    if (bigInteger9 != 1L)
      throw new NotSupportedException($"(k * x) % p != 1, {bigInteger9}");
    return this.Modular(k1, p);
  }

  public EllipticCurvePoint Add(EllipticCurvePoint point1, EllipticCurvePoint point2)
  {
    this._curve.EnsureOnCurve(point1);
    this._curve.EnsureOnCurve(point2);
    if (point1.IsInfinityPoint())
      return point2;
    if (point2.IsInfinityPoint())
      return point1;
    BigInteger x1 = point1.X;
    BigInteger y1 = point1.Y;
    BigInteger x2 = point2.X;
    BigInteger y2 = point2.Y;
    BigInteger p = this._curve.P;
    BigInteger a = this._curve.A;
    BigInteger bigInteger1;
    if (x1 == x2)
    {
      if (y1 != y2)
        return new EllipticCurvePoint((BigInteger) 0, (BigInteger) 0);
      bigInteger1 = ((BigInteger) 3 * x1 * x1 + a) * this.InverseMod((BigInteger) 2 * y1, p);
    }
    else
      bigInteger1 = (y1 - y2) * this.InverseMod(x1 - x2, p);
    BigInteger k = bigInteger1 * bigInteger1 - x1 - x2;
    BigInteger bigInteger2 = y1 + bigInteger1 * (k - x1);
    return new EllipticCurvePoint(this.Modular(k, p), this.Modular(-bigInteger2, p));
  }

  public EllipticCurvePoint PointNeg(EllipticCurvePoint point)
  {
    this._curve.EnsureOnCurve(point);
    if (point.IsInfinityPoint())
      return point;
    EllipticCurvePoint point1 = new EllipticCurvePoint(point.X, this.Modular(-point.Y, this._curve.P));
    this._curve.EnsureOnCurve(point1);
    return point1;
  }

  public EllipticCurvePoint ScalarMult(BigInteger k, EllipticCurvePoint point)
  {
    this._curve.EnsureOnCurve(point);
    BigInteger n = this._curve.N;
    if (k % n == 0L || point.IsInfinityPoint())
      return new EllipticCurvePoint((BigInteger) 0, (BigInteger) 0);
    if (k < 0L)
      return this.ScalarMult(-k, this.PointNeg(point));
    EllipticCurvePoint ellipticCurvePoint1 = new EllipticCurvePoint((BigInteger) 0, (BigInteger) 0);
    EllipticCurvePoint ellipticCurvePoint2 = point;
    while (k != 0L)
    {
      if ((k & (BigInteger) 1) == 1L)
        ellipticCurvePoint1 = this.Add(ellipticCurvePoint1, ellipticCurvePoint2);
      ellipticCurvePoint2 = this.Add(ellipticCurvePoint2, ellipticCurvePoint2);
      k >>= 1;
    }
    this._curve.EnsureOnCurve(ellipticCurvePoint1);
    return ellipticCurvePoint1;
  }

  public BigInteger Modular(BigInteger k, BigInteger p)
  {
    BigInteger bigInteger = k % p;
    if (bigInteger < 0L)
      bigInteger += p;
    return bigInteger;
  }
}
