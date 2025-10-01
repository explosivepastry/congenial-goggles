// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.ReportController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class ReportController : ThemeController
{
  [Authorize]
  public ActionResult Index()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReportIndex",
      controller = "Export"
    });
  }

  [Authorize]
  public ActionResult MainList()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReportIndex",
      controller = "Export"
    });
  }

  [Authorize]
  public ActionResult List()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReportIndex",
      controller = "Export"
    });
  }

  [Authorize]
  public ActionResult AdminOverview()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReportIndex",
      controller = "Export"
    });
  }

  [Authorize]
  public ActionResult ManageTags()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReportIndex",
      controller = "Export"
    });
  }

  [Authorize]
  public ActionResult AddTag(string id)
  {
    ReportingTag reportingTag = ReportingTag.LoadByAccount(MonnitSession.CurrentCustomer.AccountID);
    reportingTag.AddTag(id);
    reportingTag.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult RemoveTag(string id)
  {
    ReportingTag reportingTag = ReportingTag.LoadByAccount(MonnitSession.CurrentCustomer.AccountID);
    reportingTag.RemoveTag(id);
    reportingTag.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult AccessLog()
  {
    return (ActionResult) this.PartialView("DateRange", (object) new DateRangeModel());
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AccessLog(DateRangeModel Dates)
  {
    return this.ModelState.IsValid ? (ActionResult) this.PartialView((object) AuthenticationLog.LoadByAccountAndDateRange(MonnitSession.CurrentCustomer.AccountID, MonnitSession.MakeUTC(Dates.StartDate), MonnitSession.MakeUTC(Dates.EndDate.AddDays(1.0)))) : (ActionResult) this.PartialView("DateRange", (object) Dates);
  }

  [Authorize]
  public ActionResult SubscriptionExpiring()
  {
    return (ActionResult) this.View(!MonnitSession.IsCurrentCustomerMonnitAdmin ? (object) AccountSubscription.ResellerExpirationReport(MonnitSession.CurrentCustomer.AccountID) : (object) AccountSubscription.ExpirationReport());
  }

  [Authorize]
  public ActionResult NewAccounts()
  {
    return (ActionResult) this.PartialView("DateRange", (object) new DateRangeModel());
  }

  [Authorize]
  public ActionResult ResellerActivation()
  {
    return (ActionResult) this.PartialView("DateRange", (object) new DateRangeModel());
  }

  [Authorize]
  public ActionResult BatteryHealthReport()
  {
    return (ActionResult) this.PartialView(nameof (BatteryHealthReport), (object) Reporting.BatteryHealthReport.Exec(MonnitSession.CurrentCustomer.AccountID));
  }

  [Authorize]
  public ActionResult ResellerReport()
  {
    return (ActionResult) this.PartialView(nameof (ResellerReport), (object) Reporting.ResellerAccountReport.Exec(MonnitSession.CurrentCustomer.AccountID));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult NewAccounts(DateRangeModel Dates)
  {
    if (!this.ModelState.IsValid)
      return (ActionResult) this.PartialView("DateRange", (object) Dates);
    DateTime utcStart = MonnitSession.MakeUTC(Dates.StartDate);
    DateTime utcEnd = MonnitSession.MakeUTC(Dates.EndDate.AddDays(1.0));
    return (ActionResult) this.PartialView(!MonnitSession.IsCurrentCustomerMonnitAdmin ? (object) Account.ResellerNewAccounts(utcStart, utcEnd, MonnitSession.CurrentCustomer.AccountID) : (object) Account.NewAccounts(utcStart, utcEnd));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ResellerActivation(DateRangeModel Dates)
  {
    if (!this.ModelState.IsValid)
      return (ActionResult) this.PartialView("DateRange", (object) Dates);
    DataTable dataTable = AccountSubscription.ResellerActivation(MonnitSession.MakeUTC(Dates.StartDate), MonnitSession.MakeUTC(Dates.EndDate.AddDays(1.0)), MonnitSession.CurrentCustomer.AccountID);
    System.Collections.Generic.List<ResellerActivationModel> model = new System.Collections.Generic.List<ResellerActivationModel>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      model.Add(new ResellerActivationModel(row));
    return (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  public ActionResult CleanUpOldReportsFromAzure()
  {
    string str = "Success";
    string content;
    try
    {
      if (MonnitSession.IsCurrentCustomerMonnitAdmin)
      {
        int num = ScheduledReportsToStorage.DeleteBatchOfReports();
        content = $"{str}|Deleted: {num.ToString()}";
      }
      else
        content = "Failed";
    }
    catch (Exception ex)
    {
      ex.Log("ReportController.CleanUpOldReportsFromAzure ");
      content = ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  public ActionResult GetReportFile(string guid, long ScheduledReportsToStorageID)
  {
    ScheduledReportsToStorage reportsToStorage = ScheduledReportsToStorage.Load(ScheduledReportsToStorageID);
    string searchFileName = reportsToStorage.ReportFileName.Replace(",", "").Replace("/", "_").Replace("\\", "_");
    AzureTempFile azureTempFile = AzureTempFile.Files($"reportfile/{reportsToStorage.GUID}/", searchFileName).FirstOrDefault<AzureTempFile>();
    if (azureTempFile != null && guid == reportsToStorage.GUID)
    {
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + azureTempFile.FileName);
      string[] strArray = azureTempFile.FileName.Split('.');
      switch (strArray[strArray.Length - 1].ToStringSafe().ToLower())
      {
        case "pdf":
          this.Response.ContentType = "application/pdf";
          break;
        case "csv":
          this.Response.ContentType = "text/csv";
          break;
        default:
          this.Response.ContentType = "application/octet-stream";
          break;
      }
      azureTempFile.DownloadToStream(this.Response.OutputStream);
    }
    return (ActionResult) new EmptyResult();
  }

  [Authorize]
  [NoCache]
  public ActionResult ChartMultiple(string ids)
  {
    this.ViewData[nameof (ids)] = (object) ids;
    System.Collections.Generic.List<Sensor> sensors = new System.Collections.Generic.List<Sensor>();
    long num = long.MinValue;
    string[] strArray = ids.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    CustomerPermissionType permissionType = CustomerPermissionType.Find("Network_View");
    foreach (object o in strArray)
    {
      Sensor sensor = Sensor.Load(o.ToLong());
      if (sensor != null && MonnitSession.CurrentCustomer.Can(permissionType, sensor.CSNetID))
      {
        num = sensor.ApplicationID;
        sensors.Add(sensor);
      }
    }
    DateTime date = MonnitSession.HistoryFromDate.Date;
    DateTime toDate = MonnitSession.HistoryToDate.Date.AddDays(1.0);
    ChartSensorDataModel model = new ChartSensorDataModel(sensors, date, toDate);
    return sensors.Count > 0 ? (ActionResult) this.View((object) model) : (ActionResult) this.View("NoChart");
  }

  public static string BuildMultiChartDataString(System.Collections.Generic.List<DataPoint> list, string sensorName, int i)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine($"\nvar data{i.ToString()} = new google.visualization.DataTable();");
    stringBuilder.AppendLine($"\ndata{i.ToString()}.addColumn('datetime', 'Date');");
    stringBuilder.AppendLine($"\ndata{i.ToString()}.addColumn('number', '{WebUtility.HtmlDecode(sensorName).Replace("'", "'+\"'\"+'")}');");
    stringBuilder.Append($"\ndata{i.ToString()}.addRows(");
    stringBuilder.Append("[");
    bool flag = true;
    foreach (DataPoint dataPoint in list)
    {
      if (dataPoint.Value != null)
      {
        if (!flag)
          stringBuilder.Append(",");
        DateTime dateTime = MonnitSession.MakeLocal(dataPoint.Date);
        stringBuilder.AppendFormat("[new Date('{1}/{2}/{0} {3}:{4}:{5}'), {6}]", (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second, dataPoint.Value);
        flag = false;
      }
    }
    stringBuilder.Append("]");
    stringBuilder.AppendLine(");");
    return stringBuilder.ToString();
  }

  [NoCache]
  [Authorize]
  public ActionResult ChartMultipleDataRefresh(string ids, DateTime fromDate, DateTime toDate)
  {
    System.Collections.Generic.List<Sensor> sensors = new System.Collections.Generic.List<Sensor>();
    string str = ids;
    char[] chArray = new char[1]{ '|' };
    foreach (string o in str.Split(chArray))
      sensors.Add(Sensor.Load(o.ToLong()));
    return (ActionResult) this.View((object) new ChartSensorDataModel(sensors, fromDate, toDate));
  }

  [Authorize]
  public ActionResult DeviceLookup() => (ActionResult) this.PartialView();

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeviceLookup(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (sensor != null)
    {
      CSNet csNet = CSNet.Load(sensor.CSNetID);
      if (csNet == null || MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.PartialView("SensorLookup", (object) new SensorInformation(sensor));
    }
    Gateway gateway = Gateway.Load(id);
    if (gateway != null)
    {
      CSNet csNet = CSNet.Load(gateway.CSNetID);
      if (csNet == null || MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.PartialView("GatewayLookup", (object) new GatewayInformation(gateway));
    }
    return (ActionResult) this.PartialView((object) id);
  }

  [Authorize]
  public ActionResult UserSearch()
  {
    return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.PartialView() : (ActionResult) this.PartialView("Unauthorized");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserSearch(string id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.PartialView("Unauthorized");
    DataTable dataTable = Customer.Search(id);
    System.Collections.Generic.List<UserLookUpModel> userLookUpModelList = new System.Collections.Generic.List<UserLookUpModel>();
    Dictionary<long, Account> dictionary = new Dictionary<long, Account>();
    this.ViewData["query"] = (object) id;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (MonnitSession.IsCurrentCustomerMonnitAdmin)
        userLookUpModelList.Add(new UserLookUpModel(row));
      else if (MonnitSession.CurrentCustomer.AccountID == row["RetailAccountID"].ToLong())
      {
        userLookUpModelList.Add(new UserLookUpModel(row));
      }
      else
      {
        long retailAccountId = row["RetailAccountID"].ToLong();
        Account account;
        do
        {
          if (dictionary.ContainsKey(retailAccountId))
          {
            account = dictionary[retailAccountId];
          }
          else
          {
            account = Account.Load(retailAccountId);
            if (account != null)
              dictionary.Add(retailAccountId, account);
          }
          if (account != null)
          {
            retailAccountId = account.RetailAccountID;
            if (account.RetailAccountID == MonnitSession.CurrentCustomer.AccountID)
            {
              userLookUpModelList.Add(new UserLookUpModel(row));
              break;
            }
          }
        }
        while (account != null);
      }
    }
    System.Collections.Generic.List<UserLookUpModel> model = new System.Collections.Generic.List<UserLookUpModel>();
    UserLookUpModel userLookUpModel1 = (UserLookUpModel) null;
    if (userLookUpModelList.Count > 0)
    {
      foreach (UserLookUpModel userLookUpModel2 in userLookUpModelList)
      {
        if (model.Count < 1)
        {
          model.Add(userLookUpModel2);
          userLookUpModel1 = userLookUpModel2;
        }
        else if (userLookUpModel1.CustomerID != userLookUpModel2.CustomerID)
        {
          model.Add(userLookUpModel2);
          userLookUpModel1 = userLookUpModel2;
        }
        else
          userLookUpModel1 = userLookUpModel2;
      }
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  public ActionResult SensorTypeSearch()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.PartialView("Unauthorized") : (ActionResult) this.PartialView();
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorTypeSearch(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["ApplicationID"] = (object) id;
    return (ActionResult) this.PartialView((object) Sensor.LoadByApplicationID(id, 25));
  }

  [Authorize]
  public ActionResult AdminManagement()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportQuery.LoadAll().Where<ReportQuery>((System.Func<ReportQuery, bool>) (r => !r.IsDeleted)).ToList<ReportQuery>());
  }

  [Authorize]
  public ActionResult EditQuery(long id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportQuery.Load(id));
  }

  [Authorize]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult EditQuery(ReportQuery query)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    this.ModelState["AccountID"].Errors.Clear();
    this.ModelState["AccountThemeID"].Errors.Clear();
    if (query.AccountID > 0L && Account.Load(query.AccountID) == null)
      this.ModelState.AddModelError("AccountID", "Invalid AccountID");
    if (query.AccountThemeID > 0L && AccountTheme.Load(query.AccountThemeID) == null)
      this.ModelState.AddModelError("AccountThemeID", "Invalid AccountThemeID");
    query.ScheduleImmediately = (this.Request["ScheduleImmediately"].Split(',')[0].ToBool() ? 1 : 0) != 0;
    if (this.ModelState.IsValid)
    {
      bool flag = query.ReportQueryID < 1L;
      query.Save();
      if (flag)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = nameof (EditQuery),
          controller = "Report",
          id = query.ReportQueryID
        });
      this.ModelState.AddModelError("", "Saved");
    }
    return (ActionResult) this.View((object) query);
  }

  [Authorize]
  public ActionResult CreateQuery()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View("EditQuery", (object) new ReportQuery());
  }

  [Authorize]
  public ActionResult DeleteQuery(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportQuery.Load(id).Delete();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult CreateParameter(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    return (ActionResult) this.View("EditParameter", (object) new ReportParameter()
    {
      ReportQueryID = id
    });
  }

  [Authorize]
  public ActionResult EditParameter(long id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportParameter.Load(id));
  }

  [Authorize]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult EditParameter(ReportParameter parameter)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(parameter.ParamName))
      this.ModelState.AddModelError("ParamName", "Parameter Name is Required");
    if (string.IsNullOrEmpty(parameter.PredefinedValues))
      ;
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) parameter);
    parameter.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult DeleteParameter(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportParameter reportParameter = ReportParameter.Load(id);
    foreach (BaseDBObject baseDbObject in ReportParameterValue.LoadByReportParameterID(reportParameter.ReportParameterID))
      baseDbObject.Delete();
    try
    {
      reportParameter.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to delete parameter!");
    }
  }

  [Authorize]
  public ActionResult MoveParameter(long id, string dir)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportParameter reportParameter = ReportParameter.Load(id);
    if (reportParameter == null)
      return (ActionResult) this.Content("Not Found");
    ReportQuery reportQuery = ReportQuery.Load(reportParameter.ReportQueryID);
    int index1 = 0;
    foreach (ReportParameter parameter in reportQuery.Parameters)
    {
      if (parameter.ReportParameterID != reportParameter.ReportParameterID)
        ++index1;
      else
        break;
    }
    reportQuery.Parameters.RemoveAt(index1);
    int index2 = !(dir == "Up") ? index1 + 1 : index1 - 1;
    if (index2 < 0)
      index2 = 0;
    if (index2 >= reportQuery.Parameters.Count)
      index2 = reportQuery.Parameters.Count;
    reportQuery.Parameters.Insert(index2, reportParameter);
    int num = 0;
    foreach (ReportParameter parameter in reportQuery.Parameters)
    {
      parameter.SortOrder = num;
      parameter.Save();
      ++num;
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult ReportDetails(long id)
  {
    ReportSchedule model = ReportSchedule.Load(id);
    return model == null ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult UserList(long id, string q)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    return reportSchedule == null ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.View((object) Customer.LoadAllByAccount(MonnitSession.CurrentCustomer.AccountID).Where<Customer>((System.Func<Customer, bool>) (c => c.FullName.ToLower().Contains(q.ToLower()) || c.UserName.ToLower().Contains(q.ToLower()))).Except<Customer>(reportSchedule.DistributionList.Select<ReportDistribution, Customer>((System.Func<ReportDistribution, Customer>) (rd => rd.Customer))));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AddRecipient(long id, long[] customerIDs)
  {
    try
    {
      ReportSchedule reportSchedule = ReportSchedule.Load(id);
      if (reportSchedule == null)
        return (ActionResult) this.Content("Report Schedule not found");
      foreach (long customerId in customerIDs)
      {
        Customer cust = Customer.Load(customerId);
        CustomerAccountLink customerAccountLink = CustomerAccountLink.Load(cust.CustomerID, reportSchedule.AccountID);
        if (cust != null && (customerAccountLink != null || reportSchedule.AccountID == cust.AccountID))
          reportSchedule.AddCustomer(cust);
      }
      return (ActionResult) this.View((object) reportSchedule.DistributionList);
    }
    catch
    {
      return (ActionResult) this.Content("<script type='text/javascript'>alert('Unable to add recipient(s).');</script>");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult RemoveRecipient(long id, long customerID)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (reportSchedule == null)
      return (ActionResult) this.Content("Report Schedule not found");
    try
    {
      reportSchedule.RemoveCustomer(Customer.Load(customerID));
      return (ActionResult) this.View("AddRecipient", (object) reportSchedule.DistributionList);
    }
    catch
    {
      return (ActionResult) this.View("AddRecipient", (object) reportSchedule.DistributionList);
    }
  }

  [Authorize]
  public ActionResult BuildReport(long? id, long? queryID)
  {
    ReportSchedule model = ReportSchedule.Load(id ?? long.MinValue);
    if (model == null)
    {
      if (!queryID.HasValue)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      model = new ReportSchedule();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.Name = "New Report";
      model.ReportQueryID = queryID ?? long.MinValue;
      model.Schedule = "1";
      model.ScheduleType = eReportScheduleType.Monthly;
      ReportSchedule reportSchedule1 = model;
      DateTime now = DateTime.Now;
      DateTime date1 = now.Date;
      reportSchedule1.StartDate = date1;
      ReportSchedule reportSchedule2 = model;
      now = DateTime.Now;
      DateTime date2 = now.Date;
      reportSchedule2.EndDate = date2;
      model.StartTime = new TimeSpan(12, 0, 0);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult BuildReport(ReportSchedule model, FormCollection collection)
  {
    long reportQueryId = model.ReportQueryID;
    if (false)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (model.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (string.IsNullOrWhiteSpace(model.Name))
      this.ModelState.AddModelError("Name", "Name is required");
    if (string.IsNullOrWhiteSpace(model.Schedule))
      this.ModelState.AddModelError("Schedule", "Schedule is required");
    DateTime date = Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID).Date;
    DateTime startDate = model.StartDate;
    if (model.StartDate < new DateTime(2014, 1, 1) || model.StartDate > date.AddYears(1))
      this.ModelState.AddModelError("StartDate", "Start Date must be valid date");
    DateTime endDate = model.EndDate;
    if (model.EndDate < date || model.EndDate < model.StartDate)
      this.ModelState.AddModelError("EndDate", "End Date must be valid date in the future");
    TimeSpan startTime = model.StartTime;
    if (false)
      model.StartTime = new TimeSpan(12, 0, 0);
    System.Collections.Generic.List<ReportParameterValue> reportParameterValueList = new System.Collections.Generic.List<ReportParameterValue>();
    foreach (ReportParameter parameter in model.Report.Parameters)
    {
      FormCollection formCollection1 = collection;
      long reportParameterId = parameter.ReportParameterID;
      string name1 = "Param_" + reportParameterId.ToString();
      if (string.IsNullOrWhiteSpace(formCollection1[name1]))
      {
        ModelStateDictionary modelState = this.ModelState;
        reportParameterId = parameter.ReportParameterID;
        string key = "Param_" + reportParameterId.ToString();
        string errorMessage = parameter.LabelText + " is required.";
        modelState.AddModelError(key, errorMessage);
      }
      else
      {
        FormCollection formCollection2 = collection;
        reportParameterId = parameter.ReportParameterID;
        string name2 = "Param_" + reportParameterId.ToString();
        string tempValue = formCollection2[name2];
        string str1 = ReportController.ValidateParameterValueType(parameter, tempValue);
        if (!string.IsNullOrEmpty(str1))
        {
          ModelStateDictionary modelState = this.ModelState;
          reportParameterId = parameter.ReportParameterID;
          string key = "Param_" + reportParameterId.ToString();
          string errorMessage = parameter.LabelText + str1;
          modelState.AddModelError(key, errorMessage);
        }
        string str2 = ReportController.ValidateParameterType(parameter, tempValue);
        if (!string.IsNullOrEmpty(str2))
        {
          ModelStateDictionary modelState = this.ModelState;
          reportParameterId = parameter.ReportParameterID;
          string key = "Param_" + reportParameterId.ToString();
          string errorMessage = parameter.LabelText + str2;
          modelState.AddModelError(key, errorMessage);
        }
        ReportParameterValue reportParameterValue1 = model.FindParameter(parameter.ReportParameterID);
        if (reportParameterValue1 == null)
        {
          reportParameterValue1 = new ReportParameterValue();
          reportParameterValue1.ReportParameterID = parameter.ReportParameterID;
        }
        ReportParameterValue reportParameterValue2 = reportParameterValue1;
        FormCollection formCollection3 = collection;
        reportParameterId = parameter.ReportParameterID;
        string name3 = "Param_" + reportParameterId.ToString();
        string str3 = formCollection3[name3];
        reportParameterValue2.Value = str3;
        reportParameterValueList.Add(reportParameterValue1);
      }
    }
    if (this.ModelState.IsValid)
    {
      bool flag = false;
      if (model.PrimaryKeyValue < 0L)
        flag = true;
      model.Save();
      foreach (ReportParameterValue reportParameterValue in reportParameterValueList)
      {
        reportParameterValue.ReportScheduleID = model.ReportScheduleID;
        reportParameterValue.Save();
      }
      if (flag)
      {
        model.AddCustomer(MonnitSession.CurrentCustomer);
        return (ActionResult) this.Content("Success");
      }
      // ISSUE: reference to a compiler-generated field
      if (ReportController.\u003C\u003Eo__42.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ReportController.\u003C\u003Eo__42.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (ReportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ReportController.\u003C\u003Eo__42.\u003C\u003Ep__0.Target((CallSite) ReportController.\u003C\u003Eo__42.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    return (ActionResult) this.View((object) model);
  }

  private static string ValidateParameterValueType(ReportParameter Parameter, string tempValue)
  {
    object obj = (object) null;
    try
    {
      obj = Parameter.GetValue(tempValue);
    }
    catch
    {
    }
    if (obj == null)
    {
      switch (Parameter.Type.ValueType.ToLower())
      {
        case "int":
          return " must be an integer.";
        case "long":
          return " must be an integer.";
        case "double":
          return " must be an number.";
      }
    }
    return (string) null;
  }

  private static string ValidateParameterType(ReportParameter Parameter, string tempValue)
  {
    if (MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (string) null;
    switch (Parameter.Type.Name.ToLower())
    {
      case "sensorid":
        try
        {
          if (!MonnitSession.CurrentCustomer.CanSeeSensor(Sensor.Load(Convert.ToInt64(tempValue))))
            return " - unknown sensor.";
          break;
        }
        catch
        {
          return " - unknown sensor.";
        }
      case "networkid":
        try
        {
          if (!MonnitSession.CurrentCustomer.CanSeeNetwork(Convert.ToInt64(tempValue)))
            return " - unknown network.";
          break;
        }
        catch
        {
          return " - unknown network.";
        }
      case "accountid":
        try
        {
          long int64 = Convert.ToInt64(tempValue);
          if (int64 != MonnitSession.CurrentCustomer.AccountID)
          {
            if (Account.Load(int64).RetailAccountID != MonnitSession.CurrentCustomer.AccountID)
              return " - unknown account.";
            break;
          }
          break;
        }
        catch
        {
          return " - unknown account.";
        }
    }
    return (string) null;
  }

  [Authorize]
  public ActionResult SetActive(long id, bool active)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (reportSchedule == null || !MonnitSession.IsAuthorizedForAccount(reportSchedule.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    reportSchedule.IsActive = active;
    reportSchedule.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult Delete(long id)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(reportSchedule.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    try
    {
      reportSchedule.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to delete report");
    }
  }

  [Authorize]
  public ActionResult Create() => (ActionResult) this.View();

  [Authorize]
  public ActionResult CreateReport(long? id, long? queryID)
  {
    ReportSchedule model = ReportSchedule.Load(id ?? long.MinValue);
    if (model == null)
    {
      if (!queryID.HasValue)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      model = new ReportSchedule();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.Name = "New Report";
      model.ReportQueryID = queryID ?? long.MinValue;
      model.Schedule = "1";
      model.ScheduleType = eReportScheduleType.Monthly;
      ReportSchedule reportSchedule1 = model;
      DateTime now = DateTime.Now;
      DateTime date1 = now.Date;
      reportSchedule1.StartDate = date1;
      ReportSchedule reportSchedule2 = model;
      now = DateTime.Now;
      DateTime date2 = now.Date;
      reportSchedule2.EndDate = date2;
      model.StartTime = new TimeSpan(12, 0, 0);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Details(long id)
  {
    return (ActionResult) this.PartialView((object) ReportSchedule.Load(id));
  }

  [Authorize]
  public ActionResult History(long id)
  {
    return (ActionResult) this.PartialView((object) ReportSchedule.Load(id));
  }

  [Authorize]
  public ActionResult Edit(long id)
  {
    return (ActionResult) this.PartialView((object) ReportSchedule.Load(id));
  }

  [Authorize]
  public ActionResult SendTo(long id)
  {
    return (ActionResult) this.PartialView((object) ReportSchedule.Load(id));
  }

  public System.Collections.Generic.List<ReportSchedule> GetReportList(out int totalReports)
  {
    int Class = MonnitSession.ReportListFilters.Class;
    int Status = MonnitSession.ReportListFilters.Status;
    long applicationId = MonnitSession.ReportListFilters.ApplicationID;
    string Name = MonnitSession.ReportListFilters.Name;
    System.Collections.Generic.List<ReportSchedule> source1 = ReportSchedule.LoadByAccount(MonnitSession.CurrentCustomer.AccountID);
    totalReports = source1.Count<ReportSchedule>();
    IEnumerable<ReportSchedule> source2 = source1.Where<ReportSchedule>((System.Func<ReportSchedule, bool>) (n =>
    {
      if (n.ScheduleType.ToInt() != Class && Class != int.MinValue || n.IsActive.ToInt() != Status && Status != int.MinValue)
        return false;
      return Name == "" || n.Name.ToLower().Contains(Name.ToLower());
    }));
    IEnumerable<ReportSchedule> source3;
    switch (MonnitSession.ReportListSort.Column)
    {
      case "Schedule":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, eReportScheduleType>((System.Func<ReportSchedule, eReportScheduleType>) (n => n.ScheduleType)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, eReportScheduleType>((System.Func<ReportSchedule, eReportScheduleType>) (n => n.ScheduleType));
        break;
      case "Last Sent":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, DateTime>((System.Func<ReportSchedule, DateTime>) (n => n.LastRunDate)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, DateTime>((System.Func<ReportSchedule, DateTime>) (n => n.LastRunDate));
        break;
      case "Status":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, bool>((System.Func<ReportSchedule, bool>) (n => n.IsActive)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, bool>((System.Func<ReportSchedule, bool>) (n => n.IsActive));
        break;
      case "Type":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, string>((System.Func<ReportSchedule, string>) (n => n.Report.Name)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, string>((System.Func<ReportSchedule, string>) (n => n.Report.Name));
        break;
      default:
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, string>((System.Func<ReportSchedule, string>) (n => n.Name)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, string>((System.Func<ReportSchedule, string>) (n => n.Name));
        break;
    }
    return source3.ToList<ReportSchedule>();
  }

  [Authorize]
  public ActionResult FilterStatus(string status)
  {
    try
    {
      MonnitSession.ReportListFilters.Status = string.IsNullOrEmpty(status) ? int.MinValue : status.ToInt();
    }
    catch
    {
      MonnitSession.ReportListFilters.Status = int.MinValue;
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult FilterClass(string reportClass)
  {
    try
    {
      MonnitSession.ReportListFilters.Class = string.IsNullOrEmpty(reportClass) ? int.MinValue : reportClass.ToInt();
    }
    catch
    {
      MonnitSession.ReportListFilters.Class = int.MinValue;
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult FilterName(string name)
  {
    try
    {
      MonnitSession.ReportListFilters.Name = name;
    }
    catch
    {
      MonnitSession.ReportListFilters.Name = "";
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult SortBy(string column, string direction)
  {
    MonnitSession.ReportListSort.Column = column.ToStringSafe();
    MonnitSession.ReportListSort.Direction = direction.ToStringSafe();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult DatabaseStatistics()
  {
    if (!MonnitSession.CustomerCan("Database_User"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    DateTime dateTime = DateTime.UtcNow.AddMinutes(-60.0);
    DateTime utcNow = DateTime.UtcNow;
    MonnitSession.HistoryFromDate = dateTime;
    MonnitSession.HistoryToDate = utcNow;
    System.Collections.Generic.List<iMonnit.Models.DatabaseStatistics> model = iMonnit.Models.DatabaseStatistics.CriticalCheck(true, MonnitSession.HistoryFromDate, MonnitSession.HistoryToDate);
    MonnitSession.HistoryFromDate = MonnitSession.HistoryFromDate.ToLocalTime();
    MonnitSession.HistoryToDate = MonnitSession.HistoryToDate.ToLocalTime();
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult DatabaseStats(double? minutesAgo)
  {
    if (!MonnitSession.CustomerCan("Database_User"))
      return (ActionResult) this.Content("Failed");
    System.Collections.Generic.List<iMonnit.Models.DatabaseStatistics> databaseStatisticsList = new System.Collections.Generic.List<iMonnit.Models.DatabaseStatistics>();
    System.Collections.Generic.List<iMonnit.Models.DatabaseStatistics> model;
    if (minutesAgo.HasValue)
    {
      ref double? local = ref minutesAgo;
      double? nullable = minutesAgo;
      double num1 = -1.0;
      double num2 = (nullable.HasValue ? new double?(nullable.GetValueOrDefault() * num1) : new double?()).ToDouble();
      local = new double?(num2);
      model = iMonnit.Models.DatabaseStatistics.CriticalCheck(true, DateTime.UtcNow.AddMinutes(minutesAgo ?? 60.0), DateTime.UtcNow);
    }
    else
      model = iMonnit.Models.DatabaseStatistics.CriticalCheck(true, MonnitSession.HistoryFromDate.ToUniversalTime(), MonnitSession.HistoryToDate.ToUniversalTime());
    return (ActionResult) this.PartialView("DatabaseStatList", (object) model);
  }

  [Authorize]
  public ActionResult DatabaseDetails(string tableName, string status)
  {
    if (!MonnitSession.CustomerCan("Database_User"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    // ISSUE: reference to a compiler-generated field
    if (ReportController.\u003C\u003Eo__60.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ReportController.\u003C\u003Eo__60.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (tableName), typeof (ReportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ReportController.\u003C\u003Eo__60.\u003C\u003Ep__0.Target((CallSite) ReportController.\u003C\u003Eo__60.\u003C\u003Ep__0, this.ViewBag, tableName);
    // ISSUE: reference to a compiler-generated field
    if (ReportController.\u003C\u003Eo__60.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ReportController.\u003C\u003Eo__60.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (status), typeof (ReportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ReportController.\u003C\u003Eo__60.\u003C\u003Ep__1.Target((CallSite) ReportController.\u003C\u003Eo__60.\u003C\u003Ep__1, this.ViewBag, status);
    return (ActionResult) this.View((object) iMonnit.Models.DatabaseDetails.CriticalDetails(tableName, status, MonnitSession.HistoryFromDate.ToUniversalTime(), MonnitSession.HistoryToDate.ToUniversalTime()));
  }
}
