// Decompiled with JetBrains decompiler
// Type: Monnit.rfc2898
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace Monnit;

public class rfc2898
{
  private const string usageText = "Usage: RFC2898 <password>\nYou must specify the password for encryption.\n";

  public static void Main(string[] passwordargs)
  {
    if (passwordargs.Length == 0)
    {
      Console.WriteLine("Usage: RFC2898 <password>\nYou must specify the password for encryption.\n");
    }
    else
    {
      string passwordarg = passwordargs[0];
      byte[] numArray = new byte[8];
      using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
        cryptoServiceProvider.GetBytes(numArray);
      string s = "Some test data";
      int iterations = 1000;
      try
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes1 = new Rfc2898DeriveBytes(passwordarg, numArray, iterations);
        Rfc2898DeriveBytes rfc2898DeriveBytes2 = new Rfc2898DeriveBytes(passwordarg, numArray);
        Aes aes1 = Aes.Create();
        aes1.Key = rfc2898DeriveBytes1.GetBytes(16 /*0x10*/);
        MemoryStream memoryStream1 = new MemoryStream();
        CryptoStream cryptoStream1 = new CryptoStream((Stream) memoryStream1, aes1.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] bytes = new UTF8Encoding(false).GetBytes(s);
        cryptoStream1.Write(bytes, 0, bytes.Length);
        cryptoStream1.FlushFinalBlock();
        cryptoStream1.Close();
        byte[] array = memoryStream1.ToArray();
        rfc2898DeriveBytes1.Reset();
        Aes aes2 = Aes.Create();
        aes2.Key = rfc2898DeriveBytes2.GetBytes(16 /*0x10*/);
        aes2.IV = aes1.IV;
        MemoryStream memoryStream2 = new MemoryStream();
        CryptoStream cryptoStream2 = new CryptoStream((Stream) memoryStream2, aes2.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream2.Write(array, 0, array.Length);
        cryptoStream2.Flush();
        cryptoStream2.Close();
        rfc2898DeriveBytes2.Reset();
        string str = new UTF8Encoding(false).GetString(memoryStream2.ToArray());
        if (!s.Equals(str))
        {
          Console.WriteLine("Error: The two values are not equal.");
        }
        else
        {
          Console.WriteLine("The two values are equal.");
          Console.WriteLine("k1 iterations: {0}", (object) rfc2898DeriveBytes1.IterationCount);
          Console.WriteLine("k2 iterations: {0}", (object) rfc2898DeriveBytes2.IterationCount);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: {0}", (object) ex);
      }
    }
  }
}
