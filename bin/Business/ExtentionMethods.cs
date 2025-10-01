// Decompiled with JetBrains decompiler
// Type: Monnit.ExtentionMethods
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

#nullable disable
namespace Monnit;

public static class ExtentionMethods
{
  public static string[] HexTbl = Enumerable.Range(0, 256 /*0x0100*/).Select<int, string>((Func<int, string>) (v => v.ToString("X2"))).ToArray<string>();

  public static (double Slope, double Intercept) LinearInterpolation(
    double from1,
    double from2,
    double to1,
    double to2)
  {
    double num1 = (to2 - to1) / (from2 - from1);
    double num2 = to1 - from1 * num1;
    return (num1, num2);
  }

  public static double ApplySlopeAndIntercept(double slope, double intercept, double x)
  {
    return intercept + slope * x;
  }

  public static double LinearInterpolation(
    this double x,
    double lowX,
    double lowY,
    double highX,
    double highY)
  {
    double num1 = (highY - lowY) / (highX - lowX);
    double num2 = num1 * lowX - lowY;
    return num1 * x - num2;
  }

  public static double LinearInterpolation(
    this double x,
    double lowX,
    double lowY,
    double highX,
    double highY,
    int maxDecimal)
  {
    return Math.Round(x.LinearInterpolation(lowX, lowY, highX, highY), maxDecimal);
  }

  public static double LinearReversion(
    this double y,
    double lowX,
    double lowY,
    double highX,
    double highY)
  {
    double num1 = (highY - lowY) / (highX - lowX);
    double num2 = num1 * lowX - lowY;
    return (y - num2) / num1;
  }

  public static void SafeAdd(this MailAddressCollection addresses, string emailAddress)
  {
    if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(emailAddress))
      return;
    addresses.Add(new MailAddress(emailAddress));
  }

  public static void SafeAdd(
    this MailAddressCollection addresses,
    string emailAddress,
    string name)
  {
    if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(emailAddress))
      return;
    addresses.Add(new MailAddress(emailAddress, name));
  }

  public static string ToHex(this byte[] array)
  {
    StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
    foreach (byte index in array)
      stringBuilder.Append(ExtentionMethods.HexTbl[(int) index]);
    return stringBuilder.ToString();
  }

  public static string ToHex(this byte val) => ExtentionMethods.HexTbl[(int) val];

  public static IEnumerable<DateTime> EnumerateDays(this DateTime fromDate, DateTime toDate)
  {
    for (DateTime day = fromDate.Date; day.Date <= toDate.Date; day = day.AddDays(1.0))
      yield return day;
  }

  public static string ToLocalTimeFormatted(this DateTime date, long timeZoneID, string timeFormat)
  {
    DateTime localTimeById = TimeZone.GetLocalTimeById(date, timeZoneID);
    return !string.IsNullOrEmpty(timeFormat) ? localTimeById.ToString(timeFormat) : localTimeById.ToShortTimeString();
  }

  public static string ToLocalDateFormatted(this DateTime date, long timeZoneID, string dateFormat)
  {
    DateTime localTimeById = TimeZone.GetLocalTimeById(date, timeZoneID);
    return !string.IsNullOrEmpty(dateFormat) ? localTimeById.ToString(dateFormat) : localTimeById.ToShortDateString();
  }

  public static string ToLocalDateTimeFormatted(
    this DateTime date,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    return $"{date.ToLocalDateFormatted(timeZoneID, dateFormat)} {date.ToLocalTimeFormatted(timeZoneID, timeFormat)}";
  }

  public static string ToDateFormatted(this DateTime date, string dateFormat)
  {
    return !string.IsNullOrEmpty(dateFormat) ? date.ToString(dateFormat) : date.ToShortDateString();
  }

  public static string ToTimeFormatted(this DateTime date, string timeFormat)
  {
    return !string.IsNullOrEmpty(timeFormat) ? date.ToString(timeFormat) : date.ToShortTimeString();
  }

  public static string ToDateTimeFormatted(
    this DateTime date,
    string dateFormat,
    string timeFormat)
  {
    return $"{date.ToDateFormatted(dateFormat)} {date.ToTimeFormatted(timeFormat)}";
  }

  public static string To7Digits(this double d)
  {
    double num = 1.0;
    if (d < 0.0)
    {
      d = Math.Abs(d);
      num = -1.0;
    }
    return d >= 10.0 ? (d >= 100.0 ? (d >= 1000.0 ? (d >= 10000.0 ? (d >= 100000.0 ? (d >= 1000000.0 ? (d >= 10000000.0 ? (d * num).ToString() : (d * num).ToString("0000000")) : (d * num).ToString("000000.0")) : (d * num).ToString("00000.00")) : (d * num).ToString("0000.000")) : (d * num).ToString("000.0000")) : (d * num).ToString("00.00000")) : (d * num).ToString("0.000000");
  }

  public static string RemoveNonNumeric(this string number)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (number == null || !(number != string.Empty))
      return string.Empty;
    foreach (char c in number.ToCharArray())
    {
      if (char.IsNumber(c))
        stringBuilder.Append(c);
    }
    return stringBuilder.ToString();
  }

  public static string JsonStringify(this BaseDBObject obj)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append('{');
    foreach (PropertyInfo property in obj.GetType().GetProperties())
    {
      DBPropAttribute dbPropAttribute = (DBPropAttribute) null;
      object[] customAttributes = property.GetCustomAttributes(typeof (DBPropAttribute), true);
      if (customAttributes != null && customAttributes.Length != 0)
        dbPropAttribute = customAttributes[0] as DBPropAttribute;
      if (dbPropAttribute != null)
        stringBuilder.AppendFormat("\"{0}\" : \"{1}\", ", (object) dbPropAttribute.ColumnName, property.GetValue((object) obj, (object[]) null));
    }
    stringBuilder.Remove(stringBuilder.Length - 2, 2);
    stringBuilder.Append('}');
    return stringBuilder.ToString();
  }

  public static void TryAddCookie(this WebRequest webRequest, Cookie cookie)
  {
    if (!(webRequest is HttpWebRequest httpWebRequest))
      return;
    if (httpWebRequest.CookieContainer == null)
      httpWebRequest.CookieContainer = new CookieContainer();
    httpWebRequest.CookieContainer.Add(cookie);
  }

  public static void LogAuditData(
    this BaseDBObject DBObject,
    eAuditAction action,
    eAuditObject Auditobj,
    long customerID,
    string changeRecord,
    long accountID,
    string descriptionOfAction)
  {
    AuditLog.LogAuditData(customerID, DBObject.PrimaryKeyValue, action, Auditobj, changeRecord, accountID, descriptionOfAction);
  }

  public static void LogAuditData(
    this BaseDBObject DBObject,
    eAuditAction action,
    eAuditObject Auditobj,
    long customerID,
    long accountID,
    string descriptionOfAction)
  {
    DBObject.LogAuditData(action, Auditobj, customerID, DBObject.JsonStringify(), accountID, descriptionOfAction);
  }

  public static ExceptionLog Log(this Exception ex, string function)
  {
    ExceptionLog exceptionLog = new ExceptionLog(ex);
    exceptionLog.Message = $"[{Environment.MachineName}] (Exception Type: {ex.GetType().ToString()}) {function} : {exceptionLog.Message}";
    exceptionLog.Save();
    return exceptionLog;
  }

  public static string FormatJson(this string json)
  {
    string INDENT_STRING = "&nbsp;&nbsp;&nbsp;";
    int indentation = 0;
    int quoteCount = 0;
    return string.Concat(json.Select(ch => new
    {
      ch = ch,
      quotes = ch == '"' ? quoteCount++ : quoteCount
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      lineBreak = _param1.ch != ',' || _param1.quotes % 2 != 0 ? (string) null : $"{_param1.ch.ToString()}</br>{string.Concat(Enumerable.Repeat<string>(INDENT_STRING, indentation))}"
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier1 = _param1,
      openChar = _param1.\u003C\u003Eh__TransparentIdentifier0.ch == '{' || _param1.\u003C\u003Eh__TransparentIdentifier0.ch == '[' ? $"{_param1.\u003C\u003Eh__TransparentIdentifier0.ch.ToString()}</br>{string.Concat(Enumerable.Repeat<string>(INDENT_STRING, ++indentation))}" : _param1.\u003C\u003Eh__TransparentIdentifier0.ch.ToString()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier2 = _param1,
      closeChar = _param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.ch == '}' || _param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.ch == ']' ? $"</br></br>{string.Concat(Enumerable.Repeat<string>(INDENT_STRING, --indentation))}{_param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.ch.ToString()}" : _param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.ch.ToString()
    }).Select(_param1 =>
    {
      if (_param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.lineBreak != null)
        return _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.lineBreak;
      return _param1.\u003C\u003Eh__TransparentIdentifier2.openChar.Length <= 1 ? _param1.closeChar : _param1.\u003C\u003Eh__TransparentIdentifier2.openChar;
    }));
  }

  public static string EscapeJson(this string dirtyString)
  {
    string oldValue1 = "\"";
    dirtyString = dirtyString.Replace(oldValue1, "").Trim();
    string oldValue2 = "\\";
    dirtyString = dirtyString.Replace(oldValue2, "\\\\").Trim();
    string oldValue3 = "/";
    dirtyString = dirtyString.Replace(oldValue3, "\\/").Trim();
    string oldValue4 = "\\b";
    dirtyString = dirtyString.Replace(oldValue4, "").Trim();
    string oldValue5 = "\\f";
    dirtyString = dirtyString.Replace(oldValue5, "").Trim();
    string oldValue6 = "\\n";
    dirtyString = dirtyString.Replace(oldValue6, "").Trim();
    string oldValue7 = "\\r";
    dirtyString = dirtyString.Replace(oldValue7, "").Trim();
    string oldValue8 = "\\t";
    dirtyString = dirtyString.Replace(oldValue8, "").Trim();
    return dirtyString;
  }
}
