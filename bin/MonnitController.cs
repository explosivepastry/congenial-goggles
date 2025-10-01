// Decompiled with JetBrains decompiler
// Type: iMonnit.MonnitController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit;

public class MonnitController : Controller
{
  protected internal XmlResult Xml(object data) => new XmlResult(data);

  protected internal JsonpResult Jsonp(object data, JsonRequestBehavior behavior)
  {
    JsonpResult jsonpResult = new JsonpResult();
    jsonpResult.Data = data;
    jsonpResult.JsonRequestBehavior = behavior;
    return jsonpResult;
  }

  protected ActionResult FormatRequest(object obj)
  {
    try
    {
      SensorRestAPI data = new SensorRestAPI();
      data.Method = this.RouteData.Values["action"].ToString();
      switch (this.RouteData.Values["response"].ToString().ToLower())
      {
        case "xml":
          if (obj.GetType().IsGenericType && obj is ICollection)
            obj = (object) MonnitController.CreateXML(obj as ICollection);
          if (obj is DataTable)
            obj = (object) MonnitController.CreateXML(obj as DataTable);
          if (obj is DataSet)
            obj = (object) MonnitController.CreateXML(obj as DataSet);
          if (obj.GetType().Namespace == "iMonnit.API")
            obj = (object) MonnitController.CreateXML(obj);
          data.Result = obj;
          return (ActionResult) this.Xml((object) data);
        default:
          if (obj is DataTable)
            obj = (object) MonnitController.Prep(obj as DataTable);
          if (obj is DataSet)
            obj = (object) MonnitController.Prep(obj as DataSet);
          foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>) obj.GetType().GetProperties()).Where<PropertyInfo>((System.Func<PropertyInfo, bool>) (x => x.PropertyType == typeof (DateTime))))
          {
            propertyInfo.GetValue(obj);
            DateTime dateTime = DateTime.SpecifyKind((DateTime) propertyInfo.GetValue(obj), DateTimeKind.Utc);
            propertyInfo.SetValue(obj, (object) dateTime);
          }
          data.Result = obj;
          if (!string.IsNullOrEmpty(this.Request["callback"]))
            return (ActionResult) this.Jsonp((object) data, JsonRequestBehavior.AllowGet);
          JsonResult jsonResult = this.Json((object) data, JsonRequestBehavior.AllowGet);
          jsonResult.MaxJsonLength = new int?(Convert.ToInt32(ConfigData.AppSettings("MaxJsonLength", int.MaxValue.ToString())));
          return (ActionResult) jsonResult;
      }
    }
    finally
    {
      if (MonnitSession.CurrentCustomer != null)
        AuthorizeAPIAttribute.CachedAccount(MonnitSession.CurrentCustomer.AccountID).APIActive = false;
    }
  }

  public static Dictionary<string, object> Prep(DataTable table)
  {
    return new Dictionary<string, object>()
    {
      {
        table.TableName,
        (object) MonnitController.RowsToDictionary(table)
      }
    };
  }

  public static Dictionary<string, object> Prep(DataSet data)
  {
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    foreach (DataTable table in (InternalDataCollectionBase) data.Tables)
      dictionary.Add(table.TableName, (object) MonnitController.RowsToDictionary(table));
    return dictionary;
  }

  public static DataTable CreateDataTable(ICollection list)
  {
    Type genericArgument = list.GetType().GetGenericArguments()[0];
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(genericArgument);
    DataTable dataTable = new DataTable(genericArgument.Name + "List");
    for (int index = 0; index < properties.Count; ++index)
    {
      PropertyDescriptor propertyDescriptor = properties[index];
      dataTable.Columns.Add(propertyDescriptor.Name, propertyDescriptor.PropertyType);
    }
    object[] objArray = new object[properties.Count];
    foreach (object component in (IEnumerable) list)
    {
      for (int index = 0; index < objArray.Length; ++index)
        objArray[index] = properties[index].GetValue(component);
      dataTable.Rows.Add(objArray);
    }
    return dataTable;
  }

  public static XElement CreateXML(ICollection list)
  {
    Type genericArgument = list.GetType().GetGenericArguments()[0];
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(genericArgument);
    XElement xml = new XElement((XName) (genericArgument.Name + "List"));
    foreach (object obj in (IEnumerable) list)
    {
      XElement content = new XElement((XName) genericArgument.Name);
      if (genericArgument.IsPrimitive)
        content.Add((object) obj.ToString());
      else if (genericArgument == typeof (string))
      {
        content.Add((object) (string) obj);
      }
      else
      {
        for (int index = 0; index < properties.Count; ++index)
          content.Add((object) new XAttribute((XName) properties[index].Name, (object) properties[index].GetValue(obj).ToString()));
      }
      xml.Add((object) content);
    }
    return xml;
  }

  public static XElement CreateXML(DataTable table)
  {
    XElement xml = new XElement((XName) (table.TableName + "List"));
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      XElement content = new XElement((XName) table.TableName);
      foreach (DataColumn column in (InternalDataCollectionBase) table.Columns)
        content.Add((object) new XAttribute((XName) column.ColumnName, (object) row[column.ColumnName].ToString()));
      xml.Add((object) content);
    }
    return xml;
  }

  public static XElement CreateXML(DataSet tableSet)
  {
    XElement xml = new XElement((XName) tableSet.DataSetName);
    foreach (DataTable table in (InternalDataCollectionBase) tableSet.Tables)
      xml.Add((object) MonnitController.CreateXML(table));
    return xml;
  }

  public static XElement CreateXML(object obj)
  {
    Type type = obj.GetType();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
    XElement xml = new XElement((XName) type.Name);
    for (int index = 0; index < properties.Count; ++index)
      xml.Add((object) new XAttribute((XName) properties[index].Name, (object) properties[index].GetValue(obj).ToString()));
    return xml;
  }

  private static List<Dictionary<string, object>> RowsToDictionary(DataTable table)
  {
    List<Dictionary<string, object>> dictionary1 = new List<Dictionary<string, object>>();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
      for (int index = 0; index < table.Columns.Count; ++index)
        dictionary2.Add(table.Columns[index].ColumnName, row[index]);
      dictionary1.Add(dictionary2);
    }
    return dictionary1;
  }
}
