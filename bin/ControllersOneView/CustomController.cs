// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.CustomController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using RedefineImpossible;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class CustomController : OverviewControllerBase
{
  [AuthorizeDefault]
  public ActionResult ThemeView(string view, long? id)
  {
    string viewPath = MonnitViewEngine.FindViewPath(this.ControllerContext, view, nameof (ThemeView), MonnitSession.CurrentTheme.Theme);
    if (!string.IsNullOrEmpty(viewPath))
      return (ActionResult) this.View(viewPath, (object) id);
    return view == "PowerOutage_History" ? (ActionResult) this.RedirectPermanent("PowerEvent_History#Content") : (ActionResult) this.Content("View Not Found");
  }

  [AuthorizeDefault]
  public ActionResult HadliExportHTHistoryMonth(long id)
  {
    DataTable dtDataTable = new DataTable();
    IDbCommand dbCommand = Database.GetDBCommand("Custom_Hadli_DataExport_HTSensor");
    dbCommand.CommandType = CommandType.StoredProcedure;
    dbCommand.Parameters.Add((object) Database.GetParameter("@SensorID", (object) id));
    dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) DateTime.Now.AddDays(-30.0)));
    dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) DateTime.Now));
    DataSet dataSet = Database.ExecuteQuery(dbCommand);
    if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0)
      dtDataTable = dataSet.Tables[0];
    dbCommand.Connection.Close();
    string fileDownloadName = $"HadliExportHTHistoryMonth-{id}-{DateTime.Now.ToString("yyyyMMdd")}.csv";
    return (ActionResult) this.File(Encoding.ASCII.GetBytes(dtDataTable.ToCSVString()), "text/csv", fileDownloadName);
  }

  [AuthorizeDefault]
  public ActionResult HadliExportHTHistoryYear(long id)
  {
    DataTable dtDataTable = new DataTable();
    IDbCommand dbCommand = Database.GetDBCommand("Custom_Hadli_DataExport_HTSensor");
    dbCommand.CommandType = CommandType.StoredProcedure;
    dbCommand.Parameters.Add((object) Database.GetParameter("@SensorID", (object) id));
    dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) DateTime.Now.AddDays(-365.0)));
    dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) DateTime.Now));
    DataSet dataSet = Database.ExecuteQuery(dbCommand);
    if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0)
      dtDataTable = dataSet.Tables[0];
    dbCommand.Connection.Close();
    string fileDownloadName = $"HadliExportHTHistoryYear-{id}-{DateTime.Now.ToString("yyyyMMdd")}.csv";
    return (ActionResult) this.File(Encoding.ASCII.GetBytes(dtDataTable.ToCSVString()), "text/csv", fileDownloadName);
  }
}
