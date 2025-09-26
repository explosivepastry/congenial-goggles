<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
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
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_TimeofDay|Sensor is on","Sensor is on")%>
    </div>

    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Between")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ActiveAllDay" id="ActiveAllDay" <%= Model.ActiveStartTime == Model.ActiveEndTime ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","All Day")%></label>
        </div>
    </div>
</div>

<div class="activeTime row sensorEditForm">
    <div class="col-12 col-lg-3"></div>
    <div class="col-12 col-md-3 d-flex align-items-center">
        <%: Html.TranslateTag("From","From")%>: 
        <%: Html.DropDownList("ActiveStartTimeHour", (SelectList)ViewData["StartHours"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <span class="mx-1">:</span>
        <%: Html.DropDownList("ActiveStartTimeMinute", (SelectList)ViewData["StartMinutes"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <%: Html.DropDownList("ActiveStartTimeAM", (SelectList)ViewData["StartAM"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
    </div>
</div>

<div class="activeTime row sensorEditForm">
    <div class="col-12 col-lg-3"></div>
    <div class="col-12 col-md-3 d-flex align-items-center">
        <%: Html.TranslateTag("To","To")%>:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%: Html.DropDownList("ActiveEndTimeHour", (SelectList)ViewData["EndHours"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <span class="mx-1">:</span>
        <%: Html.DropDownList("ActiveEndTimeMinute", (SelectList)ViewData["EndMinutes"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
        <%: Html.DropDownList("ActiveEndTimeAM", (SelectList)ViewData["EndAM"], (Dictionary<string,object>)ViewData["HtmlAtts"])%>
    </div>
</div>
<%}
    }%>

<script type="text/javascript">

    $(document).ready(function () {
        $('#ActiveAllDay').change(function () {
            if ($('#ActiveAllDay').prop('checked')) {

                $(".activeTime").hide();
                $('#ActiveStartTimeHour').val('12');
                $('#ActiveStartTimeMinute').val('00');
                $('#ActiveStartTimeAM').val('AM');
                $('#ActiveEndTimeHour').val('12');
                $('#ActiveEndTimeMinute').val('00');
                $('#ActiveEndTimeAM').val('AM');
            }
            else {
                $(".activeTime").show();
            }
        });
        $('#ActiveAllDay').change();
    });
    $('select').addClass('form-select');

</script>

<style type="text/css">
    select {
        min-width: 90px;
    }
</style>
