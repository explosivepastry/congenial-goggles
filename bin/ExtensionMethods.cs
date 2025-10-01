// Decompiled with JetBrains decompiler
// Type: iMonnit.ExtensionMethods
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using HtmlAgilityPack;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

#nullable disable
namespace iMonnit;

public static class ExtensionMethods
{
  private static DateTime _EpochStartTime = new DateTime(1970, 1, 1);
  public static HashSet<string> BlackList = new HashSet<string>()
  {
    "script",
    "iframe",
    "form",
    "object",
    "embed",
    "link",
    "head",
    "meta",
    "<",
    ">"
  };

  public static Dictionary<int, string> EnumerationToDictionary<T>()
  {
    if (!typeof (T).IsEnum)
      throw new ArgumentException("Type must be an enum");
    return System.Enum.GetValues(typeof (T)).Cast<T>().ToDictionary<T, int, string>((System.Func<T, int>) (t => (int) (object) t), (System.Func<T, string>) (t => t.ToString()));
  }

  public static SelectList ToSelectList<TEnum>(this TEnum enumObj) where TEnum : struct
  {
    if (!typeof (TEnum).IsEnum)
      throw new ArgumentException("An Enumeration type is required.", nameof (enumObj));
    SelectList selectList = new SelectList((IEnumerable) System.Enum.GetValues(typeof (TEnum)).Cast<TEnum>().Select(e => new
    {
      ID = e.ToInt(),
      Name = e.ToString()
    }), "ID", "Name", (object) enumObj.ToInt());
    bool flag = selectList.SelectedValue.ToInt() == enumObj.ToInt();
    return selectList;
  }

  public static SelectList ToSelectList(this bool initialValue)
  {
    return new SelectList((IEnumerable) new List<string>()
    {
      "True",
      "False"
    }, (object) initialValue.ToString());
  }

  public static MvcHtmlString DropDownListBool(this HtmlHelper hlp, string name, bool defaultValue)
  {
    return hlp.DropDownListBool(name, "True", "False", defaultValue);
  }

  public static MvcHtmlString DropDownListBool(
    this HtmlHelper hlp,
    string name,
    string trueText,
    string falseText,
    bool defaultValue)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine($"<select id=\"{name}\" name=\"{name}\">");
    stringBuilder.AppendFormat("<option value='True' ");
    if (defaultValue)
      stringBuilder.Append("selected='selected' ");
    stringBuilder.AppendFormat(">{0}</option>\n", (object) trueText);
    stringBuilder.AppendFormat("<option value='False' ");
    if (!defaultValue)
      stringBuilder.Append("selected='selected' ");
    stringBuilder.AppendFormat(">{0}</option>\n", (object) falseText);
    stringBuilder.AppendLine("</select>");
    return MvcHtmlString.Create(stringBuilder.ToString());
  }

  public static MvcHtmlString DropDownList<TEnum>(this HtmlHelper hlp, string name, TEnum enumObj) where TEnum : struct
  {
    return hlp.DropDownList<TEnum>(name, "", enumObj);
  }

  public static MvcHtmlString DropDownList<TEnum>(
    this HtmlHelper hlp,
    string name,
    string className,
    TEnum enumObj)
    where TEnum : struct
  {
    if (!typeof (TEnum).IsEnum)
      throw new ArgumentException("An Enumeration type is required.", nameof (enumObj));
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"<select id=\"{name}\" name=\"{name}\"");
    if (!string.IsNullOrEmpty(className))
      stringBuilder.Append($" class=\"{className}\"");
    stringBuilder.AppendLine(">");
    foreach (string name1 in System.Enum.GetNames(typeof (TEnum)))
    {
      stringBuilder.AppendFormat("<option value='{0}' ", (object) name1);
      if (enumObj.ToString() == name1)
        stringBuilder.Append("selected='selected' ");
      stringBuilder.AppendFormat(">{0}</option>\n", (object) name1.Replace("_", " "));
    }
    stringBuilder.AppendLine("</select>");
    return MvcHtmlString.Create(stringBuilder.ToString());
  }

  public static MvcHtmlString DropDownListFor<TModel, TProperty, TDBObject>(
    this HtmlHelper<TModel> htmlHelper,
    Expression<System.Func<TModel, TProperty>> expression,
    IEnumerable<TDBObject> dbObjectList,
    string dataTextField)
    where TDBObject : BaseDBObject
  {
    return htmlHelper.DropDownListFor<TModel, TProperty, TDBObject>(expression, dbObjectList, dataTextField, "", "");
  }

  public static MvcHtmlString DropDownListFor<TModel, TProperty, TDBObject>(
    this HtmlHelper<TModel> htmlHelper,
    Expression<System.Func<TModel, TProperty>> expression,
    IEnumerable<TDBObject> dbObjectList,
    string dataTextField,
    string defaultOptionValue)
    where TDBObject : BaseDBObject
  {
    return htmlHelper.DropDownListFor<TModel, TProperty, TDBObject>(expression, dbObjectList, dataTextField, defaultOptionValue, "");
  }

  public static MvcHtmlString DropDownListFor<TModel, TProperty, TDBObject>(
    this HtmlHelper<TModel> htmlHelper,
    Expression<System.Func<TModel, TProperty>> expression,
    IEnumerable<TDBObject> dbObjectList,
    string dataTextField,
    string defaultOptionValue,
    string htmlClassName)
    where TDBObject : BaseDBObject
  {
    string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression) expression);
    long selectedValue = (object) htmlHelper.ViewData.Model == null ? long.MinValue : ((object) expression.Compile()(htmlHelper.ViewData.Model)).ToLong();
    return htmlHelper.DropDownList<TDBObject>(expressionText, selectedValue, dbObjectList, dataTextField, defaultOptionValue, htmlClassName);
  }

  public static MvcHtmlString DropDownList<TDBObject>(
    this HtmlHelper hlp,
    string name,
    long selectedValue,
    IEnumerable<TDBObject> List,
    string dataTextField,
    string defaultOptionValue,
    string htmlClassName)
    where TDBObject : BaseDBObject
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"<select id=\"{name}\" name=\"{name}\"");
    if (!string.IsNullOrEmpty(htmlClassName))
      stringBuilder.Append($" class=\"{htmlClassName}\"");
    stringBuilder.AppendLine(">");
    PropertyInfo property = typeof (TDBObject).GetProperty(dataTextField);
    if (!string.IsNullOrEmpty(defaultOptionValue))
      stringBuilder.AppendFormat("<option value='{0}' >{1}</option>\n", (object) long.MinValue, (object) defaultOptionValue);
    foreach (TDBObject dbObject in List)
    {
      BaseDBObject baseDbObject = (BaseDBObject) dbObject;
      stringBuilder.AppendFormat("<option value='{0}' ", (object) baseDbObject.PrimaryKeyValue);
      if (selectedValue == baseDbObject.PrimaryKeyValue)
        stringBuilder.Append("selected='selected' ");
      if (property != (PropertyInfo) null)
        stringBuilder.AppendFormat(">{0}</option>\n", (object) property.GetValue((object) baseDbObject).ToStringSafe().Replace("_", " "));
      else
        stringBuilder.AppendFormat(">{0}</option>\n", (object) baseDbObject.PrimaryKeyValue);
    }
    stringBuilder.AppendLine("</select>");
    return MvcHtmlString.Create(stringBuilder.ToString());
  }

  public static MvcHtmlString DropDownList<T>(
    this HtmlHelper hlp,
    string name,
    IEnumerable<T> List,
    string dataTextField,
    string dataValueField,
    string selectedValue,
    string htmlClassName)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"<select id=\"{name}\" name=\"{name}\"");
    if (!string.IsNullOrEmpty(htmlClassName))
      stringBuilder.Append($" class=\"{htmlClassName}\"");
    stringBuilder.AppendLine(">");
    PropertyInfo property1 = typeof (T).GetProperty(dataTextField);
    if (property1 == (PropertyInfo) null)
      throw new ArgumentOutOfRangeException(nameof (dataTextField));
    PropertyInfo property2 = typeof (T).GetProperty(dataValueField);
    if (property2 == (PropertyInfo) null)
      throw new ArgumentOutOfRangeException(nameof (dataValueField));
    foreach (T obj in List)
    {
      string stringSafe = property2.GetValue((object) obj).ToStringSafe();
      stringBuilder.AppendFormat("<option value='{0}' ", (object) stringSafe);
      if (selectedValue == stringSafe)
        stringBuilder.Append("selected='selected' ");
      stringBuilder.AppendFormat(">{0}</option>\n", (object) property1.GetValue((object) obj).ToStringSafe().Replace("_", " "));
    }
    stringBuilder.AppendLine("</select>");
    return MvcHtmlString.Create(stringBuilder.ToString());
  }

  public static MvcHtmlString ActionLinkWithReturnURL(
    this HtmlHelper hlp,
    string linkName,
    string actionName,
    string controllerName)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(actionName);
    stringBuilder.AppendFormat("{0}ReturnUrl=", actionName.Contains<char>('?') ? (object) "&" : (object) "?");
    stringBuilder.Append(HttpContext.Current.Request.Path);
    return LinkExtensions.ActionLink(hlp, linkName, stringBuilder.ToString(), controllerName);
  }

  public static byte[] ToByteArray(this Image img, ImageFormat format)
  {
    byte[] array;
    try
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        img.Save((Stream) memoryStream, format);
        array = memoryStream.ToArray();
      }
    }
    catch (Exception ex)
    {
      throw;
    }
    return array;
  }

  public static Image ResizeImage(this Image image, int width, int height)
  {
    Bitmap bitmap = new Bitmap(width, height);
    using (Graphics graphics = Graphics.FromImage((Image) bitmap))
    {
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
    }
    return (Image) bitmap;
  }

  public static string GetThemedContent(this HtmlHelper html, string path)
  {
    return ExtensionMethods.GetThemedContent(path);
  }

  public static string GetThemedContent(string path)
  {
    HttpContext current = HttpContext.Current;
    if (current == null)
      throw new InvalidOperationException("Http Context cannot be null.");
    if (!path.StartsWith("\\"))
      path = path.TrimStart('\\');
    if (!path.StartsWith("/"))
      path = "/" + path;
    string path1 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "/Themes/{0}/Content{1}", (object) MonnitViewEngine.GetCurrentThemeName(current), (object) path);
    return !System.IO.File.Exists(current.Server.MapPath(path1)) ? "/Content" + path : path1;
  }

  public static string GetThemedScript(this HtmlHelper html, string path)
  {
    return ExtensionMethods.GetThemedScript(path);
  }

  public static string GetThemedScript(string path)
  {
    HttpContext current = HttpContext.Current;
    if (current == null)
      throw new InvalidOperationException("Http Context cannot be null.");
    if (!path.StartsWith("\\"))
      path = path.TrimStart('\\');
    if (!path.StartsWith("/"))
      path = "/" + path;
    string path1 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "/Themes/{0}/Scripts{1}", (object) MonnitViewEngine.GetCurrentThemeName(current), (object) path);
    return !System.IO.File.Exists(current.Server.MapPath(path1)) ? "/Scripts" + path : path1;
  }

  public static MvcHtmlString GetThemedSVG(this HtmlHelper html, string imageKey)
  {
    if (HttpContext.Current == null)
      throw new InvalidOperationException("Http Context cannot be null.");
    SVGIcon svgIcon = SVGIcon.Load(imageKey, MonnitSession.CurrentTheme.AccountThemeID);
    return svgIcon != null ? MvcHtmlString.Create(svgIcon.HTMLCode) : MvcHtmlString.Create("");
  }

  public static MvcHtmlString GetThemedSVGForGateway(this HtmlHelper html, long gatewayTypeID)
  {
    if (HttpContext.Current == null)
      throw new InvalidOperationException("Http Context cannot be null.");
    string imageKey = "";
    long num = gatewayTypeID - 1L;
    if ((ulong) num <= 36UL)
    {
      switch ((uint) num)
      {
        case 0:
        case 1:
        case 27:
        case 28:
          imageKey = "usb-c";
          break;
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 30:
        case 32 /*0x20*/:
        case 34:
          imageKey = "ether-icon";
          break;
        case 7:
        case 8:
        case 12:
        case 13:
        case 18:
        case 19:
        case 20:
          imageKey = "tower-cell";
          break;
        case 9:
        case 10:
          imageKey = "network";
          break;
        case 16 /*0x10*/:
        case 17:
        case 21:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
        case 29:
        case 31 /*0x1F*/:
        case 35:
          imageKey = "tower-cell";
          break;
      }
    }
    SVGIcon svgIcon = SVGIcon.Load(imageKey, MonnitSession.CurrentTheme.AccountThemeID);
    return svgIcon != null ? MvcHtmlString.Create(svgIcon.HTMLCode) : MvcHtmlString.Create("");
  }

  public static bool IsIPAddress(this string address)
  {
    bool flag = false;
    string[] strArray = address.Split('.');
    if (strArray.Length == 4)
    {
      flag = true;
      foreach (object o in strArray)
      {
        int num = o.ToInt();
        if (num == int.MinValue || num < 0 || num > (int) byte.MaxValue)
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  public static bool IsMobile(this HttpRequestBase Request)
  {
    string serverVariable = Request.ServerVariables["HTTP_USER_AGENT"];
    if (string.IsNullOrEmpty(serverVariable))
      return false;
    Regex regex1 = new Regex("(android|bb\\d+|meego).+mobile|avantgo|bada\\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    Regex regex2 = new Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    return regex1.IsMatch(serverVariable) || regex2.IsMatch(serverVariable.Substring(0, 4));
  }

  public static bool IsSensorCertMobile(this HttpRequestBase Request)
  {
    string userAgent = Request.UserAgent;
    return !string.IsNullOrEmpty(userAgent) && userAgent.ToUpper().Contains("SENSORCERTMOBILE");
  }

  public static bool IsSensorCertMobile(this HttpRequest Request)
  {
    string userAgent = Request.UserAgent;
    return !string.IsNullOrEmpty(userAgent) && userAgent.ToUpper().Contains("SENSORCERTMOBILE");
  }

  public static IEnumerable<DateTime> EnumerateDays(this DateTime fromDate, DateTime toDate)
  {
    for (DateTime day = fromDate.Date; day.Date <= toDate.Date; day = day.AddDays(1.0))
      yield return day;
  }

  public static long ToLocalTimeTicks(this DateTime date)
  {
    return Monnit.TimeZone.GetLocalTimeById(date, MonnitSession.CurrentCustomer.Account.TimeZoneID).Ticks;
  }

  public static long ToLocalEpochTime(this DateTime date)
  {
    return Convert.ToInt64((Monnit.TimeZone.GetLocalTimeById(date, MonnitSession.CurrentCustomer.Account.TimeZoneID) - ExtensionMethods._EpochStartTime).TotalMilliseconds);
  }

  public static long ToEpochTime(this DateTime date)
  {
    return Convert.ToInt64((date - ExtensionMethods._EpochStartTime).TotalMilliseconds);
  }

  public static long ToElapsedMinutes(this DateTime date)
  {
    return Convert.ToInt64((DateTime.UtcNow - date).TotalMinutes);
  }

  public static long ToElapsedSeconds(this DateTime date)
  {
    return Convert.ToInt64((DateTime.UtcNow - date).TotalSeconds);
  }

  public static DateTime ToDateTimeFromEpoch(this long date)
  {
    return ExtensionMethods._EpochStartTime.AddMilliseconds((double) date);
  }

  public static string ToLocalTimeShort(this DateTime date)
  {
    return Monnit.TimeZone.GetLocalTimeById(date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString();
  }

  public static string ToLocalTimeShort(this DateTime date, long timeZoneID)
  {
    return Monnit.TimeZone.GetLocalTimeById(date, timeZoneID).ToShortTimeString();
  }

  public static string ToLocalDateShort(this DateTime date)
  {
    return Monnit.TimeZone.GetLocalTimeById(date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString();
  }

  public static string ToLocalDateShort(this DateTime date, long timeZoneID)
  {
    return Monnit.TimeZone.GetLocalTimeById(date, timeZoneID).ToShortDateString();
  }

  public static string ToLocalDateTimeShort(this DateTime date)
  {
    return $"{date.ToLocalDateShort()} {date.ToLocalTimeShort()}";
  }

  public static string OVToFormatDecimal(this Decimal number, string decimalFormat)
  {
    string formatDecimal = number.ToString("N", (IFormatProvider) new CultureInfo("en-US"));
    if (!string.IsNullOrEmpty(decimalFormat))
      formatDecimal = number.ToString("N", (IFormatProvider) new CultureInfo(decimalFormat));
    return formatDecimal;
  }

  public static string OVToLocalDateShort(this DateTime date)
  {
    return date.OVToLocalDateShort(MonnitSession.CurrentCustomer.Account.TimeZoneID);
  }

  public static string OVToLocalDateShort(this DateTime date, long timeZoneID)
  {
    string dateFormat = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer.Preferences["Date Format"] : "MM/dd/yyyy";
    return date.ToLocalDateFormatted(timeZoneID, dateFormat);
  }

  public static string OVToLocalTimeShort(this DateTime date)
  {
    return date.OVToLocalTimeShort(MonnitSession.CurrentCustomer.Account.TimeZoneID);
  }

  public static string OVToLocalTimeShort(this DateTime date, long timeZoneID)
  {
    string timeFormat = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer.Preferences["Time Format"] : "h:mm tt";
    return date.ToLocalTimeFormatted(timeZoneID, timeFormat);
  }

  public static string OVToLocalDateTimeShort(this DateTime date)
  {
    return $"{date.OVToLocalDateShort()} {date.OVToLocalTimeShort()}";
  }

  public static string OVToLocalDateTimeShort(this DateTime date, long timeZoneID)
  {
    return $"{date.OVToLocalDateShort(timeZoneID)} {date.OVToLocalTimeShort(timeZoneID)}";
  }

  public static string OVToDateShort(this DateTime date)
  {
    return date.ToDateFormatted(MonnitSession.CurrentCustomer.Preferences["Date Format"]);
  }

  public static string OVToTimeShort(this DateTime date)
  {
    return date.ToTimeFormatted(MonnitSession.CurrentCustomer.Preferences["Time Format"]);
  }

  public static string OVToDateTimeShort(this DateTime date)
  {
    return $"{date.OVToDateShort()} {date.OVToTimeShort()}";
  }

  public static string OVToMinutesElapsedLastMessageString(this DateTime dt)
  {
    if (dt == new DateTime(2099, 1, 1))
      return ((HtmlHelper) null).TranslateTag("Unavailable");
    string str1 = ((HtmlHelper) null).TranslateTag("Last Message: ");
    long elapsedMinutes = dt.ToElapsedMinutes();
    string str2 = elapsedMinutes > 1L ? ((HtmlHelper) null).TranslateTag("Minutes Ago") : ((HtmlHelper) null).TranslateTag("Minute Ago");
    return elapsedMinutes > 120L ? str1 + dt.OVToLocalDateTimeShort() : $"{str1}{elapsedMinutes.ToString()} {str2}";
  }

  public static string OVTestingElapsedLastMessageString(this DateTime dt)
  {
    if (dt == new DateTime(2099, 1, 1))
      return ((HtmlHelper) null).TranslateTag("Unavailable");
    long elapsedSeconds = dt.ToElapsedSeconds();
    if (elapsedSeconds > 600L)
      return "> 10 minutes ago";
    return elapsedSeconds > 120L ? (elapsedSeconds / 60L).ToString() + " minutes ago" : elapsedSeconds.ToString() + " seconds ago";
  }

  public static string Translate(this Control page, string field, string defaultText)
  {
    return string.Format("<a href='/Translate/Edit?page={1}&field={2}' target='_blank'><img src='/content/images/edit2.png'/></a>{0}", (object) defaultText, (object) page.GetType().Name, (object) field);
  }

  public static string TranslateRaw(this Control page, string field, string defaultText)
  {
    return defaultText;
  }

  public static string TranslateLink(this Control page, string field)
  {
    return $"<a href='/Translate/Edit?page={page.GetType().Name}&field={field}' target='_blank'><img src='/content/images/edit2.png'/></a>";
  }

  public static string TranslateTag(
    this HtmlHelper html,
    string tag,
    string defaultText,
    string language)
  {
    long num = 1;
    if (MonnitSession.CurrentCustomer != null)
      num = (long) MonnitSession.CurrentCustomer.Preferences["Language"].ToInt();
    if (!string.IsNullOrEmpty(language))
    {
      Language language1 = Language.LoadByName(language);
      if (language1 != null)
        num = language1.LanguageID;
    }
    string key = "Lang_" + num.ToString();
    Language language2 = TimedCache.RetrieveObject<Language>(key);
    if (language2 == null)
    {
      language2 = Language.Load(num);
      TimedCache.AddObjectToCach(key, (object) language2, new TimeSpan(0, 15, 0));
    }
    return language2.IsActive || MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")) ? TranslationTag.getTranslationOrDefault(num, tag, defaultText) : defaultText;
  }

  public static string TranslateTag(this HtmlHelper html, string tag)
  {
    string defaultText = tag;
    if (tag.Contains("|"))
    {
      string[] strArray = tag.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
      try
      {
        defaultText = strArray[1].ToString();
      }
      catch
      {
      }
    }
    return html.TranslateTag(tag, defaultText, "");
  }

  public static string TranslateTag(this HtmlHelper html, string tag, string defaultText)
  {
    return html.TranslateTag(tag, defaultText, "");
  }

  public static bool HasScriptTag(this string textToTest)
  {
    if (textToTest == null)
      return false;
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(textToTest);
    return htmlDocument.DocumentNode.SelectNodes("//script") != null;
  }

  public static string SanitizeHTMLEvent(this string textToTest)
  {
    if (string.IsNullOrWhiteSpace(textToTest))
      return "";
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(textToTest);
    ExtensionMethods.SanitizeHtmlNode(htmlDocument.DocumentNode);
    return htmlDocument.DocumentNode.InnerText;
  }

  public static string SanitizeButAllowSomeHTMLEvent(this string textToTest)
  {
    if (string.IsNullOrWhiteSpace(textToTest))
      return "";
    HtmlDocument htmlDocument = new HtmlDocument();
    string html = Notification.ReplaceAngleBracketsWithEncodedText(textToTest);
    htmlDocument.LoadHtml(html);
    ExtensionMethods.SanitizeHtmlNode(htmlDocument.DocumentNode);
    return Notification.ReplaceEncodedTextWithAngleBrackets(htmlDocument.DocumentNode.InnerHtml);
  }

  public static bool ValidateIP(string ipAddress) => IPAddress.TryParse(ipAddress, out IPAddress _);

  private static void SanitizeHtmlNode(HtmlNode node)
  {
    if (node.NodeType == HtmlNodeType.Element)
    {
      if (ExtensionMethods.BlackList.Contains(node.Name))
      {
        node.Remove();
        return;
      }
      if (node.Name == "style" && string.IsNullOrEmpty(node.InnerText) && node.InnerHtml.Contains("javascript:"))
        node.ParentNode.RemoveChild(node);
      if (node.HasAttributes)
      {
        for (int index = node.Attributes.Count - 1; index >= 0; --index)
        {
          HtmlAttribute attribute = node.Attributes[index];
          string lower1 = attribute.Name.ToLower();
          string lower2 = attribute.Value.ToLower();
          if (lower1.StartsWith("on"))
            node.Attributes.Remove(attribute);
          else if (lower2 != null && lower2.Contains("javascript:"))
            node.Attributes.Remove(attribute);
          else if (lower1 == "style" && lower2 != null && lower2.Contains("javascript:") || lower2.Contains("vbscript:"))
            node.Attributes.Remove(attribute);
        }
      }
    }
    if (!node.HasChildNodes)
      return;
    for (int index = node.ChildNodes.Count - 1; index >= 0; --index)
      ExtensionMethods.SanitizeHtmlNode(node.ChildNodes[index]);
  }

  public static IPBlacklist RetrieveCurrentIPBlacklist(this HttpRequestBase request)
  {
    List<IPBlacklist> ipBlacklistList = IPBlacklist.LoadByIP(request.ClientIP());
    IPBlacklist ipBlacklist1 = (IPBlacklist) null;
    foreach (IPBlacklist ipBlacklist2 in ipBlacklistList)
    {
      DateTime dateTime = ipBlacklist2.FirstFailedAttempt;
      dateTime = dateTime.AddMinutes((double) ConfigData.AppSettings("IPBlacklistTime", "5").ToInt());
      if (dateTime.Subtract(DateTime.UtcNow).TotalSeconds < 0.0)
        ipBlacklist2.Delete();
      else if (ipBlacklist1 == null || ipBlacklist1.FailedAttempts < ipBlacklist2.FailedAttempts)
        ipBlacklist1 = ipBlacklist2;
    }
    if (ipBlacklist1 == null)
    {
      ipBlacklist1 = new IPBlacklist();
      ipBlacklist1.FirstFailedAttempt = DateTime.UtcNow;
      ipBlacklist1.IPAddress = request.ClientIP();
    }
    return ipBlacklist1;
  }

  public static string ClientIP(this HttpRequestBase request)
  {
    return !string.IsNullOrEmpty(request.Headers["X-Forwarded-For"]) ? request.Headers["X-Forwarded-For"] : request.UserHostAddress;
  }

  public static uint Calc_CRC32(this byte[] msg)
  {
    return ExtensionMethods.Calc_CRC32_internal(uint.MaxValue, msg) ^ uint.MaxValue;
  }

  private static uint Calc_CRC32_internal(uint crc, byte[] msg)
  {
    for (int index1 = 0; index1 < msg.Length; ++index1)
    {
      crc ^= (uint) msg[index1];
      for (int index2 = 0; index2 < 8; ++index2)
      {
        if ((crc & 1U) > 0U)
          crc = crc >> 1 ^ 3988292384U;
        else
          crc >>= 1;
      }
    }
    return crc;
  }

  public static string GetResultString(this XmlResult xmlResult)
  {
    return (string) ((SensorRestAPI) xmlResult.ObjectToSerialize).Result;
  }

  public static string GetResultString(this JsonResult jsonResult)
  {
    return (string) ((SensorRestAPI) jsonResult.Data).Result;
  }

  public static string GetResultString(this ActionResult actionResult)
  {
    switch (actionResult)
    {
      case XmlResult xmlResult:
        return ExtensionMethods.GetResultString(xmlResult);
      case JsonResult jsonResult:
        return ExtensionMethods.GetResultString(jsonResult);
      default:
        throw new NotSupportedException("Unsupported Type");
    }
  }

  public static bool Failed(this ActionResult actionResult)
  {
    string lower = actionResult.GetResultString().ToLower();
    bool flag = false;
    if (lower.Contains("failed") || lower.Contains("invalid"))
      flag = true;
    return flag;
  }

  public static void InitializePropertyDefaultValues(this object obj)
  {
    foreach (PropertyInfo property in obj.GetType().GetProperties())
    {
      DefaultValueAttribute customAttribute = property.GetCustomAttribute<DefaultValueAttribute>();
      if (customAttribute != null)
        property.SetValue(obj, customAttribute.Value);
    }
  }

  public static string ToCSVString(this DataTable dtDataTable)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < dtDataTable.Columns.Count; ++index)
    {
      stringBuilder.Append((object) dtDataTable.Columns[index]);
      if (index < dtDataTable.Columns.Count - 1)
        stringBuilder.Append(",");
    }
    stringBuilder.AppendLine("");
    foreach (DataRow row in (InternalDataCollectionBase) dtDataTable.Rows)
    {
      for (int columnIndex = 0; columnIndex < dtDataTable.Columns.Count; ++columnIndex)
      {
        if (!Convert.IsDBNull(row[columnIndex]))
        {
          string source = row[columnIndex].ToString();
          if (source.Contains<char>(','))
          {
            string str = $"\"{source}\"";
            stringBuilder.Append(str);
          }
          else
            stringBuilder.Append(row[columnIndex].ToString());
        }
        if (columnIndex < dtDataTable.Columns.Count - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.AppendLine();
    }
    return stringBuilder.ToString();
  }

  public static string LabelPartialIfDebug(string label)
  {
    string str = string.Empty;
    if (Debugger.IsAttached)
    {
      Debug.Write("Debugger.IsAttached");
      str = "//# sourceURL=" + label;
    }
    return str;
  }

  public static string JSCheckDebugMode()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("console.log('TRUE \t=>\t DEBUG');");
    stringBuilder.AppendLine("console.log('FALSE \t=>\t RELEASE');");
    if (Debugger.IsAttached)
      stringBuilder.AppendLine("console.log('TRUE \t=>\t Debugger.IsAttached');");
    else
      stringBuilder.AppendLine("console.log('FALSE \t=>\t Debugger.IsAttached');");
    if (HttpContext.Current.IsDebuggingEnabled)
      stringBuilder.AppendLine("console.log('TRUE \t=>\t HttpContext.Current.IsDebuggingEnabled');");
    else
      stringBuilder.AppendLine("console.log('FALSE \t=>\t HttpContext.Current.IsDebuggingEnabled');");
    return stringBuilder.ToString();
  }

  public static string ParseDoubleToNearestToString(
    this object value,
    double nearestInterval,
    string returnIfFail,
    string formatResult = null)
  {
    double result;
    if (!double.TryParse(value.ToString(), out result))
      return returnIfFail;
    double num = Math.Round(result / nearestInterval) * nearestInterval;
    string toNearestToString = num.ToString();
    if (formatResult != null)
      toNearestToString = string.Format(formatResult, (object) num);
    return toNearestToString;
  }

  public static string ParseDoubleToString(
    this object value,
    string returnIfFail,
    string formatResult = null)
  {
    string s = value.ToString();
    double result;
    if (!double.TryParse(s, out result))
      return returnIfFail;
    if (formatResult != null)
      s = string.Format(formatResult, (object) result);
    return s;
  }

  public static string IsNullOrEmpty(this object value, string returnIfNullOrEmpty)
  {
    string str = value.ToString();
    return string.IsNullOrEmpty(str) ? returnIfNullOrEmpty : str;
  }

  public static string ParseDateTimeToString(
    this object value,
    string returnIfFail,
    string formatResult = null)
  {
    string s = value.ToString();
    DateTime result;
    if (!DateTime.TryParse(s, out result))
      return returnIfFail;
    if (formatResult != null)
      s = string.Format(formatResult, (object) result);
    return s;
  }

  public static string ParseDateTimeToLocalToString(
    this object value,
    string returnIfFail,
    long timeZoneID,
    string formatResult = null)
  {
    string s = value.ToString();
    DateTime result;
    if (!DateTime.TryParse(s, out result))
      return returnIfFail;
    Monnit.TimeZone.GetLocalTimeById(result, timeZoneID);
    if (formatResult != null)
      s = string.Format(formatResult, (object) result);
    return s;
  }

  public static byte[] ToByteArray(this string hex)
  {
    hex = hex.ToUpper();
    return Enumerable.Range(0, hex.Length).Where<int>((System.Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((System.Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16 /*0x10*/))).ToArray<byte>();
  }

  public static string ToString(this byte[] barr, int? radix = 10)
  {
    int totalWidth = 0;
    int? nullable = radix;
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case 2:
          totalWidth = 8;
          break;
        case 8:
          totalWidth = 3;
          break;
        case 16 /*0x10*/:
          totalWidth = 2;
          break;
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (byte num in barr)
      stringBuilder.Append(Convert.ToString(num, radix.Value).PadLeft(totalWidth, '0') + "-");
    return stringBuilder.ToString().TrimEnd('-').ToUpper();
  }

  public static string ByteToString(this byte b, int? radix = 10)
  {
    int totalWidth = 0;
    int? nullable = radix;
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case 2:
          totalWidth = 8;
          break;
        case 8:
          totalWidth = 3;
          break;
        case 16 /*0x10*/:
          totalWidth = 2;
          break;
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Convert.ToString(b, radix.Value).PadLeft(totalWidth, '0') + "-");
    return stringBuilder.ToString().TrimEnd('-').ToUpper();
  }
}
