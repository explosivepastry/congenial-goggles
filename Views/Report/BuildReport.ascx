<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>

<div class="formtitle" style="padding-left: 15px;">Edit</div>
<div id="mainBuildReport" style="max-height: 500px; overflow-y: auto;">
    <% using (Html.BeginForm())
        { %>
    <%-- <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>
    <%: Html.ValidationSummary(true) %>
    <div style="padding: 25px;">

        <%: Html.HiddenFor(model => model.ReportScheduleID) %>
        <%: Html.HiddenFor(model => model.AccountID) %>
        <%: Html.HiddenFor(model => model.ReportQueryID) %>
        <%: Html.HiddenFor(model => model.LastRunDate) %>
        <%: Html.HiddenFor(model => model.LastReportScheduleResultID) %>

        <div class="editor-label">
            Report Type
        </div>
        <div class="editor-field">
            <%: ReportQuery.Load(Model.ReportQueryID).Name %>
        </div>

        <div class="editor-label">
            Report Name
        </div>
        <div class="editor-field">
            <input type="text" id="Name" name="Name" value="<%= Model.Name %>" />

            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>

        <h2 style="clear: both;">Schedule</h2>

        <div class="editor-label">
            Report Schedule
        </div>
        <div class="editor-field">
            <select id="ScheduleType" name="ScheduleType">
                <%ReportQuery query = ReportQuery.Load(Model.ReportQueryID);
                    if (query.ScheduleAnnually)
                    {%><option value="Annually" id="AnnuallyOption" <%:Model.ScheduleType == eReportScheduleType.Annually ? "selected='selected'" : "" %>>Annually</option>
                <%}
                    if (query.ScheduleMonthly)
                    {%><option value="Monthly" id="MonthlyOption" <%:Model.ScheduleType == eReportScheduleType.Monthly ? "selected='selected'" : "" %>>Monthly</option>
                <%}
                    if (query.ScheduleWeekly)
                    {%><option value="Weekly" id="WeeklyOption" <%:Model.ScheduleType == eReportScheduleType.Weekly ? "selected='selected'" : "" %>>Weekly</option>
                <%}
                    if (query.ScheduleDaily)
                    {%><option value="Daily" id="DailyOption" <%:Model.ScheduleType == eReportScheduleType.Daily ? "selected='selected'" : "" %>>Daily</option>
                <%}
                    if (query.ScheduleImmediately)
                    {%><option value="Immediately" id="ImmediatleyOption" <%:Model.ScheduleType == eReportScheduleType.Immediately ? "selected='selected'" : "" %>>Once</option>
                <%}%>
            </select>
        </div>

        <div class="editor-label schedule">
            Send Report On
        </div>
        <div class="editor-field schedule">
            <select id="Schedule" name="Schedule">
                <option value="1" <%:Model.Schedule == "1" ? "selected='selected'" : "" %>>1</option>
            </select><br />
            <%: Html.ValidationMessageFor(model => model.Schedule) %>
        </div>

        <div class="editor-label timeofday">
            Time of day to run
        </div>
        <div class="editor-field timeofday">
            <select id="LocalTime" name="LocalTime">

                <%if (!query.RequiresPreAggs)
                    {%>
                <option value="0:00:00" <%:Model.LocalTime.Hours == 0 ? "selected='selected'" : "" %>>Night</option>
                <%}%>

                <option value="6:00:00" <%:Model.LocalTime.Hours == 6 ? "selected='selected'" : "" %>>Morning</option>
                <option value="12:00:00" <%:Model.LocalTime.Hours == 12 ? "selected='selected'" : "" %>>Mid-day</option>
                <option value="18:00:00" <%:Model.LocalTime.Hours == 18 ? "selected='selected'" : "" %>>Evening</option>
            </select><br />


            <%: Html.ValidationMessageFor(model => model.LocalTime) %>
        </div>

        <%--Default to always run--%>
        <input type="hidden" name="StartDate" id="StartDate" value="<%:Model.StartDate.ToShortDateString() %>" />
        <input type="hidden" name="EndDate" id="EndDate" value="1/1/2099" />

        <%if (Model.Report.Parameters.Count > 0)
            { %>
        <h2 style="clear: both;">Report Specific Parameters</h2>
        <%}%>

        <%foreach (ReportParameter Parameter in Model.Report.Parameters)
            { %>
        <div class="editor-label">
            <%: Parameter.LabelText %>
        </div>
        <div class="editor-field" id="Field_<%:Parameter.ParamName%>">
            <%switch (Parameter.Type.Name)
                {
                    case "Boolean":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>">
                <option value="1" <%: (Model.FindParameterValue(Parameter.ReportParameterID) == "1" || Model.FindParameterValue(Parameter.ReportParameterID).ToLower() == "true") ? "selected='selected'" : "" %>>true</option>
                <option value="0" <%: (Model.FindParameterValue(Parameter.ReportParameterID) == "0" || Model.FindParameterValue(Parameter.ReportParameterID).ToLower() == "false") ? "selected='selected'" : "" %>>false</option>
            </select>
            <%break;

                case "NetworkID":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>">
                <%foreach (CSNet Network in CSNet.LoadByAccountID(Model.AccountID))
                    { %>
                <option value="<%:Network.CSNetID %>" <%: Model.FindParameterValue(Parameter.ReportParameterID).ToLong() == Network.CSNetID ? "selected='selected'" : "" %>><%= Network.Name %> (<%:Network.CSNetID %>)</option>
                <%}%>
            </select>
            <%break;

                case "SensorID":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>">
                <%foreach (Sensor Sen in Sensor.LoadByAccountID(Model.AccountID))
                    { %>
                <option value="<%:Sen.SensorID %>" <%: Model.FindParameterValue(Parameter.ReportParameterID).ToLong() == Sen.SensorID ? "selected='selected'" : "" %>><%= Sen.SensorName %> (<%:Sen.SensorID %>)</option>
                <%}%>
            </select>
            <%break;

                case "ApplicationID":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>">
                <option value="-1">Any</option>
                <%foreach (MonnitApplication MApp in MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID))
                    { %>
                <option value="<%:MApp.ApplicationID %>" <%: Model.FindParameterValue(Parameter.ReportParameterID).ToLong() == MApp.ApplicationID ? "selected='selected'" : "" %>><%:MApp.ApplicationName %> (<%:MApp.ApplicationID %>)</option>
                <%}%>
            </select>
            <%break;

                case "Hour":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>">
                <option value="0" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "0" ? "selected='selected'" : "" %>>12:00 AM</option>
                <option value="1" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "1" ? "selected='selected'" : "" %>>1:00 AM</option>
                <option value="2" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "2" ? "selected='selected'" : "" %>>2:00 AM</option>
                <option value="3" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "3" ? "selected='selected'" : "" %>>3:00 AM</option>
                <option value="4" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "4" ? "selected='selected'" : "" %>>4:00 AM</option>
                <option value="5" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "5" ? "selected='selected'" : "" %>>5:00 AM</option>
                <option value="6" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "6" ? "selected='selected'" : "" %>>6:00 AM</option>
                <option value="7" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "7" ? "selected='selected'" : "" %>>7:00 AM</option>
                <option value="8" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "8" ? "selected='selected'" : "" %>>8:00 AM</option>
                <option value="9" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "9" ? "selected='selected'" : "" %>>9:00 AM</option>
                <option value="10" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "10" ? "selected='selected'" : "" %>>10:00 AM</option>
                <option value="11" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "11" ? "selected='selected'" : "" %>>11:00 AM</option>
                <option value="12" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "12" ? "selected='selected'" : "" %>>12:00 PM</option>
                <option value="13" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "13" ? "selected='selected'" : "" %>>1:00 PM</option>
                <option value="14" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "14" ? "selected='selected'" : "" %>>2:00 PM</option>
                <option value="15" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "15" ? "selected='selected'" : "" %>>3:00 PM</option>
                <option value="16" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "16" ? "selected='selected'" : "" %>>4:00 PM</option>
                <option value="17" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "17" ? "selected='selected'" : "" %>>5:00 PM</option>
                <option value="18" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "18" ? "selected='selected'" : "" %>>6:00 PM</option>
                <option value="19" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "19" ? "selected='selected'" : "" %>>7:00 PM</option>
                <option value="20" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "20" ? "selected='selected'" : "" %>>8:00 PM</option>
                <option value="21" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "21" ? "selected='selected'" : "" %>>9:00 PM</option>
                <option value="22" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "22" ? "selected='selected'" : "" %>>10:00 PM</option>
                <option value="23" <%: Model.FindParameterValue(Parameter.ReportParameterID) == "23" ? "selected='selected'" : "" %>>11:00 PM</option>
            </select>
            <%break;

                case "Predefined":%>
            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" id="Param_<%: Parameter.ParamName %>" name="Param_<%: Parameter.ReportParameterID %>">
                <%string[] values = Parameter.PredefinedValues.Split('|');
                    foreach (string s in values)
                    {
                        string[] pairValues = s.Split(':');
                        if (pairValues.Length > 1)
                        {%>
                <option value="<%:pairValues[0]%>" <%:Model.FindParameterValue(Parameter.ReportParameterID) == pairValues[0] ? "selected='selected' id='PredefinedScheduleType'" : ""%>><%:pairValues[1]%></option>
                <%}
                    else
                    {%>
                <option value="<%:s%>" <%:Model.FindParameterValue(Parameter.ReportParameterID) == s ? "selected='selected' id='PredefinedScheduleType'" : ""%>><%:s%></option>
                <%}
                    }%>
            </select>
            <%break;

                default:%>
            <input type="text" class="<%:Parameter.Type.UIHint %> form-control" style="width: 250px;" name="Param_<%: Parameter.ReportParameterID %>" id="Param_<%: Parameter.ReportParameterID %>" value="<%:Model.FindParameterValue(Parameter.ReportParameterID) %>" />
            <%break;
                }// End switch%>
            <%if (!string.IsNullOrWhiteSpace(Parameter.HelpText))
                {%>
            <img alt="help" class="helpIcon" src="<%:Html.GetThemedContent("/images/help.png")%>" title="<%:Parameter.HelpText %>">
            <%} %><br />
            <%: Html.ValidationMessage("Param_" + Parameter.ReportParameterID) %>
        </div>
        <%}// End foreach%>
    </div>

    <div style="clear: both;"></div>
    <div class="buttons">
        <span style="color: red;"><%:(string.IsNullOrEmpty(ViewBag.Message)? "" : ViewBag.Message)%></span>
        <input type="button" value="Save" class="btn btn-primary" id="saveReportButton" />
        <a href="/Report/Create" class="btn btn-light">Cancel</a>
        <div style="clear: both;"></div>
    </div>
</div>
</div>
<div id="mainBuildReportLoad" style="display: none;">
    <div id="loadingGIF" class="text-center" style="display: none;">
        <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
            <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
        </div>
    </div>
</div>
<% } %>
<script>
    $(function () {
	    <% if (ViewBag.Message == "Success")
    { %>
        setTimeout("window.location.href = '/Report/Index';", 500);
        <%}%>
        $('#saveReportButton').click(function () {
            $('#mainBuildReport').hide();
            $('#mainBuildReportLoad').show();
            var form = $('form');
            $.post('/Report/BuildReport', form.serialize(), function (data) {
                $('#mainBuildReport').parent().html(data);
                $('#mainBuildReportLoad').hide();
                $('#mainBuildReport').show();

                if (data == "Success") {
                    window.location.href = '/Report/Index';
                }
            });
        });

        $('.datepicker').datepicker();
        $('.helpIcon').tipTip();

        setTimeout("$('#Name').focus();", 500);
        setSchedule($('#ScheduleType').val());
        $('#ScheduleType').change(function () {
            setSchedule($(this).val());
        });
    });

    function setSchedule(scheduleType) {

        selectValues = {};

        switch (scheduleType) {
            case "Annually":
                selectValues = {
                    "1": "January 1",
                    "2": "February 1",
                    "3": "March 1",
                    "4": "April 1",
                    "5": "May 1",
                    "6": "June 1",
                    "7": "July 1",
                    "8": "August 1",
                    "9": "September 1",
                    "10": "October 1",
                    "11": "November 1",
                    "12": "December 1"
                };
                $('.schedule').show();
                $('.timeofday').show();
                break;
            case "Monthly":
                selectValues = {
                    "1": "1st of Month",
                    "8": "8th of Month",
                    "15": "15th of Month",
                    "22": "22nd of Month"
                };
                $('.schedule').show();
                $('.timeofday').show();
                break;
            case "Weekly":
                selectValues = {
                    "1": "Sunday",
                    "2": "Monday",
                    "3": "Tuesday",
                    "4": "Wednesday",
                    "5": "Thursday",
                    "6": "Friday",
                    "7": "Saturday"
                };
                $('.schedule').show();
                $('.timeofday').show();
                break;
            case "Daily":
                selectValues = { "1": "Everyday" };
                $('.schedule').hide();
                $('.timeofday').show();
                break;
            case "Immediately":
                selectValues = { "1": "Everyday" };
                $('.schedule').hide();
                $('.timeofday').hide();
                break;
        }

        $('#Schedule').find('option').remove();

        $.each(selectValues, function (key, value) {
            $('#Schedule').append($("<option/>", {
                value: key,
                text: value
            }));
        });

        $('#Schedule').val('<%: Model.Schedule %>');

        if ($('#Schedule').prop('selectedIndex') < 0)
            $('#Schedule').prop('selectedIndex', 0);
    }
</script>
