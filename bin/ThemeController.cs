// Decompiled with JetBrains decompiler
// Type: iMonnit.ThemeController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

#nullable disable
namespace iMonnit;

public abstract class ThemeController : Controller
{
  private static int _SensorCount;
  public static eProgramLevel _ProgramLevel;

  protected override void Execute(RequestContext requestContext)
  {
    requestContext.HttpContext.Items[(object) "themeName"] = (object) MonnitViewEngine.GetCurrentThemeName(requestContext);
    base.Execute(requestContext);
  }

  public bool IsLocalUrl(string url)
  {
    return !string.IsNullOrEmpty(url) && (url[0] == '/' && (url.Length == 1 || url[1] != '/' && url[1] != '\\') || url.Length > 1 && url[0] == '~' && url[1] == '/');
  }

  protected void RecordEULA()
  {
    Account account = MonnitSession.CurrentCustomer.Account;
    account.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
    account.EULADate = DateTime.UtcNow;
    account.Save();
    FormsAuthentication.SetAuthCookie(MonnitSession.CurrentCustomer.UserName, false);
    MonnitSession.CurrentCustomer.LastLoginDate = DateTime.UtcNow;
    MonnitSession.CurrentCustomer.Save();
  }

  public static int SensorCount()
  {
    try
    {
      ThemeController._SensorCount = TimedCache.RetrieveObject<int>(nameof (SensorCount));
      if (ThemeController._SensorCount <= 0)
      {
        ThemeController._SensorCount = Sensor.totalCount();
        if (ThemeController._SensorCount > 0)
          TimedCache.AddObjectToCach(nameof (SensorCount), (object) ThemeController._SensorCount, new TimeSpan(0, 10, 0));
      }
    }
    catch
    {
    }
    return ThemeController._SensorCount;
  }

  public eProgramLevel ProgramLevel()
  {
    if (ThemeController._ProgramLevel != 0)
      return ThemeController._ProgramLevel;
    ThemeController._ProgramLevel = eProgramLevel.NKR;
    string path = this.Server.MapPath($"~/{Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.MachineName)).TrimEnd('=')}.ky");
    if (System.IO.File.Exists(path))
    {
      string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(System.IO.File.ReadAllText(path))).Split('|');
      eProgramLevel result;
      if (strArray.Length >= 2 && strArray[1] == Environment.MachineName && Enum.TryParse<eProgramLevel>(strArray[0], out result))
        ThemeController._ProgramLevel = result;
    }
    else
      ThemeController._ProgramLevel = eProgramLevel.NKR;
    return ThemeController._ProgramLevel;
  }

  protected static string GenerateAccessToken()
  {
    int capacity = 6;
    char[] chArray = new char[62];
    char[] charArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
    byte[] data = new byte[1];
    using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
    {
      cryptoServiceProvider.GetNonZeroBytes(data);
      data = new byte[capacity];
      cryptoServiceProvider.GetNonZeroBytes(data);
    }
    StringBuilder stringBuilder = new StringBuilder(capacity);
    foreach (byte num in data)
      stringBuilder.Append(charArray[(int) num % charArray.Length]);
    return stringBuilder.ToString();
  }

  public static bool CheckBlacklist(
    string ipAddress,
    int blacklistTime,
    int blacklistCount,
    out IPBlacklist result,
    out double minuteToWait)
  {
    minuteToWait = 0.0;
    IPBlacklist ipBlacklist1 = (IPBlacklist) null;
    foreach (IPBlacklist ipBlacklist2 in IPBlacklist.LoadByIP(ipAddress))
    {
      DateTime dateTime1 = ipBlacklist2.FirstFailedAttempt;
      dateTime1 = dateTime1.AddMinutes((double) blacklistTime);
      double totalSeconds = dateTime1.Subtract(DateTime.UtcNow).TotalSeconds;
      if (totalSeconds > 0.0 && ipBlacklist2.FailedAttempts > blacklistCount)
      {
        minuteToWait = Math.Ceiling(totalSeconds / 60.0);
        ++ipBlacklist2.FailedAttempts;
        if (ipBlacklist2.FailedAttempts > blacklistCount * 3)
        {
          IPBlacklist ipBlacklist3 = ipBlacklist2;
          dateTime1 = DateTime.UtcNow;
          DateTime dateTime2 = dateTime1.AddMinutes(60.0);
          ipBlacklist3.FirstFailedAttempt = dateTime2;
        }
        ipBlacklist2.Save();
        result = (IPBlacklist) null;
        return true;
      }
      if (totalSeconds < 0.0)
        ipBlacklist2.Delete();
      else if (ipBlacklist1 == null || ipBlacklist1.FailedAttempts < ipBlacklist2.FailedAttempts)
        ipBlacklist1 = ipBlacklist2;
    }
    if (ipBlacklist1 == null)
    {
      ipBlacklist1 = new IPBlacklist();
      ipBlacklist1.FirstFailedAttempt = DateTime.UtcNow;
      ipBlacklist1.IPAddress = ipAddress;
    }
    result = ipBlacklist1;
    return false;
  }

  [AuthorizeDefault]
  public ActionResult PermissionError(
    string errorMessage = null,
    string errorSubject = null,
    [CallerMemberName] string methodName = "",
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
  {
    string pattern = "(?!.*\\\\).*(?=\\.)";
    string str = $"{Regex.Matches(Regex.Matches(sourceFilePath, pattern).OfType<Match>().Select<Match, string>((Func<Match, string>) (x => x.Value)).DefaultIfEmpty<string>("Unknown").FirstOrDefault<string>(), ".*(?=Controller)").OfType<Match>().Select<Match, string>((Func<Match, string>) (x => x.Value)).DefaultIfEmpty<string>("Unknown").FirstOrDefault<string>()}/{methodName}";
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorTranslateTag = (str + "|"),
      ErrorTitle = ("Unauthorized access to " + str),
      ErrorMessage = (errorMessage ?? $"You do not have permission to access {str}.")
    });
  }

  public static class PermissionErrorMessage
  {
    private const string IsAuthorizedForAccountMessage = "Unauthorized Access to Account: ";
    private const string IsAdminMessage = "Access Restricted to Admins";
    private const string AccountCanMulipleUsersMessage = "Multiple Users Not Authorized for Subscription Type: ";
    private const string MissingPermissionMessage = "Access Requires Permission: ";
    private const string MissingOrInvalidKeyMessage = "Missing or Invalid Key";

    public static string IsAuthorizedForAccount(long accountId)
    {
      return ((HtmlHelper) null).TranslateTag("Unauthorized Access to Account: ") + accountId.ToString();
    }

    public static string IsAdmin()
    {
      return ((HtmlHelper) null).TranslateTag("Access Restricted to Admins");
    }

    public static string AccountCanMulipleUsers(string subscriptionName = null)
    {
      if (string.IsNullOrEmpty(subscriptionName))
        subscriptionName = MonnitSession.CurrentSubscription.SubscriptionName;
      return ((HtmlHelper) null).TranslateTag("Multiple Users Not Authorized for Subscription Type: ") + subscriptionName;
    }

    public static string MissingCustomerPermission(string customerPermissionTypeName)
    {
      string description = CustomerPermissionType.Find(customerPermissionTypeName).Description;
      string str = string.IsNullOrEmpty(description) ? customerPermissionTypeName : description;
      return ((HtmlHelper) null).TranslateTag("Access Requires Permission: ") + str;
    }

    public static string MissingOrInvalidKey()
    {
      return ((HtmlHelper) null).TranslateTag("Missing or Invalid Key");
    }
  }
}
