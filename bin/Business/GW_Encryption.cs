// Decompiled with JetBrains decompiler
// Type: Monnit.GW_Encryption
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.IO;
using System.Security.Cryptography;

#nullable disable
namespace Monnit;

public static class GW_Encryption
{
  public static byte[] KeyGenerator()
  {
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.KeySize = 128 /*0x80*/;
    rijndaelManaged.GenerateKey();
    return rijndaelManaged.Key;
  }

  public static byte[] IVGenerator()
  {
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.GenerateIV();
    return rijndaelManaged.IV;
  }

  public static byte[] Encrypt128(byte[] PlainBytes, byte[] Key, byte[] InitialVector)
  {
    try
    {
      if (PlainBytes.Length % 16 /*0x10*/ != 0)
        throw new Exception("AES-128 requires an input buffer evenly divisible by 16");
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.None;
      byte[] numArray = (byte[]) null;
      using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(Key, InitialVector))
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
          {
            cryptoStream.Write(PlainBytes, 0, PlainBytes.Length);
            numArray = memoryStream.ToArray();
            cryptoStream.FlushFinalBlock();
          }
        }
      }
      return numArray;
    }
    catch (Exception ex)
    {
      throw ex;
    }
  }

  public static byte[] Decrypt128(byte[] CipherBytes, byte[] Key, byte[] InitialVector)
  {
    try
    {
      if (CipherBytes.Length % 16 /*0x10*/ != 0)
        throw new Exception("AES-128 requires an input buffer evenly divisible by 16");
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.None;
      byte[] buffer = new byte[CipherBytes.Length];
      using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(Key, InitialVector))
      {
        using (MemoryStream memoryStream = new MemoryStream(CipherBytes))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
            cryptoStream.Read(buffer, 0, buffer.Length);
        }
      }
      return buffer;
    }
    catch (Exception ex)
    {
      throw ex;
    }
  }
}
