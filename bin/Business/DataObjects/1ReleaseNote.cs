// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ReleaseNote
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class ReleaseNote
{
  [DBMethod("ReleaseNote_LoadAllBasedOnReleaseDateAndThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ReleaseNote] a WITH (NOLOCK)\r\nLEFT JOIN dbo.[ReleaseNoteViewed] b           WITH (NOLOCK) ON b.[ReleaseNoteID] = a.[ReleaseNoteID] AND (b.[CustomerID] = @CustomerID)\r\nLEFT JOIN dbo.[AccountThemeReleaseNoteLink] c WITH (NOLOCK) ON c.[ReleaseNoteID] = a.[ReleaseNoteID]\r\nWHERE b.[ReleaseNoteViewedId] IS NULL\r\n  AND (c.[IsHidden]       = 0               OR c.[IsHidden]       IS NULL)\r\n  AND (c.[AccountThemeID] = @AccountThemeID OR c.[AccountThemeID] IS NULL);\r\n")]
  internal class LoadAllBasedOnReleaseDateAndThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public DataTable Result { get; private set; }

    public LoadAllBasedOnReleaseDateAndThemeID(long accountThemeID, long customerID)
    {
      this.AccountThemeID = accountThemeID;
      this.CustomerID = customerID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("ReleaseNote_LoadAllReleaseNotesByDateAndVersion")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ReleaseNote] WITH (NOLOCK)\r\nWHERE [ReleaseDate] >= @ReleaseDate\r\n  AND CONVERT(INT, LEFT([Version], CHARINDEX('.', [Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n\r\nSELECT\r\n  a.*\r\nFROM dbo.[AccountThemeReleaseNoteLink] a WITH (NOLOCK)\r\nINNER JOIN dbo.[ReleaseNote] rn WITH (NOLOCK) ON rn.[ReleaseNoteID] = a.[ReleaseNoteID]\r\nWHERE rn.[ReleaseDate] >= @ReleaseDate\r\n  AND CONVERT(INT, LEFT(rn.[Version], CHARINDEX('.', rn.[Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n")]
  internal class LoadAllReleaseNotesByDateAndVersion : BaseDBMethod
  {
    [DBMethodParam("ReleaseDate", typeof (DateTime))]
    public DateTime ReleaseDate { get; private set; }

    [DBMethodParam("Version", typeof (string))]
    public string Version { get; private set; }

    public List<Monnit.ReleaseNote> Result { get; private set; }

    public LoadAllReleaseNotesByDateAndVersion(Version version, DateTime dateTime, bool isTestView)
    {
      this.ReleaseDate = dateTime;
      this.Version = version.ToString();
      DataSet dataSet = this.ToDataSet();
      List<Monnit.ReleaseNote> releaseNoteList = BaseDBObject.Load<Monnit.ReleaseNote>(dataSet.Tables[0]);
      List<Monnit.AccountThemeReleaseNoteLink> source = BaseDBObject.Load<Monnit.AccountThemeReleaseNoteLink>(dataSet.Tables[1]);
      for (int index = releaseNoteList.Count - 1; index >= 0; --index)
      {
        if (!isTestView && new Version(releaseNoteList[index].Version) > version)
          releaseNoteList.RemoveAt(index);
      }
      foreach (Monnit.ReleaseNote releaseNote in releaseNoteList)
      {
        Monnit.ReleaseNote rn = releaseNote;
        foreach (Monnit.AccountThemeReleaseNoteLink themeReleaseNoteLink in source.Where<Monnit.AccountThemeReleaseNoteLink>((System.Func<Monnit.AccountThemeReleaseNoteLink, bool>) (l => l.ReleaseNoteID == rn.ReleaseNoteID)))
          rn.dic.Add(themeReleaseNoteLink.AccountThemeID, themeReleaseNoteLink);
      }
      this.Result = releaseNoteList;
    }
  }

  [DBMethod("ReleaseNote_LoadActiveReleaseNotesByDateAndVersion")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ReleaseNote] WITH(NOLOCK)\r\nWHERE [ReleaseDate] >= @ReleaseDate\r\n  AND [IsDeleted]   = 0\r\n  AND CONVERT(INT, LEFT([Version], CHARINDEX('.', [Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n\r\nSELECT\r\n  a.*\r\nFROM dbo.[AccountThemeReleaseNoteLink] a WITH (NOLOCK)\r\nINNER JOIN dbo.[ReleaseNote] rn WITH (NOLOCK) ON rn.[ReleaseNoteID] = a.[ReleaseNoteID]\r\nWHERE rn.[ReleaseDate] >= @ReleaseDate\r\n  AND CONVERT(INT, LEFT(rn.[Version], CHARINDEX('.', rn.[Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n")]
  internal class LoadActiveReleaseNotesByDateAndVersion : BaseDBMethod
  {
    [DBMethodParam("ReleaseDate", typeof (DateTime))]
    public DateTime ReleaseDate { get; private set; }

    [DBMethodParam("Version", typeof (string))]
    public string Version { get; private set; }

    public List<Monnit.ReleaseNote> Result { get; private set; }

    public LoadActiveReleaseNotesByDateAndVersion(
      Version version,
      DateTime dateTime,
      bool isTestView)
    {
      this.ReleaseDate = dateTime;
      this.Version = version.ToString();
      DataSet dataSet = this.ToDataSet();
      List<Monnit.ReleaseNote> releaseNoteList = BaseDBObject.Load<Monnit.ReleaseNote>(dataSet.Tables[0]);
      List<Monnit.AccountThemeReleaseNoteLink> source = BaseDBObject.Load<Monnit.AccountThemeReleaseNoteLink>(dataSet.Tables[0]);
      for (int index = releaseNoteList.Count - 1; index >= 0; --index)
      {
        if (!isTestView && new Version(releaseNoteList[index].Version) > version)
          releaseNoteList.RemoveAt(index);
      }
      foreach (Monnit.ReleaseNote releaseNote in releaseNoteList)
      {
        Monnit.ReleaseNote rn = releaseNote;
        foreach (Monnit.AccountThemeReleaseNoteLink themeReleaseNoteLink in source.Where<Monnit.AccountThemeReleaseNoteLink>((System.Func<Monnit.AccountThemeReleaseNoteLink, bool>) (l => l.ReleaseNoteID == rn.ReleaseNoteID)))
          rn.dic.Add(themeReleaseNoteLink.AccountThemeID, themeReleaseNoteLink);
      }
      this.Result = releaseNoteList;
    }
  }

  [DBMethod("ReleaseNote_LoadAllReleaseNotesByVersion")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    SELECT\r\n      *\r\n    FROM dbo.[ReleaseNote] WITH (NOLOCK)\r\n    WHERE CONVERT(INT, LEFT([Version], CHARINDEX('.', [Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n\r\n    SELECT\r\n      a.*\r\n    FROM dbo.[AccountThemeReleaseNoteLink] a WITH (NOLOCK)\r\n    INNER JOIN dbo.[ReleaseNote] rn WITH (NOLOCK) ON rn.[ReleaseNoteID] = a.[ReleaseNoteID]\r\n    WHERE CONVERT(INT, LEFT(rn.[Version], CHARINDEX('.', rn.[Version])-1)) <= CONVERT(INT, LEFT(@Version, CHARINDEX('.', @Version)-1));\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n  \r\n\tDECLARE @Recipients varchar(500)\r\n\tDECLARE @Subject varchar(30)\r\n\tDECLARE @Body VARCHAR(2000)\r\n\r\n\tDECLARE @Params VARCHAR(1000)\r\n  SET @Params = '@Version: ' + CONVERT(VARCHAR(100), @Version)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 2, @ErrorSysMsg, @Params)\r\n  \r\n\tSET @Body = '<p>Team, </p> <p>Critical Procedure Failed: '+@ProcName+'. Please Address. '+CONVERT(VARCHAR(20), GETDATE() )+' </p> \r\n  <p>ErrorMessage: '+CONVERT(VARCHAR(20), @ErrorNum) +' ' + @ErrorSysMsg+'</p>\r\n\t<p>Sincerely,</p><p>-DBA</p>'\r\n\r\n\tSET @Subject = 'MonnitDB ProcFail - Urgency 2'\r\n\tSET @Recipients = (select value from ConfigData where KeyName = 'DB_Procfail_Contacts')\r\n\r\n    EXEC msdb.dbo.sp_send_dbmail \r\n\t  @Profile_name = 'Alerts',\r\n\t  @Recipients = @Recipients , \r\n      @subject = @Subject,  \r\n      @body = @Body,  \r\n      @body_format = 'HTML' ;  \r\n\r\nEND CATCH\r\n")]
  internal class LoadAllReleaseNotesByVersion : BaseDBMethod
  {
    [DBMethodParam("Version", typeof (string))]
    public string Version { get; private set; }

    public List<Monnit.ReleaseNote> Result { get; private set; }

    public LoadAllReleaseNotesByVersion(Version version)
    {
      this.Version = version.ToString();
      DataSet dataSet = this.ToDataSet();
      List<Monnit.ReleaseNote> releaseNoteList = BaseDBObject.Load<Monnit.ReleaseNote>(dataSet.Tables[0]);
      List<Monnit.AccountThemeReleaseNoteLink> source = BaseDBObject.Load<Monnit.AccountThemeReleaseNoteLink>(dataSet.Tables[1]);
      for (int index = releaseNoteList.Count - 1; index >= 0; --index)
      {
        if (new Version(releaseNoteList[index].Version) > new Version(this.Version))
          releaseNoteList.RemoveAt(index);
      }
      foreach (Monnit.ReleaseNote releaseNote in releaseNoteList)
      {
        Monnit.ReleaseNote rn = releaseNote;
        foreach (Monnit.AccountThemeReleaseNoteLink themeReleaseNoteLink in source.Where<Monnit.AccountThemeReleaseNoteLink>((System.Func<Monnit.AccountThemeReleaseNoteLink, bool>) (l => l.ReleaseNoteID == rn.ReleaseNoteID)))
          rn.dic.Add(themeReleaseNoteLink.AccountThemeID, themeReleaseNoteLink);
      }
      this.Result = releaseNoteList;
    }
  }
}
