// Decompiled with JetBrains decompiler
// Type: Monnit.MonnitUtil
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

#nullable disable
namespace Monnit;

public static class MonnitUtil
{
  public static int MinPasswordLength
  {
    get
    {
      int num = ConfigData.AppSettings(nameof (MinPasswordLength)).ToInt();
      return num > 0 ? num : 8;
    }
  }

  public static bool IsValidPassword(string passwordText, AccountTheme accountTheme)
  {
    if (passwordText == null)
      return false;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    foreach (char c in passwordText)
    {
      if (c >= 'a' && c <= 'z')
        flag4 = true;
      if (c >= 'A' && c <= 'Z')
        flag3 = true;
      if (c >= '0' && c <= '9')
        flag1 = true;
      if (!char.IsLetterOrDigit(c))
        flag2 = true;
    }
    bool RequiresSpecial;
    bool RequiresNumber;
    bool RequiresMixed;
    int MinPasswordLength;
    MonnitUtil.PasswordRequirements(accountTheme, out RequiresSpecial, out RequiresNumber, out RequiresMixed, out MinPasswordLength);
    if (RequiresSpecial && !flag2)
    {
      MonnitUtil.PasswordHelperString(accountTheme);
      return false;
    }
    if (RequiresNumber && !flag1)
    {
      MonnitUtil.PasswordHelperString(accountTheme);
      return false;
    }
    if (RequiresMixed && (!flag3 || !flag4))
    {
      MonnitUtil.PasswordHelperString(accountTheme);
      return false;
    }
    if (passwordText.Length >= MinPasswordLength)
      return true;
    MonnitUtil.PasswordHelperString(accountTheme);
    return false;
  }

  public static string PasswordRequirementsString(AccountTheme accountTheme)
  {
    bool passwordRequiresSpecial = ConfigData.AppSettings("PasswordRequiresSpecial").ToBool();
    bool passwordRequiresNumber = ConfigData.AppSettings("PasswordRequiresNumber").ToBool();
    bool passwordRequiresMixed = ConfigData.AppSettings("PasswordRequiresMixed").ToBool();
    int minPasswordLength = ConfigData.AppSettings("MinPasswordLength").ToInt();
    if (accountTheme != null)
    {
      passwordRequiresSpecial = accountTheme.PasswordRequiresSpecial;
      passwordRequiresNumber = accountTheme.PasswordRequiresNumber;
      passwordRequiresMixed = accountTheme.PasswordRequiresMixed;
      minPasswordLength = accountTheme.MinPasswordLength;
    }
    return $"{passwordRequiresSpecial.ToString()}|{passwordRequiresNumber.ToString()}|{passwordRequiresMixed.ToString()}|{minPasswordLength.ToString()}";
  }

  public static int DefaultPremiumLength(AccountTheme acctTheme)
  {
    int defaultPremiumDays = ConfigData.AppSettings("DefaultPremiumDays").ToInt();
    if (acctTheme != null)
      defaultPremiumDays = acctTheme.DefaultPremiumDays;
    return defaultPremiumDays;
  }

  public static long DefaultAccountSubscription(AccountTheme acctTheme)
  {
    long num = 5;
    if (acctTheme != null)
      num = acctTheme.DefaultAccountSubscriptionTypeID;
    return num;
  }

  private static void PasswordRequirements(
    AccountTheme accountTheme,
    out bool RequiresSpecial,
    out bool RequiresNumber,
    out bool RequiresMixed,
    out int MinPasswordLength)
  {
    RequiresSpecial = ConfigData.AppSettings("PasswordRequiresSpecial").ToBool();
    RequiresNumber = ConfigData.AppSettings("PasswordRequiresNumber").ToBool();
    RequiresMixed = ConfigData.AppSettings("PasswordRequiresMixed").ToBool();
    MinPasswordLength = ConfigData.AppSettings(nameof (MinPasswordLength)).ToInt();
    if (accountTheme == null)
      return;
    RequiresSpecial = accountTheme.PasswordRequiresSpecial;
    RequiresNumber = accountTheme.PasswordRequiresNumber;
    RequiresMixed = accountTheme.PasswordRequiresMixed;
    MinPasswordLength = accountTheme.MinPasswordLength;
  }

  public static string PasswordHelperString(AccountTheme acctTheme)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool RequiresSpecial;
    bool RequiresNumber;
    bool RequiresMixed;
    int MinPasswordLength;
    MonnitUtil.PasswordRequirements(acctTheme, out RequiresSpecial, out RequiresNumber, out RequiresMixed, out MinPasswordLength);
    if (RequiresSpecial)
      flag2 = true;
    if (RequiresNumber)
      flag3 = true;
    if (RequiresMixed)
      flag1 = true;
    return $"Password must contain at least:{(flag1 ? (object) "\n 1 uppercase and 1 lowercase letter," : (object) "")}{(flag2 ? (object) "\n 1 special character," : (object) "")}{(flag3 ? (object) "\n 1 number," : (object) "")}{$"\n {MinPasswordLength} characters."}";
  }

  public static string LogFileDirectory
  {
    get
    {
      string logFileDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetAssembly(typeof (MonnitUtil)).Location));
      if (!string.IsNullOrEmpty(ConfigData.AppSettings("logfilePath")))
      {
        logFileDirectory = ConfigData.AppSettings("logfilePath");
        foreach (Environment.SpecialFolder folder in System.Enum.GetValues(typeof (Environment.SpecialFolder)))
          logFileDirectory = logFileDirectory.Replace($"[{System.Enum.GetName(typeof (Environment.SpecialFolder), (object) folder)}]", Environment.GetFolderPath(folder));
      }
      if (!logFileDirectory.EndsWith("/") && !logFileDirectory.EndsWith("\\"))
        logFileDirectory += "\\";
      return logFileDirectory;
    }
  }

  public static SmtpClient GetSMTPClient(MailMessage mail, Account accnt)
  {
    return MonnitUtil.GetSMTPClient(mail, AccountTheme.Find(accnt));
  }

  public static SmtpClient GetSMTPClient(MailMessage mail, AccountTheme theme)
  {
    SmtpClient smtpClient = (SmtpClient) null;
    try
    {
      string address;
      string displayName;
      if (theme != null)
      {
        address = !string.IsNullOrEmpty(theme.SMTPDefaultFrom) ? theme.SMTPDefaultFrom : ConfigData.AppSettings("SMTPDefaultFrom");
        displayName = !string.IsNullOrEmpty(theme.SMTPFriendlyName) ? theme.SMTPFriendlyName : (!string.IsNullOrWhiteSpace(ConfigData.AppSettings("SMTPFriendlyName")) ? ConfigData.AppSettings("SMTPFriendlyName") : ConfigData.AppSettings("SMTPFreindlyName"));
        if (!string.IsNullOrEmpty(theme.SMTP))
        {
          smtpClient = new SmtpClient(theme.SMTP);
          smtpClient.Port = theme.SMTPPort;
          smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(theme.SMTPUser, theme.SMTPPasswordPlainText);
          smtpClient.EnableSsl = theme.SMTPUseSSL;
        }
        if (!string.IsNullOrEmpty(theme.SMTPReturnPath))
        {
          mail.Headers.Add("Return-Path", theme.SMTPReturnPath);
          mail.Headers.Add("From", theme.SMTPReturnPath);
          mail.Sender = new MailAddress(theme.SMTPReturnPath);
        }
      }
      else
      {
        address = ConfigData.AppSettings("SMTPDefaultFrom");
        displayName = !string.IsNullOrWhiteSpace(ConfigData.AppSettings("SMTPFriendlyName")) ? ConfigData.AppSettings("SMTPFriendlyName") : ConfigData.AppSettings("SMTPFreindlyName");
      }
      mail.From = new MailAddress(address, displayName);
      if (smtpClient == null)
      {
        smtpClient = new SmtpClient(ConfigData.AppSettings("SMTP"));
        smtpClient.Port = ConfigData.AppSettings("SMTPPort").ToInt();
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(ConfigData.AppSettings("SMTPUser"), ConfigData.AppSettings("SMTPPassword"));
        smtpClient.EnableSsl = ConfigData.AppSettings("SMTPUseSSL").ToBool();
        if (!string.IsNullOrEmpty(ConfigData.AppSettings("SMTPReturnPath")))
        {
          mail.Headers.Add("Return-Path", ConfigData.AppSettings("SMTPReturnPath"));
          mail.Headers.Add("From", ConfigData.AppSettings("SMTPReturnPath"));
          mail.Sender = new MailAddress(ConfigData.AppSettings("SMTPReturnPath"));
        }
      }
      return smtpClient;
    }
    catch (Exception ex)
    {
      smtpClient?.Dispose();
      ExceptionLog.Log(ex);
      throw new Exception("Error Configuring SMTP Server " + ex.Message);
    }
  }

  public static string GetFromNumberForVoice(Account accnt)
  {
    return MonnitUtil.GetFromNumberForVoice(AccountTheme.Find(accnt));
  }

  public static string GetFromNumberForVoice(AccountTheme theme)
  {
    try
    {
      string empty = string.Empty;
      return theme == null ? ConfigData.AppSettings("FromPhone") : (!string.IsNullOrEmpty(theme.FromPhone) ? theme.FromPhone : ConfigData.AppSettings("FromPhone"));
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      throw new Exception("Error Retrieving From Number " + ex.Message);
    }
  }

  public static string GetFromNumber(Account account, Country country)
  {
    try
    {
      AccountTheme accountTheme = AccountTheme.Find(account);
      string fromNumber = string.Empty;
      if (country != null && accountTheme != null && country.SupportsAlphanumeric && (!country.PreRegistrationRequired || country.PreRegistrationRequired && country.PreRegistrationCompleted))
        fromNumber = accountTheme.AlphanumericSenderID;
      else if (accountTheme != null)
        fromNumber = accountTheme.FromPhone;
      return fromNumber;
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      throw new Exception("Error Retrieving From Number " + ex.Message);
    }
  }

  public static bool SendMail(string to, string subject, string body, Account account)
  {
    using (MailMessage mail = new MailMessage())
    {
      try
      {
        mail.To.Clear();
        mail.To.SafeAdd(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        throw new Exception("Error Building Email " + ex.Message);
      }
      return MonnitUtil.SendMail(mail, account);
    }
  }

  public static bool SendMail(MailMessage mail, Account account)
  {
    return MonnitUtil.SendMail(mail, AccountTheme.Find(account));
  }

  public static bool SendMail(MailMessage mail, AccountTheme theme)
  {
    using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, theme))
      return MonnitUtil.SendMail(mail, smtpClient);
  }

  public static bool SendMail(MailMessage mail, SmtpClient smtp)
  {
    try
    {
      if (mail.To.Count == 0)
        return false;
      if (!string.IsNullOrEmpty(ConfigData.AppSettings("SMTPBccUser")))
      {
        bool flag = true;
        foreach (MailAddress mailAddress in (Collection<MailAddress>) mail.Bcc)
        {
          if (mailAddress.Address == ConfigData.AppSettings("SMTPBccUser"))
            flag = false;
        }
        if (flag)
          mail.Bcc.Add(ConfigData.AppSettings("SMTPBccUser"));
      }
      try
      {
        if (ConfigData.AppSettings("EnableDebugLog").ToBool())
          new DebugLog()
          {
            IdentifierType = "MonnitUtil.SendMail",
            IdentifierValue = long.MinValue,
            Severity = long.MinValue,
            Server = "",
            LogDate = DateTime.UtcNow,
            CodePath = "MonnitUtil.SendMail",
            Message = $"SMTPHost: {smtp.Host} Subject: {mail.Subject} To: {mail.To.ToString()}"
          }.Save();
      }
      catch (Exception ex)
      {
        ex.Log($"MonnitUtil.SendMail(MailMessage.From.User = {mail.From.User}, MailMessage.Sender.DisplayName = {mail.Sender.DisplayName}, MailMessage.Sender.Address = {mail.Sender.Address}, MailMessage.Sender.DisplayName = {mail.Subject}, SmtpClient.Host = {smtp.Host}, SmtpClient.Port = {smtp.Port}");
      }
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
      smtp.Send(mail);
      smtp.Dispose();
      return true;
    }
    catch (Exception ex)
    {
      ex.Log(WindowsIdentity.GetCurrent().Name + ".MonnitUtil.SendMail");
      throw new Exception("SMTP error " + ex.Message);
    }
  }

  public static string CheckDigit(long input)
  {
    long num1 = input;
    long num2 = 0;
    try
    {
      for (; input != 0L; input /= 10L)
        num2 = num2 * 10L + input % 10L;
      char[] baseChars = new char[26]
      {
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'I',
        'J',
        'K',
        'L',
        'M',
        'N',
        'O',
        'P',
        'Q',
        'R',
        'S',
        'T',
        'U',
        'V',
        'W',
        'X',
        'Y',
        'Z'
      };
      string str = MonnitUtil.IntToBase(num2, baseChars).PadLeft(4, 'A');
      return str.Substring(str.Length - 4);
    }
    catch (Exception ex)
    {
      string str = $"Error while checking the Sensor or Gateway Code ({num1}): {ex.Message}";
      ex.Log(str);
      throw new Exception(str);
    }
  }

  public static bool ValidateCheckDigit(long input, string checkDigit)
  {
    try
    {
      if (string.IsNullOrEmpty(checkDigit))
        return false;
      string str = MonnitUtil.CheckDigit(input);
      if (str == checkDigit)
        return true;
      return checkDigit.Length >= 3 && str == checkDigit.Substring(2, checkDigit.Length - 2);
    }
    catch (Exception ex)
    {
      ex.Log($"MonnitUtil.ValidateCheckDigit(input = {input}, checkDigit = {checkDigit})");
      return false;
    }
  }

  public static int ReverseCheckDigit(string input)
  {
    if (input.Length != 6 && input.Length != 4)
      return 0;
    char[] charArray = input.ToCharArray();
    char[] chArray1 = new char[26]
    {
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z'
    };
    char[] chArray2 = new char[32 /*0x20*/];
    int length = chArray1.Length;
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    for (int index = length - 1; index >= 0; --index)
    {
      if ((int) charArray[0] == (int) chArray1[index])
        num1 = index;
      if ((int) charArray[1] == (int) chArray1[index])
        num2 = index;
      if ((int) charArray[2] == (int) chArray1[index])
        num3 = index;
      if ((int) charArray[3] == (int) chArray1[index])
        num4 = index;
    }
    int num5 = ((double) (num4 + num3 * length) + (double) num2 * Math.Pow((double) length, 2.0) + (double) num1 * Math.Pow((double) length, 3.0)).ToInt();
    int num6 = 0;
    for (; num5 != 0; num5 /= 10)
      num6 = num6 * 10 + num5 % 10;
    return num6;
  }

  public static string FormatBytesToStringArray(byte[] b) => b.FormatBytesToStringArray();

  public static byte[] FormatStringToByteArray(string s) => s.FormatStringToByteArray();

  public static string IntToBase36(long value)
  {
    char[] baseChars = new char[36]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z'
    };
    return MonnitUtil.IntToBase(value, baseChars);
  }

  private static string IntToBase62(long value)
  {
    char[] baseChars = new char[62]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z',
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z'
    };
    return MonnitUtil.IntToBase(value, baseChars);
  }

  private static string IntToBase(long value, char[] baseChars)
  {
    int sourceIndex = 32 /*0x20*/;
    char[] sourceArray = new char[sourceIndex];
    int length = baseChars.Length;
    do
    {
      sourceArray[--sourceIndex] = baseChars[value % (long) length];
      value /= (long) length;
    }
    while (value > 0L);
    char[] destinationArray = new char[32 /*0x20*/ - sourceIndex];
    Array.Copy((Array) sourceArray, sourceIndex, (Array) destinationArray, 0, 32 /*0x20*/ - sourceIndex);
    return new string(destinationArray);
  }

  private static long BaseToInt(string input, char[] baseChars)
  {
    int length1 = input.Length;
    int[] numArray = new int[length1];
    char[] charArray = input.ToCharArray();
    int length2 = baseChars.Length;
    for (int index1 = length2 - 1; index1 >= 0; --index1)
    {
      for (int index2 = 0; index2 < length1; ++index2)
      {
        if ((int) charArray[index2] == (int) baseChars[index1])
          numArray[index2] = index1;
      }
    }
    double o = 0.0;
    for (int y = 0; y < length1; ++y)
      o += (double) numArray[length1 - 1 - y] * Math.Pow((double) length2, (double) y);
    return o.ToLong();
  }

  public static void FillVariableLengthConfig(
    ref byte[] configurationPage,
    int start,
    int maxLength,
    byte[] value)
  {
    Array.Copy((Array) new byte[maxLength], 0, (Array) configurationPage, start, maxLength);
    Array.Copy((Array) value, 0, (Array) configurationPage, start, value.Length > maxLength - 1 ? maxLength - 1 : value.Length);
  }

  public static string HttpPost(
    string url,
    NameValueCollection data,
    NetworkCredential credentials)
  {
    byte[] bytes = (byte[]) null;
    using (WebClient webClient = new WebClient())
    {
      if (credentials != null)
        webClient.Credentials = (ICredentials) credentials;
      bytes = webClient.UploadValues(url, data);
    }
    return Encoding.UTF8.GetString(bytes);
  }

  public static string HttpGet(string url, NameValueCollection data, NetworkCredential credentials)
  {
    string str = string.Empty;
    using (WebClient webClient = new WebClient())
    {
      if (credentials != null)
        webClient.Credentials = (ICredentials) credentials;
      str = webClient.DownloadString(url);
    }
    return str;
  }

  public static bool CheckDBConnection() => new Monnit.Data.MonnitUtil.CheckDBConnection().Result;

  public static bool ExternalMessageTable()
  {
    return ConfigData.AppSettings(nameof (ExternalMessageTable), "False").ToBool();
  }

  public static bool UseEncryption()
  {
    try
    {
      return !(ConfigData.AppSettings(nameof (UseEncryption), "True").ToLower() == "false");
    }
    catch
    {
      return true;
    }
  }

  public static Bitmap GetCustomerImageBitmap(Stream str)
  {
    Bitmap customerImageBitmap1 = (Bitmap) Image.FromStream(str);
    if (customerImageBitmap1.Width * customerImageBitmap1.Height < 10000)
      return customerImageBitmap1;
    int width = Math.Sqrt(10000.0 * (double) customerImageBitmap1.Height / (double) customerImageBitmap1.Width).ToInt();
    int height = (10000.0 / (double) width).ToInt();
    using (customerImageBitmap1)
    {
      Bitmap customerImageBitmap2 = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      using (Graphics graphics = Graphics.FromImage((Image) customerImageBitmap2))
      {
        graphics.Clear(Color.Transparent);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage((Image) customerImageBitmap1, new Rectangle(0, 0, width, height), new Rectangle(0, 0, customerImageBitmap1.Width, customerImageBitmap1.Height), GraphicsUnit.Pixel);
      }
      return customerImageBitmap2;
    }
  }

  public static byte[] GenerateSalt()
  {
    byte[] data = new byte[24];
    using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
      cryptoServiceProvider.GetBytes(data);
    return data;
  }

  public static byte[] GenerateHash(string password, byte[] salt, int iterations)
  {
    using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(password), salt, iterations))
      return rfc2898DeriveBytes.GetBytes(24);
  }

  public static string CreateKeyValue(int length)
  {
    StringBuilder stringBuilder = new StringBuilder();
    Random random = new Random(Guid.NewGuid().GetHashCode());
    for (int index = 0; index < length; ++index)
    {
      char ch = "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZz0123456789"[random.Next(0, "abcdefghijklmnopqrstuvwxyABCDEFGHIJKLMNOPQRSTUVWXYZz0123456789".Length)];
      stringBuilder.Append(ch);
    }
    return stringBuilder.ToString();
  }

  public static XDocument SendToSecured(string address, string body, int timeout = 30000)
  {
    try
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(address);
      httpWebRequest.Timeout = timeout;
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "application/json";
      byte[] bytes = new ASCIIEncoding().GetBytes(body);
      httpWebRequest.ContentLength = (long) bytes.Length;
      using (Stream requestStream = httpWebRequest.GetRequestStream())
      {
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
      }
      string text = "";
      using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
      {
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
          text = streamReader.ReadToEnd();
          streamReader.Close();
        }
        response.Close();
      }
      return XDocument.Parse(text);
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.SendToSecured ");
      throw ex;
    }
  }

  public static Cable LookUpCable(long cableID)
  {
    XDocument xdocument = XDocument.Load(string.Format("{1}/xml/LookUpCable?cableID={0}", (object) cableID, (object) ConfigData.FindValue("LookUpHost")));
    return xdocument.Descendants((XName) "APILookUpCable").FirstOrDefault<XElement>() == null ? (Cable) null : xdocument.Descendants((XName) "APILookUpCable").Select<XElement, Cable>((Func<XElement, Cable>) (c => new Cable()
    {
      CableID = cableID,
      CreateDate = c.Attribute((XName) "CreateDate").Value.ToDateTime(),
      SKU = c.Attribute((XName) "SKU").Value.ToString(),
      ApplicationID = c.Attribute((XName) "ApplicationID").Value.ToLong(),
      CableMinorRevision = c.Attribute((XName) "CableMinorRevision").Value.ToInt(),
      CableMajorRevision = c.Attribute((XName) "CableMajorRevision").Value.ToInt()
    })).FirstOrDefault<Cable>();
  }

  public static void SendTwilioSMS(
    long notificationRecordedID,
    Customer cust,
    string FromNumber,
    string ToNumber,
    string body)
  {
    try
    {
      string newValue = ConfigData.AppSettings("TwilioAccountSid");
      string userName = ConfigData.AppSettings("TwilioAuthSid");
      string password = ConfigData.AppSettings("TwilioAuthSecret");
      string url = $"{ConfigData.AppSettings("TwilioBaseAPI")}{ConfigData.AppSettings("TwilioMessageResource")}".Replace("{AccountSid}", newValue);
      string str = $"{AccountTheme.BaseURL(cust.Account)}/api/TwilioMessageCallback?notificationRecordedID={notificationRecordedID}";
      body = new Regex("[\"!#$%*+;<=>@[\\]^\\\\_`{|}~°º]").Replace(body, "");
      body = Notification.ReplaceEncodedTextWithAngleBrackets(body);
      System.Text.Encoder encoder = new UTF7Encoding().GetEncoder();
      char[] charArray = body.ToCharArray();
      byte[] bytes = new byte[charArray.Length];
      int charsUsed = 0;
      int bytesUsed = 0;
      bool completed = false;
      encoder.Convert(charArray, 0, charArray.Length, bytes, 0, charArray.Length, true, out charsUsed, out bytesUsed, out completed);
      int length;
      if (completed)
      {
        body = Encoding.UTF7.GetString(bytes, 0, bytes.Length);
        length = body.Length > 150 ? 150 : body.Length;
      }
      else
        length = body.Length > 150 ? 150 : body.Length;
      body = body.Substring(0, length);
      MonnitUtil.HttpPost(url, new NameValueCollection()
      {
        {
          "From",
          FromNumber
        },
        {
          "To",
          ToNumber
        },
        {
          "Body",
          body
        },
        {
          "StatusCallback",
          str
        }
      }, new NetworkCredential(userName, password));
    }
    catch (Exception ex)
    {
      ex.Log($"MonnitUtil.SendTwillioSMS[NotificationRecordedID: {notificationRecordedID.ToString()}][CustomerID: {cust.CustomerID.ToString()}][From: {FromNumber}][To: {ToNumber}] ");
    }
  }

  public static void SendTwilioSMS(string FromNumber, string ToNumber, string body)
  {
    try
    {
      string newValue = ConfigData.AppSettings("TwilioAccountSid");
      string userName = ConfigData.AppSettings("TwilioAuthSid");
      string password = ConfigData.AppSettings("TwilioAuthSecret");
      string url = $"{ConfigData.AppSettings("TwilioBaseAPI")}{ConfigData.AppSettings("TwilioMessageResource")}".Replace("{AccountSid}", newValue);
      body = new Regex("[\"!#$%&*+;<=>@[\\]^\\\\_`{|}~°º]").Replace(body, "");
      System.Text.Encoder encoder = new UTF7Encoding().GetEncoder();
      char[] charArray = body.ToCharArray();
      byte[] bytes = new byte[charArray.Length];
      int charsUsed = 0;
      int bytesUsed = 0;
      bool completed = false;
      encoder.Convert(charArray, 0, charArray.Length, bytes, 0, charArray.Length, true, out charsUsed, out bytesUsed, out completed);
      int length;
      if (completed)
      {
        body = Encoding.UTF7.GetString(bytes, 0, bytes.Length);
        length = body.Length > 150 ? 150 : body.Length;
      }
      else
        length = body.Length > 65 ? 65 : body.Length;
      body = body.Substring(0, length);
      MonnitUtil.HttpPost(url, new NameValueCollection()
      {
        {
          "From",
          FromNumber
        },
        {
          "To",
          ToNumber
        },
        {
          "Body",
          body
        }
      }, new NetworkCredential(userName, password));
    }
    catch (Exception ex)
    {
      ex.Log($"Notification.SendTwillioSMS[From: {FromNumber}][To: {ToNumber}] ");
    }
  }

  public static void SendTwilioCallback(
    NotificationRecorded notificationRecorded,
    Customer cust,
    string FromNumber,
    string ToNumber)
  {
    try
    {
      string newValue = ConfigData.AppSettings("TwilioAccountSid");
      string userName = ConfigData.AppSettings("TwilioAuthSid");
      string password = ConfigData.AppSettings("TwilioAuthSecret");
      string url = $"{ConfigData.AppSettings("TwilioBaseAPI")}{ConfigData.AppSettings("TwilioCallResource")}".Replace("{AccountSid}", newValue);
      string str1 = AccountTheme.BaseURL(cust.Account);
      string str2 = $"{str1}/notification/TwilioCall/{notificationRecorded.CustomerID}?notificationRecordedID={notificationRecorded.NotificationRecordedID}";
      string str3 = $"{str1}/api/TwilioCallCallback?notificationRecordedID={notificationRecorded.NotificationRecordedID}";
      MonnitUtil.HttpPost(url, new NameValueCollection()
      {
        {
          "From",
          FromNumber
        },
        {
          "To",
          ToNumber
        },
        {
          "Url",
          str2
        },
        {
          "StatusCallback",
          str3
        }
      }, new NetworkCredential(userName, password));
    }
    catch (Exception ex)
    {
      ex.Log($"Notification.SendTwilioCallback[From: {FromNumber}][To: {ToNumber}] ");
    }
  }

  public static void SendTwilioVoiceMessage(string FromNumber, string ToNumber, string message)
  {
    try
    {
      string newValue = ConfigData.AppSettings("TwilioAccountSid");
      string userName = ConfigData.AppSettings("TwilioAuthSid");
      string password = ConfigData.AppSettings("TwilioAuthSecret");
      MonnitUtil.HttpPost($"{ConfigData.AppSettings("TwilioBaseAPI")}{ConfigData.AppSettings("TwilioCallResource")}".Replace("{AccountSid}", newValue), new NameValueCollection()
      {
        {
          "From",
          FromNumber
        },
        {
          "To",
          ToNumber
        },
        {
          "Twiml",
          $"<?xml version='1.0' encoding='UTF-8'?><Response><Say>{message}</Say> </Response>"
        }
      }, new NetworkCredential(userName, password));
    }
    catch (Exception ex)
    {
      ex.Log($"Notification.SendTwilioVoiceMessage[From: {FromNumber}][To: {ToNumber}] ");
    }
  }

  public static void SendTwilioPhoneVerification(
    Validation v,
    Account account,
    long customerID,
    string FromNumber,
    string ToNumber)
  {
    try
    {
      string newValue = ConfigData.AppSettings("TwilioAccountSid");
      string userName = ConfigData.AppSettings("TwilioAuthSid");
      string password = ConfigData.AppSettings("TwilioAuthSecret");
      string url = $"{ConfigData.AppSettings("TwilioBaseAPI")}{ConfigData.AppSettings("TwilioCallResource")}".Replace("{AccountSid}", newValue);
      string str1 = AccountTheme.BaseURL(account);
      string str2 = $"{str1}/notification/PhoneVerification/{v.CustomerID}?validationID={v.ValidationID}";
      string str3 = $"{str1}/api/PhoneVerificationCallback?validationID={v.ValidationID}";
      account.LogAuditData(eAuditAction.Create, eAuditObject.Notification, customerID, account.AccountID, $"call: {str2}, callback: {str3}, From: {FromNumber}, To: {ToNumber}");
      MonnitUtil.HttpPost(url, new NameValueCollection()
      {
        {
          "From",
          FromNumber
        },
        {
          "To",
          ToNumber
        },
        {
          "Url",
          str2
        },
        {
          "StatusCallback",
          str3
        }
      }, new NetworkCredential(userName, password));
    }
    catch (Exception ex)
    {
      ex.Log($"Notification.SendTwilioPhoneVerification[From: {FromNumber}][To: {ToNumber}] ");
    }
  }

  public static string GetMEATempAuthToken()
  {
    string key = "MEATempAuthToken";
    string meaTempAuthToken = TimedCache.RetrieveObject<string>(key);
    if (!string.IsNullOrEmpty(meaTempAuthToken))
      return meaTempAuthToken;
    string str = ConfigData.AppSettings("MEA_API_Location");
    try
    {
      meaTempAuthToken = XDocument.Load($"{str}xml?applicationAuthGuid={ConfigData.FindValue("MEA_API_Auth_Guid")}").Descendants((XName) "object").Single<XElement>().Attribute((XName) "auth").Value;
      TimedCache.AddObjectToCach(key, (object) meaTempAuthToken, new TimeSpan(0, 1, 0));
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.GetMEATempAuthToken ");
    }
    return meaTempAuthToken;
  }

  public static string GetLatestEncryptedFirmwareVersion(string sku, bool isGatewayBase)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string encryptedFirmwareVersion = (string) null;
    string key = isGatewayBase ? "Util_GWFirmwareVersion" : "Util_RadioVersion";
    try
    {
      if (TimedCache.ContainsKey(key))
        dictionary = TimedCache.RetrieveObject<Dictionary<string, string>>(key);
      if (dictionary != null && dictionary.ContainsKey(sku))
      {
        encryptedFirmwareVersion = dictionary[sku];
      }
      else
      {
        encryptedFirmwareVersion = XDocument.Load($"{ConfigData.AppSettings("LookUpHost")}xml/GetLatestSKUFirmware/{""}?sku={sku}&isGatewayBase={isGatewayBase}").Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
        if (!string.IsNullOrWhiteSpace(encryptedFirmwareVersion) && !encryptedFirmwareVersion.Contains("Failed"))
        {
          dictionary[sku] = encryptedFirmwareVersion;
          TimedCache.RemoveObject(key);
          TimeSpan cacheLength = new TimeSpan(0, Convert.ToInt32(ConfigData.AppSettings("Util_FirmwareVersionCacheTime_Minutes", "120")), 0);
          TimedCache.AddObjectToCach(key, (object) dictionary, cacheLength);
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.GetLatestEncryptedFirmwareVersion ");
    }
    return encryptedFirmwareVersion;
  }

  public static string GetLatestFirmwareVersionFromMEA(string sku, bool isGatewayBase)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string firmwareVersionFromMea = (string) null;
    string key = isGatewayBase ? "Util_GWFirmwareVersion" : "Util_RadioVersion";
    try
    {
      if (TimedCache.ContainsKey(key))
        dictionary = TimedCache.RetrieveObject<Dictionary<string, string>>(key);
      if (dictionary != null && dictionary.ContainsKey(sku))
      {
        firmwareVersionFromMea = dictionary[sku];
      }
      else
      {
        uri = $"{ConfigData.AppSettings("MEA_API_Location")}xml/GetLatestSKUFirmware/{MonnitUtil.GetMEATempAuthToken()}?sku={sku}&isGatewayBase={isGatewayBase}";
        XDocument xdocument = XDocument.Load(uri);
        firmwareVersionFromMea = xdocument.Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
        if (!string.IsNullOrWhiteSpace(firmwareVersionFromMea) && !firmwareVersionFromMea.Contains("Failed"))
        {
          dictionary[sku] = firmwareVersionFromMea;
          TimedCache.RemoveObject(key);
          TimeSpan cacheLength = new TimeSpan(0, Convert.ToInt32(ConfigData.AppSettings("Util_FirmwareVersionCacheTime_Minutes", "120")), 0);
          TimedCache.AddObjectToCach(key, (object) dictionary, cacheLength);
        }
        if (flag)
        {
          TimeSpan timeSpan = DateTime.UtcNow - utcNow;
          new MeaApiLog()
          {
            MethodName = "MEA-GetLatestFirmwareVersionFromMEA",
            MachineName = Environment.MachineName,
            RequestBody = uri,
            ResponseBody = xdocument.ToString(),
            CreateDate = utcNow,
            SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
            IsException = false
          }.Save();
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.GetLatestFirmwareVersionFromMEA ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetLatestFirmwareVersionFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return firmwareVersionFromMea;
  }

  public static double RetrieveItemTaxValue(
    string sku,
    int qty,
    long addressID,
    long storeUserID,
    string sessionID,
    double price = 0.0)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/GetTaxForItem/{0}?", (object) MonnitUtil.GetMEATempAuthToken());
    string body = $"{{\"sku\":\"{sku}\",\"qty\":\"{qty.ToString()}\",\"price\":\"{price.ToString()}\",\"sessionID\":\"{sessionID}\",\"userProfileID\":\"{storeUserID.ToString()}\",\"addressID\":\"{addressID.ToString()}\" }}";
    double num = 0.0;
    try
    {
      XDocument secured = MonnitUtil.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveItemTaxValue",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      num = secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value.ToDouble();
      num = num >= 0.0 ? num : 0.0;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.RetrieveItemTaxValue  Message: ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveItemTaxValue",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return num;
  }

  public static string AuthorizePayment(
    string storeLinkGuid,
    long storeUserID,
    long addressID,
    string sku,
    int qty,
    Decimal tax,
    string sessionID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/AuthorizePayment/{0}?", (object) MonnitUtil.GetMEATempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{storeLinkGuid}\",\"userProfileID\":\"{storeUserID.ToString()}\",\"userAddressID\":\"{addressID.ToString()}\",\"sku\":\"{sku}\",\"qty\":\"{qty.ToString()}\",\"tax\":\"{tax.ToString()}\",\"sessionID\":\"{sessionID}\" }}";
    string str2;
    try
    {
      XDocument secured = MonnitUtil.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AuthorizePayment",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      str2 = secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.AuthorizePayment  Message: ");
      str2 = "Failed";
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AuthorizePayment",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return str2;
  }

  public static string CapturePayment(
    string storeLinkGuid,
    long orderID,
    long orderItemID,
    Decimal authID,
    Decimal total,
    long storeUserID,
    long addressID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/CapturePayment/{0}?", (object) MonnitUtil.GetMEATempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{storeLinkGuid}\",\"orderID\":\"{orderID.ToString()}\",\"orderItemID\":\"{orderItemID.ToString()}\",\"authID\":\"{authID.ToString()}\",\"total\":\"{total.ToString()}\",\"userProfileID\":\"{storeUserID.ToString()}\",\"userAddressID\":\"{addressID.ToString()}\" }}";
    string str2;
    try
    {
      XDocument secured = MonnitUtil.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CapturePayment",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      str2 = secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.CapturePayment  Message: ");
      str2 = "Failed";
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CapturePayment",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return str2;
  }

  public static string CancelAuthorization(
    Decimal authID,
    Decimal total,
    long orderID,
    long orderItemID,
    bool deleteOrder)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/CancelAuthorization/{0}?", (object) MonnitUtil.GetMEATempAuthToken());
    string body = $"{{\"authID\":\"{authID.ToString()}\",\"total\":\"{total.ToString()}\",\"orderID\":\"{orderID.ToString()}\",\"orderItemID\":\"{orderItemID.ToString()}\",\"deleteOrder\":\"{deleteOrder.ToString()}\" }}";
    string str2;
    try
    {
      XDocument secured = MonnitUtil.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CancelAuthorization",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      str2 = secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.CancelAuthorization  Message: ");
      str2 = "Failed";
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CancelAuthorization",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return str2;
  }

  public static string GetDataPlanStatusFromMEA(string simID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/GetDataPlanStatus/{0}?", (object) MonnitUtil.GetMEATempAuthToken());
    string body = $"{{\"simID\":\"{simID}\" }}";
    string planStatusFromMea;
    try
    {
      XDocument secured = MonnitUtil.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetDataPlanStatusFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      planStatusFromMea = secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitUtil.GetDataPlanStatusFromMEA: ");
      planStatusFromMea = "Inactive";
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetDataPlanStatusFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return planStatusFromMea;
  }
}
