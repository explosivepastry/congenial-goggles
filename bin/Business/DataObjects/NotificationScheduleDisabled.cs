// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationScheduleDisabled
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("NotificationScheduleDisabled")]
public class NotificationScheduleDisabled : BaseDBObject
{
  private long _NotificationScheduleDisabledID = long.MinValue;
  private int _StartMonth = int.MinValue;
  private int _StartDay = int.MinValue;
  private int _EndMonth = int.MinValue;
  private int _EndDay = int.MinValue;
  private long _NotificationID = long.MinValue;

  [DBProp("NotificationScheduleDisabledID", IsPrimaryKey = true)]
  public long NotificationScheduleDisabledID
  {
    get => this._NotificationScheduleDisabledID;
    set => this._NotificationScheduleDisabledID = value;
  }

  [DBProp("StartMonth")]
  public int StartMonth
  {
    get => this._StartMonth;
    set => this._StartMonth = value;
  }

  [DBProp("StartDay")]
  public int StartDay
  {
    get => this._StartDay;
    set => this._StartDay = value;
  }

  [DBProp("EndMonth")]
  public int EndMonth
  {
    get => this._EndMonth;
    set => this._EndMonth = value;
  }

  [DBProp("EndDay")]
  public int EndDay
  {
    get => this._EndDay;
    set => this._EndDay = value;
  }

  [DBProp("NotificationID")]
  [DBForeignKey("Notification", "NotificationID")]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  public static void AddNotificationScheduleDisabled(
    long notificationID,
    List<int> monthList,
    int month)
  {
    int index1 = 0;
    bool flag1 = true;
    bool flag2 = false;
    NotificationScheduleDisabled scheduleDisabled = new NotificationScheduleDisabled();
    for (int index2 = 0; index2 < monthList.Count; ++index2)
    {
      if (monthList[index2] != -1)
        index1 = index2;
      else
        flag2 = true;
      if (flag1)
      {
        scheduleDisabled.NotificationID = notificationID;
        scheduleDisabled.StartMonth = month;
        scheduleDisabled.StartDay = monthList[index2];
        flag1 = false;
      }
      if (flag2)
      {
        scheduleDisabled.EndMonth = month;
        scheduleDisabled.EndDay = monthList[index1];
        scheduleDisabled.Save();
        scheduleDisabled = new NotificationScheduleDisabled();
        flag1 = true;
        flag2 = false;
      }
    }
  }

  public static List<int> DayList(string[] days)
  {
    List<int> source = new List<int>();
    for (int index = 0; index < days.Length; ++index)
    {
      if (source.Count > 0 && days[index].ToInt() - 1 != source.Last<int>())
        source.Add(-1);
      source.Add(days[index].ToInt());
      if (days.Length - 1 == index)
        source.Add(-1);
    }
    return source;
  }

  public static Dictionary<int, Dictionary<int, bool>> fillMonthDayDic(
    List<NotificationScheduleDisabled> notidisablelist)
  {
    Dictionary<int, Dictionary<int, bool>> dictionary = new Dictionary<int, Dictionary<int, bool>>();
    foreach (NotificationScheduleDisabled scheduleDisabled in notidisablelist)
    {
      if (scheduleDisabled.StartMonth == 1)
      {
        if (!dictionary.ContainsKey(1))
          dictionary.Add(1, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[1].ContainsKey(2))
          dictionary[1].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][2])
          dictionary[1][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(3))
          dictionary[1].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][3])
          dictionary[1][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(4))
          dictionary[1].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][4])
          dictionary[1][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(5))
          dictionary[1].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][5])
          dictionary[1][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(6))
          dictionary[1].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][6])
          dictionary[1][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(7))
          dictionary[1].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][7])
          dictionary[1][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(8))
          dictionary[1].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][8])
          dictionary[1][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(9))
          dictionary[1].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][9])
          dictionary[1][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(10))
          dictionary[1].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][10])
          dictionary[1][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(11))
          dictionary[1].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][11])
          dictionary[1][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(12))
          dictionary[1].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][12])
          dictionary[1][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(13))
          dictionary[1].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][13])
          dictionary[1][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(14))
          dictionary[1].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][14])
          dictionary[1][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(15))
          dictionary[1].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][15])
          dictionary[1][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(16 /*0x10*/))
          dictionary[1].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[1][16 /*0x10*/])
          dictionary[1][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(17))
          dictionary[1].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][17])
          dictionary[1][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(18))
          dictionary[1].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][18])
          dictionary[1][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(19))
          dictionary[1].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][19])
          dictionary[1][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(20))
          dictionary[1].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][20])
          dictionary[1][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(21))
          dictionary[1].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][21])
          dictionary[1][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(22))
          dictionary[1].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][22])
          dictionary[1][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(23))
          dictionary[1].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][23])
          dictionary[1][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(24))
          dictionary[1].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][24])
          dictionary[1][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(25))
          dictionary[1].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][25])
          dictionary[1][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(26))
          dictionary[1].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][26])
          dictionary[1][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(27))
          dictionary[1].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][27])
          dictionary[1][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(28))
          dictionary[1].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][28])
          dictionary[1][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(29))
          dictionary[1].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][29])
          dictionary[1][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(30))
          dictionary[1].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[1][30])
          dictionary[1][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[1].ContainsKey(31 /*0x1F*/))
          dictionary[1].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[1][31 /*0x1F*/])
          dictionary[1][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 2)
      {
        if (!dictionary.ContainsKey(2))
          dictionary.Add(2, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[2].ContainsKey(2))
          dictionary[2].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][2])
          dictionary[2][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(3))
          dictionary[2].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][3])
          dictionary[2][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(4))
          dictionary[2].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][4])
          dictionary[2][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(5))
          dictionary[2].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][5])
          dictionary[2][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(6))
          dictionary[2].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][6])
          dictionary[2][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(7))
          dictionary[2].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][7])
          dictionary[2][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(8))
          dictionary[2].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][8])
          dictionary[2][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(9))
          dictionary[2].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][9])
          dictionary[2][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(10))
          dictionary[2].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][10])
          dictionary[2][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(11))
          dictionary[2].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][11])
          dictionary[2][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(12))
          dictionary[2].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][12])
          dictionary[2][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(13))
          dictionary[2].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][13])
          dictionary[2][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(14))
          dictionary[2].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][14])
          dictionary[2][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(15))
          dictionary[2].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][15])
          dictionary[2][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(16 /*0x10*/))
          dictionary[2].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[2][16 /*0x10*/])
          dictionary[2][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(17))
          dictionary[2].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][17])
          dictionary[2][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(18))
          dictionary[2].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][18])
          dictionary[2][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(19))
          dictionary[2].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][19])
          dictionary[2][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(20))
          dictionary[2].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][20])
          dictionary[2][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(21))
          dictionary[2].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][21])
          dictionary[2][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(22))
          dictionary[2].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][22])
          dictionary[2][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(23))
          dictionary[2].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][23])
          dictionary[2][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(24))
          dictionary[2].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][24])
          dictionary[2][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(25))
          dictionary[2].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][25])
          dictionary[2][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(26))
          dictionary[2].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][26])
          dictionary[2][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(27))
          dictionary[2].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][27])
          dictionary[2][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(28))
          dictionary[2].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][28])
          dictionary[2][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[2].ContainsKey(29))
          dictionary[2].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[2][29])
          dictionary[2][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 3)
      {
        if (!dictionary.ContainsKey(3))
          dictionary.Add(3, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[3].ContainsKey(2))
          dictionary[3].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][2])
          dictionary[3][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(3))
          dictionary[3].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][3])
          dictionary[3][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(4))
          dictionary[3].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][4])
          dictionary[3][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(5))
          dictionary[3].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][5])
          dictionary[3][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(6))
          dictionary[3].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][6])
          dictionary[3][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(7))
          dictionary[3].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][7])
          dictionary[3][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(8))
          dictionary[3].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][8])
          dictionary[3][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(9))
          dictionary[3].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][9])
          dictionary[3][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(10))
          dictionary[3].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][10])
          dictionary[3][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(11))
          dictionary[3].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][11])
          dictionary[3][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(12))
          dictionary[3].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][12])
          dictionary[3][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(13))
          dictionary[3].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][13])
          dictionary[3][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(14))
          dictionary[3].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][14])
          dictionary[3][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(15))
          dictionary[3].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][15])
          dictionary[3][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(16 /*0x10*/))
          dictionary[3].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[3][16 /*0x10*/])
          dictionary[3][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(17))
          dictionary[3].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][17])
          dictionary[3][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(18))
          dictionary[3].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][18])
          dictionary[3][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(19))
          dictionary[3].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][19])
          dictionary[3][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(20))
          dictionary[3].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][20])
          dictionary[3][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(21))
          dictionary[3].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][21])
          dictionary[3][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(22))
          dictionary[3].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][22])
          dictionary[3][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(23))
          dictionary[3].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][23])
          dictionary[3][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(24))
          dictionary[3].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][24])
          dictionary[3][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(25))
          dictionary[3].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][25])
          dictionary[3][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(26))
          dictionary[3].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][26])
          dictionary[3][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(27))
          dictionary[3].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][27])
          dictionary[3][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(28))
          dictionary[3].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][28])
          dictionary[3][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(29))
          dictionary[3].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][29])
          dictionary[3][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(30))
          dictionary[3].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[3][30])
          dictionary[3][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[3].ContainsKey(31 /*0x1F*/))
          dictionary[3].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[3][31 /*0x1F*/])
          dictionary[3][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 4)
      {
        if (!dictionary.ContainsKey(4))
          dictionary.Add(4, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[4].ContainsKey(2))
          dictionary[4].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][2])
          dictionary[4][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(3))
          dictionary[4].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][3])
          dictionary[4][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(4))
          dictionary[4].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][4])
          dictionary[4][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(5))
          dictionary[4].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][5])
          dictionary[4][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(6))
          dictionary[4].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][6])
          dictionary[4][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(7))
          dictionary[4].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][7])
          dictionary[4][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(8))
          dictionary[4].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][8])
          dictionary[4][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(9))
          dictionary[4].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][9])
          dictionary[4][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(10))
          dictionary[4].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][10])
          dictionary[4][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(11))
          dictionary[4].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][11])
          dictionary[4][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(12))
          dictionary[4].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][12])
          dictionary[4][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(13))
          dictionary[4].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][13])
          dictionary[4][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(14))
          dictionary[4].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][14])
          dictionary[4][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(15))
          dictionary[4].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][15])
          dictionary[4][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(16 /*0x10*/))
          dictionary[4].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[4][16 /*0x10*/])
          dictionary[4][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(17))
          dictionary[4].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][17])
          dictionary[4][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(18))
          dictionary[4].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][18])
          dictionary[4][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(19))
          dictionary[4].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][19])
          dictionary[4][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(20))
          dictionary[4].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][20])
          dictionary[4][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(21))
          dictionary[4].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][21])
          dictionary[4][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(22))
          dictionary[4].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][22])
          dictionary[4][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(23))
          dictionary[4].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][23])
          dictionary[4][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(24))
          dictionary[4].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][24])
          dictionary[4][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(25))
          dictionary[4].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][25])
          dictionary[4][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(26))
          dictionary[4].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][26])
          dictionary[4][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(27))
          dictionary[4].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][27])
          dictionary[4][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(28))
          dictionary[4].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][28])
          dictionary[4][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(29))
          dictionary[4].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][29])
          dictionary[4][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[4].ContainsKey(30))
          dictionary[4].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[4][30])
          dictionary[4][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 5)
      {
        if (!dictionary.ContainsKey(5))
          dictionary.Add(5, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[5].ContainsKey(2))
          dictionary[5].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][2])
          dictionary[5][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(3))
          dictionary[5].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][3])
          dictionary[5][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(4))
          dictionary[5].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][4])
          dictionary[5][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(5))
          dictionary[5].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][5])
          dictionary[5][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(6))
          dictionary[5].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][6])
          dictionary[5][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(7))
          dictionary[5].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][7])
          dictionary[5][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(8))
          dictionary[5].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][8])
          dictionary[5][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(9))
          dictionary[5].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][9])
          dictionary[5][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(10))
          dictionary[5].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][10])
          dictionary[5][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(11))
          dictionary[5].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][11])
          dictionary[5][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(12))
          dictionary[5].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][12])
          dictionary[5][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(13))
          dictionary[5].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][13])
          dictionary[5][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(14))
          dictionary[5].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][14])
          dictionary[5][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(15))
          dictionary[5].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][15])
          dictionary[5][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(16 /*0x10*/))
          dictionary[5].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[5][16 /*0x10*/])
          dictionary[5][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(17))
          dictionary[5].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][17])
          dictionary[5][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(18))
          dictionary[5].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][18])
          dictionary[5][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(19))
          dictionary[5].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][19])
          dictionary[5][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(20))
          dictionary[5].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][20])
          dictionary[5][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(21))
          dictionary[5].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][21])
          dictionary[5][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(22))
          dictionary[5].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][22])
          dictionary[5][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(23))
          dictionary[5].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][23])
          dictionary[5][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(24))
          dictionary[5].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][24])
          dictionary[5][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(25))
          dictionary[5].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][25])
          dictionary[5][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(26))
          dictionary[5].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][26])
          dictionary[5][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(27))
          dictionary[5].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][27])
          dictionary[5][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(28))
          dictionary[5].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][28])
          dictionary[5][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(29))
          dictionary[5].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][29])
          dictionary[5][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(30))
          dictionary[5].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[5][30])
          dictionary[5][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[5].ContainsKey(31 /*0x1F*/))
          dictionary[5].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[5][31 /*0x1F*/])
          dictionary[5][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 6)
      {
        if (!dictionary.ContainsKey(6))
          dictionary.Add(6, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[6].ContainsKey(2))
          dictionary[6].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][2])
          dictionary[6][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(3))
          dictionary[6].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][3])
          dictionary[6][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(4))
          dictionary[6].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][4])
          dictionary[6][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(5))
          dictionary[6].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][5])
          dictionary[6][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(6))
          dictionary[6].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][6])
          dictionary[6][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(7))
          dictionary[6].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][7])
          dictionary[6][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(8))
          dictionary[6].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][8])
          dictionary[6][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(9))
          dictionary[6].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][9])
          dictionary[6][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(10))
          dictionary[6].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][10])
          dictionary[6][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(11))
          dictionary[6].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][11])
          dictionary[6][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(12))
          dictionary[6].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][12])
          dictionary[6][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(13))
          dictionary[6].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][13])
          dictionary[6][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(14))
          dictionary[6].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][14])
          dictionary[6][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(15))
          dictionary[6].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][15])
          dictionary[6][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(16 /*0x10*/))
          dictionary[6].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[6][16 /*0x10*/])
          dictionary[6][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(17))
          dictionary[6].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][17])
          dictionary[6][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(18))
          dictionary[6].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][18])
          dictionary[6][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(19))
          dictionary[6].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][19])
          dictionary[6][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(20))
          dictionary[6].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][20])
          dictionary[6][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(21))
          dictionary[6].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][21])
          dictionary[6][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(22))
          dictionary[6].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][22])
          dictionary[6][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(23))
          dictionary[6].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][23])
          dictionary[6][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(24))
          dictionary[6].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][24])
          dictionary[6][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(25))
          dictionary[6].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][25])
          dictionary[6][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(26))
          dictionary[6].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][26])
          dictionary[6][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(27))
          dictionary[6].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][27])
          dictionary[6][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(28))
          dictionary[6].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][28])
          dictionary[6][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(29))
          dictionary[6].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][29])
          dictionary[6][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[6].ContainsKey(30))
          dictionary[6].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[6][30])
          dictionary[6][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 7)
      {
        if (!dictionary.ContainsKey(7))
          dictionary.Add(7, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[7].ContainsKey(2))
          dictionary[7].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][2])
          dictionary[7][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(3))
          dictionary[7].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][3])
          dictionary[7][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(4))
          dictionary[7].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][4])
          dictionary[7][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(5))
          dictionary[7].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][5])
          dictionary[7][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(6))
          dictionary[7].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][6])
          dictionary[7][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(7))
          dictionary[7].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][7])
          dictionary[7][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(8))
          dictionary[7].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][8])
          dictionary[7][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(9))
          dictionary[7].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][9])
          dictionary[7][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(10))
          dictionary[7].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][10])
          dictionary[7][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(11))
          dictionary[7].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][11])
          dictionary[7][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(12))
          dictionary[7].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][12])
          dictionary[7][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(13))
          dictionary[7].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][13])
          dictionary[7][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(14))
          dictionary[7].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][14])
          dictionary[7][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(15))
          dictionary[7].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][15])
          dictionary[7][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(16 /*0x10*/))
          dictionary[7].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[7][16 /*0x10*/])
          dictionary[7][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(17))
          dictionary[7].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][17])
          dictionary[7][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(18))
          dictionary[7].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][18])
          dictionary[7][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(19))
          dictionary[7].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][19])
          dictionary[7][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(20))
          dictionary[7].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][20])
          dictionary[7][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(21))
          dictionary[7].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][21])
          dictionary[7][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(22))
          dictionary[7].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][22])
          dictionary[7][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(23))
          dictionary[7].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][23])
          dictionary[7][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(24))
          dictionary[7].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][24])
          dictionary[7][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(25))
          dictionary[7].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][25])
          dictionary[7][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(26))
          dictionary[7].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][26])
          dictionary[7][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(27))
          dictionary[7].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][27])
          dictionary[7][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(28))
          dictionary[7].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][28])
          dictionary[7][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(29))
          dictionary[7].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][29])
          dictionary[7][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(30))
          dictionary[7].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[7][30])
          dictionary[7][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[7].ContainsKey(31 /*0x1F*/))
          dictionary[7].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[7][31 /*0x1F*/])
          dictionary[7][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 8)
      {
        if (!dictionary.ContainsKey(8))
          dictionary.Add(8, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[8].ContainsKey(2))
          dictionary[8].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][2])
          dictionary[8][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(3))
          dictionary[8].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][3])
          dictionary[8][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(4))
          dictionary[8].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][4])
          dictionary[8][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(5))
          dictionary[8].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][5])
          dictionary[8][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(6))
          dictionary[8].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][6])
          dictionary[8][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(7))
          dictionary[8].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][7])
          dictionary[8][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(8))
          dictionary[8].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][8])
          dictionary[8][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(9))
          dictionary[8].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][9])
          dictionary[8][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(10))
          dictionary[8].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][10])
          dictionary[8][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(11))
          dictionary[8].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][11])
          dictionary[8][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(12))
          dictionary[8].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][12])
          dictionary[8][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(13))
          dictionary[8].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][13])
          dictionary[8][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(14))
          dictionary[8].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][14])
          dictionary[8][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(15))
          dictionary[8].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][15])
          dictionary[8][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(16 /*0x10*/))
          dictionary[8].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[8][16 /*0x10*/])
          dictionary[8][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(17))
          dictionary[8].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][17])
          dictionary[8][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(18))
          dictionary[8].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][18])
          dictionary[8][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(19))
          dictionary[8].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][19])
          dictionary[8][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(20))
          dictionary[8].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][20])
          dictionary[8][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(21))
          dictionary[8].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][21])
          dictionary[8][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(22))
          dictionary[8].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][22])
          dictionary[8][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(23))
          dictionary[8].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][23])
          dictionary[8][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(24))
          dictionary[8].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][24])
          dictionary[8][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(25))
          dictionary[8].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][25])
          dictionary[8][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(26))
          dictionary[8].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][26])
          dictionary[8][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(27))
          dictionary[8].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][27])
          dictionary[8][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(28))
          dictionary[8].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][28])
          dictionary[8][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(29))
          dictionary[8].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][29])
          dictionary[8][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(30))
          dictionary[8].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[8][30])
          dictionary[8][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[8].ContainsKey(31 /*0x1F*/))
          dictionary[8].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[8][31 /*0x1F*/])
          dictionary[8][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 9)
      {
        if (!dictionary.ContainsKey(9))
          dictionary.Add(9, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[9].ContainsKey(2))
          dictionary[9].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][2])
          dictionary[9][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(3))
          dictionary[9].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][3])
          dictionary[9][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(4))
          dictionary[9].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][4])
          dictionary[9][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(5))
          dictionary[9].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][5])
          dictionary[9][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(6))
          dictionary[9].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][6])
          dictionary[9][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(7))
          dictionary[9].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][7])
          dictionary[9][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(8))
          dictionary[9].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][8])
          dictionary[9][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(9))
          dictionary[9].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][9])
          dictionary[9][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(10))
          dictionary[9].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][10])
          dictionary[9][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(11))
          dictionary[9].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][11])
          dictionary[9][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(12))
          dictionary[9].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][12])
          dictionary[9][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(13))
          dictionary[9].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][13])
          dictionary[9][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(14))
          dictionary[9].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][14])
          dictionary[9][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(15))
          dictionary[9].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][15])
          dictionary[9][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(16 /*0x10*/))
          dictionary[9].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[9][16 /*0x10*/])
          dictionary[9][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(17))
          dictionary[9].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][17])
          dictionary[9][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(18))
          dictionary[9].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][18])
          dictionary[9][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(19))
          dictionary[9].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][19])
          dictionary[9][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(20))
          dictionary[9].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][20])
          dictionary[9][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(21))
          dictionary[9].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][21])
          dictionary[9][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(22))
          dictionary[9].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][22])
          dictionary[9][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(23))
          dictionary[9].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][23])
          dictionary[9][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(24))
          dictionary[9].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][24])
          dictionary[9][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(25))
          dictionary[9].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][25])
          dictionary[9][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(26))
          dictionary[9].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][26])
          dictionary[9][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(27))
          dictionary[9].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][27])
          dictionary[9][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(28))
          dictionary[9].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][28])
          dictionary[9][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(29))
          dictionary[9].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][29])
          dictionary[9][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[9].ContainsKey(30))
          dictionary[9].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[9][30])
          dictionary[9][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 10)
      {
        if (!dictionary.ContainsKey(10))
          dictionary.Add(10, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[10].ContainsKey(2))
          dictionary[10].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][2])
          dictionary[10][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(3))
          dictionary[10].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][3])
          dictionary[10][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(4))
          dictionary[10].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][4])
          dictionary[10][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(5))
          dictionary[10].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][5])
          dictionary[10][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(6))
          dictionary[10].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][6])
          dictionary[10][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(7))
          dictionary[10].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][7])
          dictionary[10][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(8))
          dictionary[10].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][8])
          dictionary[10][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(9))
          dictionary[10].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][9])
          dictionary[10][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(10))
          dictionary[10].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][10])
          dictionary[10][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(11))
          dictionary[10].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][11])
          dictionary[10][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(12))
          dictionary[10].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][12])
          dictionary[10][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(13))
          dictionary[10].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][13])
          dictionary[10][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(14))
          dictionary[10].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][14])
          dictionary[10][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(15))
          dictionary[10].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][15])
          dictionary[10][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(16 /*0x10*/))
          dictionary[10].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[10][16 /*0x10*/])
          dictionary[10][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(17))
          dictionary[10].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][17])
          dictionary[10][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(18))
          dictionary[10].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][18])
          dictionary[10][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(19))
          dictionary[10].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][19])
          dictionary[10][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(20))
          dictionary[10].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][20])
          dictionary[10][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(21))
          dictionary[10].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][21])
          dictionary[10][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(22))
          dictionary[10].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][22])
          dictionary[10][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(23))
          dictionary[10].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][23])
          dictionary[10][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(24))
          dictionary[10].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][24])
          dictionary[10][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(25))
          dictionary[10].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][25])
          dictionary[10][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(26))
          dictionary[10].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][26])
          dictionary[10][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(27))
          dictionary[10].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][27])
          dictionary[10][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(28))
          dictionary[10].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][28])
          dictionary[10][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(29))
          dictionary[10].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][29])
          dictionary[10][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(30))
          dictionary[10].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[10][30])
          dictionary[10][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[10].ContainsKey(31 /*0x1F*/))
          dictionary[10].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[10][31 /*0x1F*/])
          dictionary[10][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 11)
      {
        if (!dictionary.ContainsKey(11))
          dictionary.Add(11, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[11].ContainsKey(2))
          dictionary[11].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][2])
          dictionary[11][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(3))
          dictionary[11].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][3])
          dictionary[11][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(4))
          dictionary[11].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][4])
          dictionary[11][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(5))
          dictionary[11].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][5])
          dictionary[11][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(6))
          dictionary[11].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][6])
          dictionary[11][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(7))
          dictionary[11].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][7])
          dictionary[11][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(8))
          dictionary[11].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][8])
          dictionary[11][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(9))
          dictionary[11].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][9])
          dictionary[11][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(10))
          dictionary[11].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][10])
          dictionary[11][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(11))
          dictionary[11].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][11])
          dictionary[11][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(12))
          dictionary[11].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][12])
          dictionary[11][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(13))
          dictionary[11].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][13])
          dictionary[11][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(14))
          dictionary[11].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][14])
          dictionary[11][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(15))
          dictionary[11].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][15])
          dictionary[11][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(16 /*0x10*/))
          dictionary[11].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[11][16 /*0x10*/])
          dictionary[11][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(17))
          dictionary[11].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][17])
          dictionary[11][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(18))
          dictionary[11].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][18])
          dictionary[11][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(19))
          dictionary[11].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][19])
          dictionary[11][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(20))
          dictionary[11].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][20])
          dictionary[11][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(21))
          dictionary[11].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][21])
          dictionary[11][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(22))
          dictionary[11].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][22])
          dictionary[11][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(23))
          dictionary[11].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][23])
          dictionary[11][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(24))
          dictionary[11].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][24])
          dictionary[11][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(25))
          dictionary[11].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][25])
          dictionary[11][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(26))
          dictionary[11].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][26])
          dictionary[11][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(27))
          dictionary[11].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][27])
          dictionary[11][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(28))
          dictionary[11].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][28])
          dictionary[11][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(29))
          dictionary[11].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][29])
          dictionary[11][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[11].ContainsKey(30))
          dictionary[11].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[11][30])
          dictionary[11][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
      }
      if (scheduleDisabled.StartMonth == 12)
      {
        if (!dictionary.ContainsKey(12))
          dictionary.Add(12, new Dictionary<int, bool>()
          {
            {
              1,
              1 >= scheduleDisabled.StartDay && 1 <= scheduleDisabled.EndDay
            }
          });
        if (!dictionary[12].ContainsKey(2))
          dictionary[12].Add(2, 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][2])
          dictionary[12][2] = 2 >= scheduleDisabled.StartDay && 2 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(3))
          dictionary[12].Add(3, 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][3])
          dictionary[12][3] = 3 >= scheduleDisabled.StartDay && 3 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(4))
          dictionary[12].Add(4, 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][4])
          dictionary[12][4] = 4 >= scheduleDisabled.StartDay && 4 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(5))
          dictionary[12].Add(5, 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][5])
          dictionary[12][5] = 5 >= scheduleDisabled.StartDay && 5 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(6))
          dictionary[12].Add(6, 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][6])
          dictionary[12][6] = 6 >= scheduleDisabled.StartDay && 6 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(7))
          dictionary[12].Add(7, 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][7])
          dictionary[12][7] = 7 >= scheduleDisabled.StartDay && 7 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(8))
          dictionary[12].Add(8, 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][8])
          dictionary[12][8] = 8 >= scheduleDisabled.StartDay && 8 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(9))
          dictionary[12].Add(9, 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][9])
          dictionary[12][9] = 9 >= scheduleDisabled.StartDay && 9 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(10))
          dictionary[12].Add(10, 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][10])
          dictionary[12][10] = 10 >= scheduleDisabled.StartDay && 10 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(11))
          dictionary[12].Add(11, 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][11])
          dictionary[12][11] = 11 >= scheduleDisabled.StartDay && 11 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(12))
          dictionary[12].Add(12, 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][12])
          dictionary[12][12] = 12 >= scheduleDisabled.StartDay && 12 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(13))
          dictionary[12].Add(13, 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][13])
          dictionary[12][13] = 13 >= scheduleDisabled.StartDay && 13 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(14))
          dictionary[12].Add(14, 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][14])
          dictionary[12][14] = 14 >= scheduleDisabled.StartDay && 14 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(15))
          dictionary[12].Add(15, 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][15])
          dictionary[12][15] = 15 >= scheduleDisabled.StartDay && 15 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(16 /*0x10*/))
          dictionary[12].Add(16 /*0x10*/, 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[12][16 /*0x10*/])
          dictionary[12][16 /*0x10*/] = 16 /*0x10*/ >= scheduleDisabled.StartDay && 16 /*0x10*/ <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(17))
          dictionary[12].Add(17, 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][17])
          dictionary[12][17] = 17 >= scheduleDisabled.StartDay && 17 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(18))
          dictionary[12].Add(18, 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][18])
          dictionary[12][18] = 18 >= scheduleDisabled.StartDay && 18 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(19))
          dictionary[12].Add(19, 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][19])
          dictionary[12][19] = 19 >= scheduleDisabled.StartDay && 19 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(20))
          dictionary[12].Add(20, 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][20])
          dictionary[12][20] = 20 >= scheduleDisabled.StartDay && 20 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(21))
          dictionary[12].Add(21, 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][21])
          dictionary[12][21] = 21 >= scheduleDisabled.StartDay && 21 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(22))
          dictionary[12].Add(22, 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][22])
          dictionary[12][22] = 22 >= scheduleDisabled.StartDay && 22 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(23))
          dictionary[12].Add(23, 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][23])
          dictionary[12][23] = 23 >= scheduleDisabled.StartDay && 23 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(24))
          dictionary[12].Add(24, 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][24])
          dictionary[12][24] = 24 >= scheduleDisabled.StartDay && 24 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(25))
          dictionary[12].Add(25, 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][25])
          dictionary[12][25] = 25 >= scheduleDisabled.StartDay && 25 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(26))
          dictionary[12].Add(26, 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][26])
          dictionary[12][26] = 26 >= scheduleDisabled.StartDay && 26 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(27))
          dictionary[12].Add(27, 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][27])
          dictionary[12][27] = 27 >= scheduleDisabled.StartDay && 27 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(28))
          dictionary[12].Add(28, 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][28])
          dictionary[12][28] = 28 >= scheduleDisabled.StartDay && 28 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(29))
          dictionary[12].Add(29, 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][29])
          dictionary[12][29] = 29 >= scheduleDisabled.StartDay && 29 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(30))
          dictionary[12].Add(30, 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay);
        else if (!dictionary[12][30])
          dictionary[12][30] = 30 >= scheduleDisabled.StartDay && 30 <= scheduleDisabled.EndDay;
        if (!dictionary[12].ContainsKey(31 /*0x1F*/))
          dictionary[12].Add(31 /*0x1F*/, 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay);
        else if (!dictionary[12][31 /*0x1F*/])
          dictionary[12][31 /*0x1F*/] = 31 /*0x1F*/ >= scheduleDisabled.StartDay && 31 /*0x1F*/ <= scheduleDisabled.EndDay;
      }
    }
    return dictionary;
  }

  public static List<NotificationScheduleDisabled> LoadByNotificationID(long notiID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationScheduleDisabled>("NotificationID", (object) notiID);
  }

  public static void DeleteByNotificationID(long notificationID)
  {
    Monnit.Data.NotificationScheduleDisabled.DeleteByNotificationID byNotificationId = new Monnit.Data.NotificationScheduleDisabled.DeleteByNotificationID(notificationID);
  }

  public static void DeleteByMonthAndNotificationID(int month, long notificationID)
  {
    Monnit.Data.NotificationScheduleDisabled.DeleteByMonthAndNotificationID andNotificationId = new Monnit.Data.NotificationScheduleDisabled.DeleteByMonthAndNotificationID(month, notificationID);
  }
}
