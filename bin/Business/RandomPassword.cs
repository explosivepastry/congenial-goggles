// Decompiled with JetBrains decompiler
// Type: Monnit.RandomPassword
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Security.Cryptography;

#nullable disable
namespace Monnit;

public class RandomPassword
{
  private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
  private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;
  private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
  private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
  private static string PASSWORD_CHARS_NUMERIC = "23456789";
  private static string PASSWORD_CHARS_SPECIAL = "*$-+?_#=!%{}/";

  public static string Generate()
  {
    return RandomPassword.Generate(RandomPassword.DEFAULT_MIN_PASSWORD_LENGTH, RandomPassword.DEFAULT_MAX_PASSWORD_LENGTH);
  }

  public static string Generate(int length) => RandomPassword.Generate(length, length);

  public static string Generate(int minLength, int maxLength)
  {
    if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
      return (string) null;
    char[][] chArray1 = new char[4][]
    {
      RandomPassword.PASSWORD_CHARS_LCASE.ToCharArray(),
      RandomPassword.PASSWORD_CHARS_UCASE.ToCharArray(),
      RandomPassword.PASSWORD_CHARS_NUMERIC.ToCharArray(),
      RandomPassword.PASSWORD_CHARS_SPECIAL.ToCharArray()
    };
    int[] numArray1 = new int[chArray1.Length];
    for (int index = 0; index < numArray1.Length; ++index)
      numArray1[index] = chArray1[index].Length;
    int[] numArray2 = new int[chArray1.Length];
    for (int index = 0; index < numArray2.Length; ++index)
      numArray2[index] = index;
    byte[] data = new byte[4];
    new RNGCryptoServiceProvider().GetBytes(data);
    Random random = new Random(((int) data[0] & (int) sbyte.MaxValue) << 24 | (int) data[1] << 16 /*0x10*/ | (int) data[2] << 8 | (int) data[3]);
    char[] chArray2 = minLength >= maxLength ? new char[minLength] : new char[random.Next(minLength, maxLength + 1)];
    int maxValue = numArray2.Length - 1;
    for (int index1 = 0; index1 < chArray2.Length; ++index1)
    {
      int index2 = maxValue != 0 ? random.Next(0, maxValue) : 0;
      int index3 = numArray2[index2];
      int index4 = numArray1[index3] - 1;
      int index5 = index4 != 0 ? random.Next(0, index4 + 1) : 0;
      chArray2[index1] = chArray1[index3][index5];
      if (index4 == 0)
      {
        numArray1[index3] = chArray1[index3].Length;
      }
      else
      {
        if (index4 != index5)
        {
          char ch = chArray1[index3][index4];
          chArray1[index3][index4] = chArray1[index3][index5];
          chArray1[index3][index5] = ch;
        }
        --numArray1[index3];
      }
      if (maxValue == 0)
      {
        maxValue = numArray2.Length - 1;
      }
      else
      {
        if (maxValue != index2)
        {
          int num = numArray2[maxValue];
          numArray2[maxValue] = numArray2[index2];
          numArray2[index2] = num;
        }
        --maxValue;
      }
    }
    return new string(chArray2);
  }
}
