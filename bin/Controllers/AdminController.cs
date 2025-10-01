// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.AdminController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[Authorize]
public class AdminController : ThemeController
{
  public ActionResult AcknowledgeMaint(long id)
  {
    MaintenanceWindowCustomer maintenanceWindowCustomer = MonnitSession.CurrentCustomer.MaintWindowAcked(id, eMaintenanceWindowCustomerType.Pop_Up);
    if (maintenanceWindowCustomer == null)
      maintenanceWindowCustomer = new MaintenanceWindowCustomer()
      {
        MaintenanceWindowID = id,
        CustomerID = MonnitSession.CurrentCustomer.CustomerID,
        Type = eMaintenanceWindowCustomerType.Pop_Up
      };
    maintenanceWindowCustomer.Acknowledged = true;
    maintenanceWindowCustomer.Save();
    return (ActionResult) this.Content("Success");
  }

  public void AddTimeDropDowns() => this.AddTimeDropDowns(new TimeSpan(0L), new TimeSpan(0L));

  public void AddTimeDropDowns(TimeSpan fromTime, TimeSpan endTime)
  {
    string[] items1 = new string[24]
    {
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "10",
      "11",
      "12",
      "13",
      "14",
      "15",
      "16",
      "17",
      "18",
      "19",
      "20",
      "21",
      "22",
      "23"
    };
    string[] items2 = new string[12]
    {
      "00",
      "05",
      "10",
      "15",
      "20",
      "25",
      "30",
      "35",
      "40",
      "45",
      "50",
      "55"
    };
    this.ViewData["FromHours"] = (object) new SelectList((IEnumerable) items1);
    this.ViewData["FromMinutes"] = (object) new SelectList((IEnumerable) items2, (object) fromTime.Minutes.ToString());
    this.ViewData["ToHours"] = (object) new SelectList((IEnumerable) items1);
    this.ViewData["ToMinutes"] = (object) new SelectList((IEnumerable) items2, (object) endTime.Minutes.ToString());
  }

  public ActionResult InboundDataPacket()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    this.AddTimeDropDowns();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target1 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p0 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__0;
    object viewBag1 = this.ViewBag;
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.AddDays(-7.0);
    string shortDateString1 = dateTime.ToShortDateString();
    object obj1 = target1((CallSite) p0, viewBag1, shortDateString1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target2 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p1 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__1;
    object viewBag2 = this.ViewBag;
    dateTime = DateTime.Now;
    dateTime = dateTime.AddDays(2.0);
    string shortDateString2 = dateTime.ToShortDateString();
    object obj2 = target2((CallSite) p1, viewBag2, shortDateString2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__3.\u003C\u003Ep__2, this.ViewBag, 10);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__3.\u003C\u003Ep__3, this.ViewBag, int.MinValue);
    return (ActionResult) this.View((object) new List<InboundPacket>());
  }

  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult InboundDataPacket(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime1 = collection["FromMonths"].ToDateTime();
    DateTime dateTime2 = collection["ToMonths"].ToDateTime();
    int hour1 = collection["FromHours"].ToInt();
    int hour2 = collection["ToHours"].ToInt();
    int minute1 = collection["FromMinutes"].ToInt();
    int minute2 = collection["ToMinutes"].ToInt();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, dateTime1.ToShortDateString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, dateTime2.ToShortDateString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__2, this.ViewBag, hour1.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__3, this.ViewBag, minute1.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__4.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__4, this.ViewBag, hour2.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__5.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__5, this.ViewBag, minute2.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__6.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__6, this.ViewBag, collection["GatewayID"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Search", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__7.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__7, this.ViewBag, collection["Search"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__8.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__8, this.ViewBag, collection["Count"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj10 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__9.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__9, this.ViewBag, collection["Response"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__4.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj11 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__10.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__10, this.ViewBag, collection["TimeFormat"].ToInt());
    if (this.ModelState.IsValid)
    {
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (AdminController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target1 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p14 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__13 = CallSite<Func<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, DateTime, object, object> target2 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, DateTime, object, object>> p13 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__13;
        DateTime dateTime3 = dateTime1.SetTime(hour1, minute1, 0);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__12 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target3 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p12 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__11.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__11, this.ViewBag);
        object obj13 = target3((CallSite) p12, -1, obj12);
        object obj14 = target2((CallSite) p13, dateTime3, obj13);
        DateTime fromDate = target1((CallSite) p14, obj14);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (AdminController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target4 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__18.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p18 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__18;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__17 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__17 = CallSite<Func<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, DateTime, object, object> target5 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__17.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, DateTime, object, object>> p17 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__17;
        DateTime dateTime4 = dateTime2.SetTime(hour2, minute2, 0);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__16 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target6 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__16.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p16 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__16;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__4.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__4.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = AdminController.\u003C\u003Eo__4.\u003C\u003Ep__15.Target((CallSite) AdminController.\u003C\u003Eo__4.\u003C\u003Ep__15, this.ViewBag);
        object obj16 = target6((CallSite) p16, -1, obj15);
        object obj17 = target5((CallSite) p17, dateTime4, obj16);
        DateTime toDate = target4((CallSite) p18, obj17);
        this.AddTimeDropDowns(fromDate.TimeOfDay, toDate.TimeOfDay);
        return (ActionResult) this.View((object) InboundPacket.LoadByFilter(new long?(collection["GatewayID"].ToLong()), new long?(), fromDate, toDate, collection["Count"].ToInt(), new int?(collection["Response"].ToInt())).ToList<InboundPacket>());
      }
      catch (Exception ex)
      {
        ex.Log("AdminController.InboundDataPacket");
      }
    }
    return (ActionResult) this.View();
  }

  public ActionResult OutboundDataPacket()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target1 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p0 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__0;
    object viewBag1 = this.ViewBag;
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.AddMonths(-1);
    string shortDateString1 = dateTime.ToShortDateString();
    object obj1 = target1((CallSite) p0, viewBag1, shortDateString1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target2 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p1 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__1;
    object viewBag2 = this.ViewBag;
    dateTime = DateTime.Now;
    dateTime = dateTime.AddDays(1.0);
    string shortDateString2 = dateTime.ToShortDateString();
    object obj2 = target2((CallSite) p1, viewBag2, shortDateString2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, 10);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__5.\u003C\u003Ep__3, this.ViewBag, int.MinValue);
    this.AddTimeDropDowns();
    return (ActionResult) this.View((object) new List<OutboundPacket>());
  }

  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult OutboundDataPacket(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime1 = collection["FromMonths"].ToDateTime();
    DateTime dateTime2 = collection["ToMonths"].ToDateTime();
    int hour1 = collection["FromHours"].ToInt();
    int hour2 = collection["ToHours"].ToInt();
    int minute1 = collection["FromMinutes"].ToInt();
    int minute2 = collection["ToMinutes"].ToInt();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__0, this.ViewBag, dateTime1.ToShortDateString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__1, this.ViewBag, dateTime2.ToShortDateString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__2, this.ViewBag, hour1.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__3, this.ViewBag, minute1.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__4.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__4, this.ViewBag, hour2.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__5.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__5, this.ViewBag, minute2.ToString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__6.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__6, this.ViewBag, collection["GatewayID"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Search", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__7.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__7, this.ViewBag, collection["Search"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__8.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__8, this.ViewBag, collection["Count"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj10 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__9.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__9, this.ViewBag, collection["Response"]);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__6.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj11 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__10.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__10, this.ViewBag, collection["TimeFormat"].ToInt());
    if (this.ModelState.IsValid)
    {
      try
      {
        if (dateTime2.Hour == 0 && dateTime2.Minute == 0)
        {
          try
          {
            dateTime2.SetDate(dateTime2.Year, dateTime2.Month, dateTime2.Day + 1);
          }
          catch (Exception ex1)
          {
            ex1.Log("AdminController.OutboundDataPacket | to.SetDate(to.Year, to.Month, to.Day + 1)");
            try
            {
              dateTime2.SetDate(dateTime2.Year, dateTime2.Month + 1, 1);
            }
            catch (Exception ex2)
            {
              ex1.Log("AdminController.OutboundDataPacket | to.SetDate(to.Year, to.Month + 1, 1)");
              dateTime2.SetDate(dateTime2.Year + 1, 1, 1);
            }
          }
        }
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (AdminController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target1 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p14 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__13 = CallSite<Func<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, DateTime, object, object> target2 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, DateTime, object, object>> p13 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__13;
        DateTime dateTime3 = dateTime1.SetTime(hour1, minute1, 0);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__12 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target3 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p12 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__11.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__11, this.ViewBag);
        object obj13 = target3((CallSite) p12, -1, obj12);
        object obj14 = target2((CallSite) p13, dateTime3, obj13);
        DateTime fromDate = target1((CallSite) p14, obj14);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (AdminController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target4 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__18.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p18 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__18;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__17 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__17 = CallSite<Func<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, DateTime, object, object> target5 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__17.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, DateTime, object, object>> p17 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__17;
        DateTime dateTime4 = dateTime2.SetTime(hour2, minute2, 0);
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__16 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target6 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__16.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p16 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__16;
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__6.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__6.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = AdminController.\u003C\u003Eo__6.\u003C\u003Ep__15.Target((CallSite) AdminController.\u003C\u003Eo__6.\u003C\u003Ep__15, this.ViewBag);
        object obj16 = target6((CallSite) p16, -1, obj15);
        object obj17 = target5((CallSite) p17, dateTime4, obj16);
        dateTime2 = target4((CallSite) p18, obj17);
        this.AddTimeDropDowns();
        return (ActionResult) this.View((object) OutboundPacket.LoadByFilter(new long?(collection["GatewayID"].ToLong()), collection["Search"].ToString(), fromDate, dateTime2, collection["Count"].ToInt(), new int?(collection["Response"].ToInt())).ToList<OutboundPacket>());
      }
      catch (Exception ex)
      {
        ex.Log("AdminController.OutboundDataPacket");
      }
    }
    return (ActionResult) this.View();
  }

  private bool IsEnterprise
  {
    get
    {
      return this.ProgramLevel() != eProgramLevel.NKR && this.ProgramLevel() != eProgramLevel.EnterpriseUnlimited;
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeleteAccountTheme(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    try
    {
      Monnit.AccountTheme.Load(id).Delete();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "CreateChangeAccountTheme"
      });
    }
    catch (Exception ex)
    {
      ex.Log($"AdminController.DeleteAccountTheme | Unable to Delete this Theme | AccountThemeID = {id}");
      return (ActionResult) this.Content("Unable to Delete this Theme");
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AccountTheme(long? id, FormCollection collection)
  {
    Monnit.AccountTheme accountTheme = Monnit.AccountTheme.Load(id.ToLong());
    if (accountTheme != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (AdminController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AdminController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<AccountThemeContact>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountThemeContactList", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AdminController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, AccountThemeContact.LoadByAccountThemeID(accountTheme.AccountThemeID));
    }
    Monnit.AccountTheme model = Monnit.AccountTheme.Load(id ?? long.MinValue) ?? new Monnit.AccountTheme();
    try
    {
      if (string.IsNullOrEmpty(collection["SMTPFriendlyName"].ToString()))
        this.ModelState.AddModelError("SMTPFriendlyName", "Required");
      if (string.IsNullOrEmpty(collection["SMTPDefaultFrom"].ToString()))
        this.ModelState.AddModelError("SMTPDefaultFrom", "Required");
      if (string.IsNullOrWhiteSpace(collection["WhiteLabelReseller"]))
      {
        if (string.IsNullOrEmpty(collection["Domain"].ToString()))
          this.ModelState.AddModelError("Domain", "Required");
        if (!string.IsNullOrEmpty(collection["SMTP"].ToString()))
        {
          if (string.IsNullOrEmpty(collection["SMTPPasswordPlainText"].ToString()))
            this.ModelState.AddModelError("SMTPPasswordPlainText", "Required");
          if (collection["SMTPPort"].ToInt() < 1 || collection["SMTPPort"].ToInt() > 65000 || string.IsNullOrEmpty(collection["SMTPPort"].ToString()))
            this.ModelState.AddModelError("SMTPPort", "Required");
          if (string.IsNullOrEmpty(collection["SMTPUser"].ToString()))
            this.ModelState.AddModelError("SMTPUser", "Required");
        }
        if (string.IsNullOrEmpty(collection["Theme"].ToString()))
          this.ModelState.AddModelError("Theme", "Required");
      }
      if (this.ModelState.IsValid)
      {
        this.UpdateModel<Monnit.AccountTheme>(model);
        model.Save();
        // ISSUE: reference to a compiler-generated field
        if (AdminController.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AdminController.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = AdminController.\u003C\u003Eo__10.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__10.\u003C\u003Ep__1, this.ViewBag, "Success");
        return (ActionResult) this.View((object) model);
      }
      // ISSUE: reference to a compiler-generated field
      if (AdminController.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AdminController.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = AdminController.\u003C\u003Eo__10.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__10.\u003C\u003Ep__2, this.ViewBag, "");
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      string str = !id.HasValue ? "null" : id.ToString();
      ex.Log($"AdminController.AccountTheme | Failed | AccountThemeID = {id}");
      // ISSUE: reference to a compiler-generated field
      if (AdminController.\u003C\u003Eo__10.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AdminController.\u003C\u003Eo__10.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AdminController.\u003C\u003Eo__10.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__10.\u003C\u003Ep__3, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) model);
    }
  }

  [AuthorizeDefault]
  public ActionResult SystemIcons(long? id)
  {
    if (!MonnitSession.CustomerCan("SVG_Icon_Admin"))
      return this.PermissionError(methodName: nameof (SystemIcons), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 391);
    long num = id ?? MonnitSession.CurrentTheme.AccountThemeID;
    Monnit.AccountTheme accountTheme = Monnit.AccountTheme.Load(num);
    if (accountTheme == null || accountTheme.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return this.PermissionError(methodName: nameof (SystemIcons), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 397);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "accountThemeID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__11.\u003C\u003Ep__0, this.ViewBag, num);
    return (ActionResult) this.View((object) SVGIcon.LoadForTheme(num));
  }

  [AuthorizeDefault]
  public ActionResult SystemStatus(string keyWord)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return this.PermissionError(methodName: nameof (SystemStatus), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 408);
    IEnumerable<string> source = ConfigData.LoadAll().OrderBy<ConfigData, string>((System.Func<ConfigData, string>) (c => c.Key)).Select<ConfigData, string>((System.Func<ConfigData, string>) (c => c.Key));
    if (!string.IsNullOrEmpty(keyWord))
      source = source.Where<string>((System.Func<string, bool>) (k => k.ToLower().Contains(keyWord.ToLower())));
    Dictionary<string, string> model = new Dictionary<string, string>();
    foreach (string key in source)
    {
      if (!string.IsNullOrEmpty(key) && !model.ContainsKey(key))
        model.Add(key, ConfigData.AppSettings(key));
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public static IEnumerable<SelectListItem> GetSystemIconsDropdownFilter()
  {
    IEnumerable<SelectListItem> selectListItems = SettingsController.GetSystemIconEnumDict().Values.Select<string, SelectListItem>((System.Func<string, SelectListItem>) (x => new SelectListItem()
    {
      Value = x,
      Text = x
    })).Prepend<SelectListItem>(new SelectListItem()
    {
      Value = "",
      Text = "All Icons"
    });
    return (IEnumerable<SelectListItem>) new SelectList((IEnumerable) selectListItems, "Value", "Text", (object) selectListItems.Single<SelectListItem>((System.Func<SelectListItem, bool>) (s => s.Value == "")).Value);
  }

  [AuthorizeDefault]
  public static IEnumerable<SelectListItem> GetSystemIconsDropdownFilterAlt()
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>()
    {
      {
        "",
        "All Icons"
      }
    }.Concat<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) SettingsController.GetSystemIconEnumDict().Values.ToDictionary<string, string, string>((System.Func<string, string>) (x => x), (System.Func<string, string>) (x => x))).ToDictionary<KeyValuePair<string, string>, string, string>((System.Func<KeyValuePair<string, string>, string>) (kvp => kvp.Key), (System.Func<KeyValuePair<string, string>, string>) (kvp => kvp.Value));
    return (IEnumerable<SelectListItem>) new SelectList((IEnumerable) dictionary, "Key", "Value", (object) dictionary[""]);
  }

  [AuthorizeDefault]
  public ActionResult SystemIconFilter(long? id, string categoryFilter, string nameFilter)
  {
    if (!MonnitSession.CustomerCan("SVG_Icon_Admin"))
      return this.PermissionError(methodName: nameof (SystemIconFilter), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 465);
    long num = id ?? MonnitSession.CurrentTheme.AccountThemeID;
    Monnit.AccountTheme accountTheme = Monnit.AccountTheme.Load(num);
    if (accountTheme == null || accountTheme.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return this.PermissionError(methodName: nameof (SystemIconFilter), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 471);
    List<SVGIcon> svgIconList = SVGIcon.LoadForTheme(num);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalIcons", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AdminController.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, svgIconList.Count);
    if (!string.IsNullOrEmpty(categoryFilter))
      svgIconList = svgIconList.Where<SVGIcon>((System.Func<SVGIcon, bool>) (i => i.Category == categoryFilter)).ToList<SVGIcon>();
    if (!string.IsNullOrEmpty(nameFilter))
      svgIconList = svgIconList.Where<SVGIcon>((System.Func<SVGIcon, bool>) (i => i.Name.Contains(nameFilter) || i.ImageKey.Contains(nameFilter))).ToList<SVGIcon>();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredIcons", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AdminController.\u003C\u003Eo__15.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__15.\u003C\u003Ep__1, this.ViewBag, svgIconList.Count);
    return (ActionResult) this.PartialView((object) svgIconList);
  }

  [AuthorizeDefault]
  public ActionResult SystemIconEdit(long id)
  {
    return !MonnitSession.CustomerCan("SVG_Icon_Admin") ? this.PermissionError(methodName: nameof (SystemIconEdit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AdminController.cs", sourceLineNumber: 492) : (ActionResult) this.View((object) SVGIcon.Load(id));
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateInput(false)]
  public ActionResult SystemIconEdit(FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("SVG_Icon_Admin"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      SVGIcon model = SVGIcon.Load(collection["IconID"].ToLong());
      if (this.ModelState.IsValid)
      {
        model.Name = collection["Name"];
        model.ImageKey = collection["ImageKey"];
        model.Category = collection["Category"];
        model.HTMLCode = collection["HTMLcode"];
        model.IsDefault = collection["IsDefault"].ToBool();
        model.Save();
        this.ViewData["Result"] = (object) "Success!";
      }
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      this.ViewData["Result"] = (object) ("Error: " + ex.Message);
    }
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult ThemePreference(long? id)
  {
    if (!id.HasValue)
      id = new long?(MonnitSession.CurrentCustomer.Account.GetThemeID());
    List<AccountThemePreferenceTypeLink> model = AccountThemePreferenceTypeLink.LoadByAccountThemeID(id ?? long.MinValue);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "accountThemeID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__18.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  public ActionResult EditThemePreference(long id, FormCollection collection)
  {
    try
    {
      foreach (PreferenceType preferenceType in PreferenceType.LoadAll())
      {
        AccountThemePreferenceTypeLink preferenceTypeLink = AccountThemePreferenceTypeLink.LoadByPreferenceTypeIDandAccountThemeID(preferenceType.PreferenceTypeID, id);
        if (preferenceTypeLink == null)
        {
          preferenceTypeLink = new AccountThemePreferenceTypeLink();
          preferenceTypeLink.PreferenceTypeID = preferenceType.PreferenceTypeID;
          preferenceTypeLink.AccountThemeID = id;
        }
        preferenceTypeLink.AccountCanOverride = !string.IsNullOrEmpty(collection[preferenceType.Name + "_AccountCan"]) && collection[preferenceType.Name + "_AccountCan"].ToLower() == "on";
        preferenceTypeLink.CustomerCanOverride = !string.IsNullOrEmpty(collection[preferenceType.Name + "_CustomerCan"]) && collection[preferenceType.Name + "_CustomerCan"].ToLower() == "on";
        preferenceTypeLink.DefaultValue = string.IsNullOrEmpty(collection[preferenceType.Name + "_DefaultVal"]) ? preferenceType.DefaultValue : collection[preferenceType.Name + "_DefaultVal"];
        preferenceTypeLink.Save();
      }
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult AuthorizeNet()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0 && !this.IsEnterprise)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__20.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/AuthorizeNet.aspx");
  }

  public ActionResult AutomatedEmails()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__21.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/AutomatedEmails.aspx");
  }

  public ActionResult SMTPSendEmail()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__22.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/SMTPSendEmail.aspx");
  }

  public ActionResult ExceptionLogging()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__23.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/ExceptionLogging.aspx");
  }

  public ActionResult CellularUsage()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__24.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/CellularUsage.aspx");
  }

  public ActionResult Administrative()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View("~/Views/Admin/SiteConfigs/Administrative.aspx");
  }

  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult EditSiteConfigs(FormCollection collection, string formName)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      if (formName == "SMTPSendEmail")
      {
        Monnit.AccountTheme currentTheme = MonnitSession.CurrentTheme;
        currentTheme.SMTP = collection["SMTP"];
        currentTheme.SMTPPort = collection["SMTPPort"].ToInt();
        currentTheme.SMTPUser = collection["SMTPUser"];
        currentTheme.SMTPPassword = MonnitSession.UseEncryption ? collection["SMTPPassword"].Encrypt() : collection["SMTPPassword"];
        currentTheme.SMTPDefaultFrom = collection["SMTPDefaultFrom"];
        currentTheme.SMTPFriendlyName = collection["SMTPFriendlyName"];
        currentTheme.oldSMTPFriendlyName = collection["SMTPFriendlyName"];
        currentTheme.SMTPUseSSL = collection["SMTPUseSSL"].ToBool();
        currentTheme.SMTPReturnPath = collection["SMTPReturnPath"];
        currentTheme.Save();
      }
      else
      {
        foreach (string allKey in collection.AllKeys)
        {
          if (!string.IsNullOrEmpty(allKey) && (allKey != nameof (formName) || allKey != "__RequestVerificationToken"))
            ConfigData.SetAppSettings(allKey, collection[allKey]);
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (AdminController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AdminController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AdminController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__26.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (AdminController.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AdminController.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AdminController.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__26.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    switch (formName)
    {
      case "Administrative":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/Administrative.aspx");
      case "AuthorizeNet":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/AuthorizeNet.aspx");
      case "AutomatedEmails":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/AutomatedEmails.aspx");
      case "CellularUsage":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/CellularUsage.aspx");
      case "ExceptionLogging":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/ExceptionLogging.aspx");
      case "SMTPSendEmail":
        return (ActionResult) this.View("~/Views/Admin/SiteConfigs/SMTPSendEmail.aspx");
      default:
        return (ActionResult) this.View();
    }
  }

  public ActionResult Settings() => (ActionResult) this.View();

  public ActionResult EditSensorApplication(long? ApplicationID)
  {
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__28.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__28.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MonnitAppID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__28.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__28.\u003C\u003Ep__0, this.ViewBag, ApplicationID);
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) SensorApplication.LoadAll().Where<SensorApplication>((System.Func<SensorApplication, bool>) (sa =>
    {
      long applicationId = sa.ApplicationID;
      long? nullable = ApplicationID;
      long valueOrDefault = nullable.GetValueOrDefault();
      return applicationId == valueOrDefault & nullable.HasValue;
    })).ToList<SensorApplication>());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditSensorApplication(long ApplicationID, long[] SensorAppData)
  {
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__29.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__29.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MonnitAppID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__29.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__29.\u003C\u003Ep__0, this.ViewBag, ApplicationID);
    ResellerToSensorApplication.DeleteByResellerID(MonnitSession.CurrentCustomer.AccountID, ApplicationID);
    if (SensorAppData != null)
    {
      for (int index = 0; index < SensorAppData.Length; ++index)
        new ResellerToSensorApplication()
        {
          ResellerAccountID = MonnitSession.CurrentCustomer.AccountID,
          SensorApplicationID = SensorAppData[index]
        }.Save();
    }
    return (ActionResult) this.View(nameof (EditSensorApplication), (object) SensorApplication.LoadAll().Where<SensorApplication>((System.Func<SensorApplication, bool>) (sa => sa.ApplicationID == ApplicationID)).ToList<SensorApplication>());
  }

  public ActionResult EditSMSCarriersList(long? currentThemeAccountID)
  {
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (currentThemeAccountID), typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__30.\u003C\u003Ep__0, this.ViewBag, currentThemeAccountID);
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) SMSAccount.SMSList(currentThemeAccountID.GetValueOrDefault()).ToList<SMSCarrier>());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditSMSCarriersList(long currentThemeAccountID, FormCollection collection)
  {
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__31.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__31.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (currentThemeAccountID), typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AdminController.\u003C\u003Eo__31.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__31.\u003C\u003Ep__0, this.ViewBag, currentThemeAccountID);
    SMSAccount.DeleteByAccountID(MonnitSession.CurrentTheme.AccountThemeID);
    List<long> longList = new List<long>();
    foreach (string allKey in collection.AllKeys)
    {
      if (allKey.Contains("SMSCarrierID_"))
        longList.Add(collection[allKey].ToLong());
    }
    if (longList.Count > 1)
    {
      for (int index = 0; index < longList.Count; ++index)
        new SMSAccount()
        {
          AccountThemeID = MonnitSession.CurrentTheme.AccountThemeID,
          SMSAccountID = MonnitSession.CurrentTheme.AccountID,
          SMSCarrierID = longList[index]
        }.Save();
    }
    return (ActionResult) this.View(nameof (EditSMSCarriersList), (object) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountID).ToList<SMSCarrier>());
  }

  public ActionResult ReleaseNoteWindows()
  {
    return (ActionResult) this.View((object) (List<AccountThemeReleaseNoteModel>) this.TempData["rnList"]);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ReleaseNoteWindows(FormCollection collection)
  {
    foreach (string allKey in collection.AllKeys)
      MonnitSession.CurrentCustomer.AcknowledgeReleaseNote(collection[allKey].ToLong(), MonnitSession.CurrentCustomer.CustomerID, true);
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  public ActionResult ReleaseNote(DateTime? releaseDate)
  {
    try
    {
      if (MonnitSession.CurrentCustomer.AccountID != MonnitSession.CurrentTheme.AccountID && !MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      DateTime dattime = releaseDate ?? DateTime.MinValue;
      List<Monnit.ReleaseNote> releaseNoteList = !(dattime != DateTime.MinValue) ? Monnit.ReleaseNote.LoadAll() : Monnit.ReleaseNote.LoadAll().Where<Monnit.ReleaseNote>((System.Func<Monnit.ReleaseNote, bool>) (rd => rd.ReleaseDate == dattime)).ToList<Monnit.ReleaseNote>();
      return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.View((object) releaseNoteList.Where<Monnit.ReleaseNote>((System.Func<Monnit.ReleaseNote, bool>) (r => !r.IsDeleted)).ToList<Monnit.ReleaseNote>()) : (ActionResult) this.View((object) releaseNoteList);
    }
    catch
    {
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  public ActionResult DeleteAccountThemeToReleaseNote(long releasenoteid, long accountthemeid)
  {
    foreach (BaseDBObject baseDbObject in AccountThemeReleaseNoteLink.LoadByAccountThemeIDAndReleaseNoteID(accountthemeid, releasenoteid))
      baseDbObject.Delete();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "ReleaseNote"
    });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeleteMaintenanceLink(long maintenanceID, long accountthemeID)
  {
    try
    {
      AccountThemeMaintenanceLink.LoadByAccountThemeIDAndMaintenanceID(accountthemeID, maintenanceID).Delete();
      return (ActionResult) this.Content("Delete link Success!");
    }
    catch
    {
      return (ActionResult) this.Content("Delete link Failed!");
    }
  }

  [Authorize]
  public ActionResult MassEmail()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.RedirectPermanent("/Overview");
    List<AdminController.MassEmailContact> model = new List<AdminController.MassEmailContact>();
    foreach (Monnit.AccountTheme at in Monnit.AccountTheme.LoadAll().Where<Monnit.AccountTheme>((System.Func<Monnit.AccountTheme, bool>) (c => c.IsActive)))
    {
      AdminController.MassEmailContact massEmailContact = new AdminController.MassEmailContact(at);
      model.Add(massEmailContact);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult MassEmailContactList(long ThemeID)
  {
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.RedirectPermanent("/Overview") : (ActionResult) this.PartialView((object) new AdminController.MassEmailContact(Monnit.AccountTheme.Load(ThemeID)));
  }

  [HttpPost]
  public ActionResult MassEmailContent(string flag)
  {
    eEmailTemplateFlag flag1 = (eEmailTemplateFlag) Enum.Parse(typeof (eEmailTemplateFlag), flag, true);
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.RedirectPermanent("/Overview") : (ActionResult) this.PartialView(nameof (MassEmailContent), (object) EmailTemplate.LoadByAccountAndFlag(MonnitSession.CurrentCustomer.AccountID, flag1));
  }

  [HttpPost]
  public ActionResult MassEmailBody()
  {
    return (ActionResult) this.PartialView((object) new EmailTemplate());
  }

  [HttpPost]
  [ValidateInput(false)]
  public ActionResult buildEmail(string Subject, string EditorData, string IDs)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.Content("Unauthorized");
    IDs = IDs.TrimEnd('|');
    string str = IDs;
    char[] chArray = new char[1]{ '|' };
    foreach (string o in str.Split(chArray))
    {
      try
      {
        SystemEmail systemEmail1 = new SystemEmail();
        SystemEmailContent systemEmailContent = new SystemEmailContent();
        systemEmailContent.ModifiedDate = DateTime.UtcNow;
        systemEmailContent.Subject = Subject;
        systemEmailContent.Body = EditorData.SanitizeButAllowSomeHTMLEvent();
        systemEmailContent.TemplateFlag = "Generic";
        systemEmailContent.Save();
        List<AccountThemeContact> accountThemeContactList = AccountThemeContact.LoadByAccountThemeID((long) o.ToInt());
        if (accountThemeContactList.Count > 0)
        {
          foreach (AccountThemeContact accountThemeContact in accountThemeContactList)
          {
            SystemEmail systemEmail2 = systemEmail1;
            systemEmail2.ToAddress = $"{systemEmail2.ToAddress}{accountThemeContact.Email}; ";
          }
          systemEmail1.CreateDate = DateTime.UtcNow;
          systemEmail1.Status = "New";
          systemEmail1.DoRetry = true;
          systemEmail1.RetryCount = 0;
          systemEmail1.SystemEmailContentID = systemEmailContent.SystemEmailContentID;
          systemEmail1.Save();
        }
      }
      catch (Exception ex)
      {
        ex.Log("AccountControler.buildEmail  Failed to add to SystemEmail");
        return (ActionResult) this.Content("Failed");
      }
    }
    return (ActionResult) this.Content("Success: Email Queued");
  }

  public ActionResult SensorMessageAuditLookUp()
  {
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DatefromString", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target1 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__0.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p0 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__0;
    object viewBag1 = this.ViewBag;
    DateTime dateTime = DateTime.Now;
    dateTime = dateTime.AddDays(-7.0);
    string shortDateString1 = dateTime.ToShortDateString();
    object obj1 = target1((CallSite) p0, viewBag1, shortDateString1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DatetoString", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string, object> target2 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string, object>> p1 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__1;
    object viewBag2 = this.ViewBag;
    dateTime = DateTime.Now;
    dateTime = dateTime.AddDays(2.0);
    string shortDateString2 = dateTime.ToShortDateString();
    object obj2 = target2((CallSite) p1, viewBag2, shortDateString2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__2, this.ViewBag, 0);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__3, this.ViewBag, 0);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__4.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__4, this.ViewBag, 0);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__5.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__5, this.ViewBag, 0);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__6.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__6, this.ViewBag, 10);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__7.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__7, this.ViewBag, int.MinValue);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__44.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__44.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = AdminController.\u003C\u003Eo__44.\u003C\u003Ep__8.Target((CallSite) AdminController.\u003C\u003Eo__44.\u003C\u003Ep__8, this.ViewBag, 0);
    return (ActionResult) this.View((object) new List<SensorMessageAudit.SensorMessageAuditCountModel>());
  }

  public ActionResult AuditDetails(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    string eventType)
  {
    List<SensorMessageAudit.SensorMessageAuditDataModel> source = new List<SensorMessageAudit.SensorMessageAuditDataModel>();
    switch (eventType.ToLower())
    {
      case "appcommandresponse":
      case "join":
      case "parent":
      case "readresponse":
      case "writeresponse":
        using (List<SensorMessageAudit>.Enumerator enumerator = SensorMessageAudit.Search(sensorID, fromDate, toDate, eventType).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SensorMessageAudit current = enumerator.Current;
            source.Add(SensorMessageAudit.ConvertData(current));
          }
          break;
        }
      case "deliveredlate":
        using (List<DataMessage>.Enumerator enumerator = DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, 10000, new Guid?()).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            DataMessage current = enumerator.Current;
            if (current.LinkQuality == 0 || current.SignalStrength == 0)
              source.Add(SensorMessageAudit.ConvertOtherData(current, eventType));
          }
          break;
        }
      case "missed":
        DataTable dataTable = DataMessage.LoadMissedBySensorAndDateRange(sensorID, fromDate, toDate);
        if (dataTable.Rows.Count > 0)
        {
          IEnumerator enumerator = dataTable.Rows.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              DataRow current = (DataRow) enumerator.Current;
              MissedSensorMessages missedSensorMessages = new MissedSensorMessages(current);
              SensorMessageAudit.SensorMessageAuditDataModel messageAuditDataModel1 = new SensorMessageAudit.SensorMessageAuditDataModel();
              DataMessage dataMessage = DataMessage.LoadLastBefore(sensorID, current["MissedMessageDate"].ToDateTime());
              if (dataMessage.GatewayID != long.MinValue)
              {
                messageAuditDataModel1.SensorID = sensorID;
                messageAuditDataModel1.GatewayID = dataMessage.GatewayID;
                messageAuditDataModel1.MessageDate = current["MissedMessageDate"].ToDateTime();
                messageAuditDataModel1.Success = "N/A";
                SensorMessageAudit.SensorMessageAuditDataModel messageAuditDataModel2 = messageAuditDataModel1;
                string[] strArray = new string[7];
                strArray[0] = eventType;
                strArray[1] = " - Time Between Messages- Days:";
                int num = missedSensorMessages.Days;
                strArray[2] = num.ToString();
                strArray[3] = " Hours:";
                num = missedSensorMessages.Hours;
                strArray[4] = num.ToString();
                strArray[5] = " Minutes:";
                num = missedSensorMessages.Minutes;
                strArray[6] = num.ToString();
                string str = string.Concat(strArray);
                messageAuditDataModel2.MessageEvent = str;
                source.Add(messageAuditDataModel1);
              }
            }
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        else
          break;
      case "restart":
        DateTime dateTime1 = DateTime.MinValue;
        List<DataMessage> dataMessageList = DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, 10000, new Guid?());
        DataMessage dataMessage1 = DataMessage.LoadLastBefore(sensorID, fromDate);
        if (dataMessage1 != null)
          dateTime1 = dataMessage1.MessageDate;
        int num1 = 0;
        using (List<DataMessage>.Enumerator enumerator = dataMessageList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            DataMessage current = enumerator.Current;
            if (current.MessageDate.AddMinutes(-2.0) > dateTime1)
              num1 = 0;
            if ((current.State & 1) == 1)
            {
              if (num1 == 0)
                source.Add(SensorMessageAudit.ConvertOtherData(current, eventType));
              ++num1;
              if (num1 > 4)
                num1 = 0;
            }
            else
              num1 = 0;
            dateTime1 = current.MessageDate;
          }
          break;
        }
      case "timeshift":
        DateTime dateTime2 = DateTime.MinValue;
        List<DataMessage> list = DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, 10000, new Guid?()).OrderByDescending<DataMessage, DateTime>((System.Func<DataMessage, DateTime>) (x => x.MessageDate)).ToList<DataMessage>();
        DataMessage dataMessage2 = DataMessage.LoadLastBefore(sensorID, fromDate);
        if (dataMessage2 != null)
          dateTime2 = dataMessage2.MessageDate;
        using (List<DataMessage>.Enumerator enumerator = list.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            DataMessage current = enumerator.Current;
            Sensor sensor = Sensor.Load(current.SensorID);
            if (sensor.GenerationType.ToUpper().Contains("GEN1"))
            {
              if (Math.Abs((current.MessageDate.AddMinutes(sensor.ReportInterval) - dateTime2).TotalSeconds) > 4.0)
                source.Add(SensorMessageAudit.ConvertOtherData(current, eventType));
            }
            else if (sensor.GenerationType.ToUpper().Contains("GEN2") && Math.Abs((current.MessageDate.AddMinutes(sensor.ReportInterval) - dateTime2).TotalSeconds) > 2.0)
              source.Add(SensorMessageAudit.ConvertOtherData(current, eventType));
            dateTime2 = current.MessageDate;
          }
          break;
        }
    }
    return (ActionResult) this.PartialView((object) source.OrderBy<SensorMessageAudit.SensorMessageAuditDataModel, DateTime>((System.Func<SensorMessageAudit.SensorMessageAuditDataModel, DateTime>) (x => x.MessageDate)).ToList<SensorMessageAudit.SensorMessageAuditDataModel>());
  }

  [HttpPost]
  public ActionResult SensorMessageAuditLookUp(FormCollection collection)
  {
    DateTime dateTime1 = collection["FromMonths"].ToDateTime();
    DateTime dateTime2 = collection["ToMonths"].ToDateTime();
    int hour1 = collection["FromHours"].ToInt();
    int hour2 = collection["ToHours"].ToInt();
    int minute1 = collection["FromMinutes"].ToInt();
    int minute2 = collection["ToMinutes"].ToInt();
    long sensorID = collection["SensorID"].ToLong();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DatefromString", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, dateTime1.ToShortDateString());
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DatetoString", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__1.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__1, this.ViewBag, dateTime2.ToShortDateString());
    DateTime dateTime3 = dateTime1.SetTime(hour1, minute1, 0);
    dateTime1 = dateTime3.ToUniversalTime();
    dateTime3 = dateTime2.SetTime(hour2, minute2, 0);
    dateTime2 = dateTime3.ToUniversalTime();
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, DateTime, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__2.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__2, this.ViewBag, dateTime1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, DateTime, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__3.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__3, this.ViewBag, dateTime2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__4.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__4, this.ViewBag, hour1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "fromMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__5.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__5, this.ViewBag, minute1);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toHours", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__6.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__6, this.ViewBag, hour2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "toMinutes", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__7.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__7, this.ViewBag, minute2);
    // ISSUE: reference to a compiler-generated field
    if (AdminController.\u003C\u003Eo__46.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AdminController.\u003C\u003Eo__46.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorID", typeof (AdminController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = AdminController.\u003C\u003Eo__46.\u003C\u003Ep__8.Target((CallSite) AdminController.\u003C\u003Eo__46.\u003C\u003Ep__8, this.ViewBag, sensorID);
    if (this.ModelState.IsValid)
    {
      try
      {
        List<SensorMessageAudit.SensorMessageAuditCountModel> counts = SensorMessageAudit.GetCounts(sensorID, dateTime1, dateTime2);
        foreach (SensorMessageAudit.SensorMessageAuditCountModel messageAuditCountModel in SensorMessageAudit.GetOtherEventCount(sensorID, dateTime1, dateTime2))
          counts.Add(messageAuditCountModel);
        return (ActionResult) this.View((object) counts);
      }
      catch (Exception ex)
      {
        ex.Log("AdminController.SensorMessageAuditLookUp");
      }
    }
    return (ActionResult) this.View();
  }

  public ActionResult AcknowledgePopupNotice(
    long customerID,
    long accountID,
    int popupNoticeType,
    bool ignoreVersion)
  {
    try
    {
      if (!Enum.IsDefined(typeof (ePopupNoticeType), (object) popupNoticeType))
        return (ActionResult) this.Content("Invalid PopupNoticeType");
      ePopupNoticeType popupNoticeType1 = (ePopupNoticeType) Enum.ToObject(typeof (ePopupNoticeType), popupNoticeType);
      if (ignoreVersion)
      {
        switch (popupNoticeType1)
        {
          case ePopupNoticeType.GatewayFirmwareUpdate:
            using (List<Gateway>.Enumerator enumerator = MonnitSession.CachedUpdateableGateways(accountID).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Gateway current = enumerator.Current;
                PopupNoticeRecord popupNoticeRecord1 = PopupNoticeRecord.LoadPopupNoticeRecordByCustomerIDAccountIDTypeSKUAndFirmwareVersion(customerID, accountID, popupNoticeType1, current.SKU, current.GatewayFirmwareVersion);
                popupNoticeRecord1.DateLastSeen = DateTime.Now;
                popupNoticeRecord1.Save();
                PopupNoticeRecord popupNoticeRecord2 = PopupNoticeRecord.LoadPopupNoticeRecordByCustomerIDAccountIDTypeSKUAndFirmwareVersion(customerID, accountID, popupNoticeType1, current.SKU, current.APNFirmwareVersion);
                popupNoticeRecord2.DateLastSeen = DateTime.Now;
                popupNoticeRecord2.Save();
              }
              break;
            }
          case ePopupNoticeType.SensorFirmwareUpdate:
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (List<Sensor>.Enumerator enumerator = MonnitSession.CachedOTASuiteSensors(accountID).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Sensor current = enumerator.Current;
                PopupNoticeRecord popupNoticeRecord = PopupNoticeRecord.LoadPopupNoticeRecordByCustomerIDAccountIDTypeSKUAndFirmwareVersion(customerID, accountID, popupNoticeType1, current.SKU, current.FirmwareVersion);
                popupNoticeRecord.DateLastSeen = DateTime.Now;
                popupNoticeRecord.Save();
              }
              break;
            }
        }
      }
      else
      {
        PopupNoticeRecord popupNoticeRecord = PopupNoticeRecord.LoadPopupNoticeRecordByCustomerIDAccountIDAndType(customerID, accountID, popupNoticeType1);
        popupNoticeRecord.DateLastSeen = DateTime.Now;
        popupNoticeRecord.Save();
      }
      MonnitSession.PopupNoticeRecords = (List<PopupNoticeRecord>) null;
      MonnitSession.CurrentCustomer.ShowPopupNotice = false;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log();
      return (ActionResult) this.Content(ex.Message);
    }
  }

  public class MassEmailContact
  {
    private List<AccountThemeContact> _Contacts;

    public MassEmailContact()
    {
    }

    public MassEmailContact(Monnit.AccountTheme at)
    {
      AccountThemeContact accountThemeContact = AccountThemeContact.LoadByAccountThemeID(at.AccountThemeID).FirstOrDefault<AccountThemeContact>();
      this.AccountID = at.AccountID;
      this.AccountThemeID = at.AccountThemeID;
      this.AccountName = at.Theme;
      if (accountThemeContact == null)
        return;
      this.ContactName = $"{accountThemeContact.FirstName} {accountThemeContact.LastName}";
      this.ContactEmail = accountThemeContact.Email;
    }

    [DBProp("AccountID")]
    public long AccountID { get; set; }

    [DBProp("AccountThemeID")]
    public long AccountThemeID { get; set; }

    [DBProp("AccountName")]
    public string AccountName { get; set; }

    [DBProp("ContactName")]
    public string ContactName { get; set; }

    [DBProp("ContactEmail")]
    public string ContactEmail { get; set; }

    public List<AccountThemeContact> Contacts
    {
      get
      {
        if (this._Contacts == null)
          this._Contacts = AccountThemeContact.LoadByAccountThemeID(this.AccountThemeID);
        return this._Contacts;
      }
    }
  }

  public class MassEmailContentModel
  {
    public MassEmailContentModel()
    {
    }

    public MassEmailContentModel(string subject, string body)
    {
      this.Subject = subject;
      this.Body = body;
    }

    [DBProp("Subject")]
    public string Subject { get; set; }

    [DBProp("Body")]
    public string Body { get; set; }
  }
}
