<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MultiChartSensorDataModel>>" %>

<%
    foreach(MultiChartSensorDataModel model in Model)
    {
        eDatumType datum = model.GroupSensor.Sensor.getDatumType(model.GroupSensor.DatumIndex);
        string ViewToFind = string.Format("DataTypeSpecific\\{0}\\DatumChart", datum.ToInt().ToString("D2"));
        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
        {
            ViewBag.returnConfirmationURL = ViewToFind;
            Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", model);
        }
        else
        {
            Html.RenderPartial("~\\Views\\Sensor\\DataTypeSpecific\\Default\\DatumChart.ascx", model);

        }
%>

    <div class="clearfix"></div>
<%  } %>

