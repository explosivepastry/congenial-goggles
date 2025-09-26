<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--<%
    if (!Model.GenerationType.Contains("2")) //This checks to see if Generation is Gen2// Not supported for Gen2/Alta until further notice 6/27/2017
    {
        if (new Version(Model.FirmwareVersion) >= new Version("1.2.009") && Model.SensorTypeID != 4)
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!Model.CanUpdate)
            {
                dic.Add("disabled", "disabled");
                ViewData["disabled"] = true;


            }

            dic.Add("style", "width:50px;");
            ViewData["HtmlAtts"] = dic;
            
%>--%>



<%--
<div class="form-group">
    <div class="bold col-md-3 col-sm-3 col-xs-12">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Sensor is on","Sensor is on")%>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
        <input type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveAllDay" id="ActiveAllDay" <%= Model.ActiveStartTime == Model.ActiveEndTime ? "checked" : "" %> data-toggle="toggle" data-on="<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|All Day","All Day")%>" data-off="<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Between","Between")%>" />
    </div>
</div>

<div class="activeTime form-group">
    <div class="bold col-md-3 col-sm-3 col-xs-12">
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
        <%: Html.TranslateTag("From","From")%>: 
         <%: Html.DropDownList("ActiveStartTimeHour", (SelectList)ViewData["StartHours"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>:
                <%: Html.DropDownList("ActiveStartTimeMinute", (SelectList)ViewData["StartMinutes"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <%: Html.DropDownList("ActiveStartTimeAM", (SelectList)ViewData["StartAM"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
    </div>
</div>

<div class="activeTime form-group">
    <div class="bold col-md-3 col-sm-3 col-xs-12">
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
        <%: Html.TranslateTag("To","To")%>:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%: Html.DropDownList("ActiveEndTimeHour", (SelectList)ViewData["EndHours"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>:
                <%: Html.DropDownList("ActiveEndTimeMinute", (SelectList)ViewData["EndMinutes"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <%: Html.DropDownList("ActiveEndTimeAM", (SelectList)ViewData["EndAM"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
    </div>
</div>--%>


<%--<%}
    }%>--%>

