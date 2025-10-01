// Decompiled with JetBrains decompiler
// Type: Monnit.EllipticCurvePoint
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Numerics;

#nullable disable
namespace Monnit;

public class EllipticCurvePoint
{
  public BigInteger X;
  public BigInteger Y;

  public EllipticCurvePoint(BigInteger iX, BigInteger iY)
  {
    this.X = iX;
    this.Y = iY;
  }

  public EllipticCurvePoint(byte[] buffer, int startIndex)
  {
    byte[] destinationArray = new byte[33];
    Array.Copy((Array) buffer, startIndex, (Array) destinationArray, 0, 32 /*0x20*/);
    destinationArray[32 /*0x20*/] = (byte) 0;
    this.X = new BigInteger(destinationArray);
    Array.Copy((Array) buffer, 32 /*0x20*/ + startIndex, (Array) destinationArray, 0, 32 /*0x20*/);
    this.Y = new BigInteger(destinationArray);
  }

  public bool IsInfinityPoint() => this.X == 0L || this.Y == 0L;

  public byte[] toArray()
  {
    byte[] destinationArray = new byte[64 /*0x40*/];
    Array.Clear((Array) destinationArray, 0, destinationArray.Length);
    Array.Copy((Array) this.X.ToByteArray(), 0, (Array) destinationArray, 0, 32 /*0x20*/);
    Array.Copy((Array) this.Y.ToByteArray(), 0, (Array) destinationArray, 32 /*0x20*/, 32 /*0x20*/);
    return destinationArray;
  }

  public byte[] extractAESKEY(int method)
  {
    byte[] array = this.toArray();
    byte[] aeskey = new byte[16 /*0x10*/];
    if (method == 0)
      ;
    aeskey[0] = array[12];
    aeskey[1] = array[62];
    aeskey[2] = array[31 /*0x1F*/];
    aeskey[3] = array[29];
    aeskey[4] = array[19];
    aeskey[5] = array[1];
    aeskey[6] = array[56];
    aeskey[7] = array[5];
    aeskey[8] = array[39];
    aeskey[9] = array[58];
    aeskey[10] = array[16 /*0x10*/];
    aeskey[11] = array[21];
    aeskey[12] = array[4];
    aeskey[13] = array[36];
    aeskey[14] = array[63 /*0x3F*/];
    aeskey[15] = array[32 /*0x20*/];
    return aeskey;
  }

  public byte[] extractAESIV(int method)
  {
    byte[] array = this.toArray();
    byte[] aesiv = new byte[16 /*0x10*/];
    if (method == 0)
      ;
    aesiv[0] = array[48 /*0x30*/];
    aesiv[1] = array[8];
    aesiv[2] = array[3];
    aesiv[3] = array[37];
    aesiv[4] = array[18];
    aesiv[5] = array[24];
    aesiv[6] = array[61];
    aesiv[7] = array[17];
    aesiv[8] = array[54];
    aesiv[9] = array[13];
    aesiv[10] = array[44];
    aesiv[11] = array[22];
    aesiv[12] = array[23];
    aesiv[13] = array[46];
    aesiv[14] = array[7];
    aesiv[15] = array[17];
    return aesiv;
  }
}
