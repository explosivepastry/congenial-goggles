<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>
<style>
    .dropdown-content {
        display: none;
        background-color: #f6f6f6;
        min-width: 230px;
        overflow: visible;
        border: 1px solid #ddd;
        z-index: 1;
    }

        .dropdown-content a {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
            cursor: pointer;
        }

            .dropdown-content a:hover {
                background-color: #ddd;
            }

    .show {
        display: block;
    }
</style>


<div class="formtitle" style="padding-left: 15px;" hidden>
    <%: Html.TranslateTag("Export/ReportEdit|Edit Report"," Edit Report") %>
</div>

<div id="mainBuildReport" style="max-height: 900px;">

    <% using (Html.BeginForm())
        { %>

    <%=Html.ValidationSummary(true) %>

    <div class="col-12 ">

        <%: Html.HiddenFor(model => model.ReportScheduleID) %>
        <%: Html.HiddenFor(model => model.AccountID) %>
        <%: Html.HiddenFor(model => model.ReportQueryID) %>
        <%: Html.HiddenFor(model => model.LastRunDate) %>
        <%: Html.HiddenFor(model => model.LastReportScheduleResultID) %>

        <div class="">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Export/ReportEdit|Edit Report"," Edit Report") %>
                    </div>
                </div>

                <div class="card_container__body">
                    <div class="card_container__body__content">
                        <div class="row section" style="margin-left: 10px; margin-bottom: 10px;">
                            <div class="form-group"><%: Html.TranslateTag("Export/_BuildReport|Report Name","Report Name") %></div>
                            <div class="col-md-3 col-xs-6">
                                <input type="text" id="mapName" name="Name" value="<%= Model.Name %>" class="form-control user-dets" style="width: 250px;">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="row section" style="margin-left: 10px; margin-bottom: 10px;">
                            <div class="form-group"><%: Html.TranslateTag("Export/_BuildReport|Report Schedule","Report Schedule") %></div>
                            <div class="col-md-2 col-xs-6">

                                <select id="ScheduleType" name="ScheduleType" class="form-select" style="width: 250px;">
                                    <%ReportQuery query = ReportQuery.Load(Model.ReportQueryID);
                                        if (query.ScheduleAnnually)
                                        {%><option value="Annually" id="AnnuallyOption" <%:Model.ScheduleType == eReportScheduleType.Annually ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Annually","Annually") %></option>
                                    <%}

                                        if (query.ScheduleMonthly)
                                        {%><option value="Monthly" id="MonthlyOption" <%:Model.ScheduleType == eReportScheduleType.Monthly ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Monthly","Monthly") %></option>
                                    <%}

                                        if (query.ScheduleWeekly)
                                        {%><option value="Weekly" id="WeeklyOption" <%:Model.ScheduleType == eReportScheduleType.Weekly ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Weekly","Weekly") %></option>
                                    <%}

                                        if (query.ScheduleDaily)
                                        {%><option value="Daily" id="DailyOption" <%:Model.ScheduleType == eReportScheduleType.Daily ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Daily","Daily") %></option>
                                    <%}

                                        if (query.ScheduleImmediately)
                                        {%><option value="Immediately" id="ImmediatelyOption" <%:Model.ScheduleType == eReportScheduleType.Immediately ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Once","Once") %></option>
                                    <%}%>
                                </select>

                                <div class="clearfix"></div>
                            </div>
                        </div>

                        <div class="row section schedule" style="margin-left: 10px; margin-bottom: 10px;">
                            <div class="0 form-group"><%: Html.TranslateTag("Export/_BuildReport|Send Report On","Send Report On") %></div>
                            <div class="col-md-2 col-xs-6 schedule">
                                <select id="Schedule" name="Schedule" class="form-select" style="width: 250px;">
                                    <option value="1" <%:Model.Schedule == "1" ? "selected='selected'" : "" %>>1</option>
                                </select>
                                <%: Html.ValidationMessageFor(model => model.Schedule) %>
                                <div class="clearfix"></div>
                            </div>
                        </div>

                        <div class="row section timeofday" style="margin-left: 10px; margin-bottom: 10px;">
                            <div class="0 form-group"><%: Html.TranslateTag("Export/_BuildReport|Time of day to run","Time of day to run") %></div>
                            <div class="col-md-2 col-xs-6 timeofday">
                                <select id="LocalTime" name="LocalTime" class="form-select" style="width: 250px;">

                                    <%if (!query.RequiresPreAggs)
                                        {%>
                                    <option value="0:00:00" <%:Model.LocalTime.Hours == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Night","Night") %></option>
                                    <%}%>
                                    <option value="6:00:00" <%:Model.LocalTime.Hours == 6 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Morning","Morning") %></option>
                                    <option value="12:00:00" <%:Model.LocalTime.Hours == 12 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Mid-day","Mid-day") %></option>
                                    <option value="18:00:00" <%:Model.LocalTime.Hours == 18 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Export/_BuildReport|Evening","Evening") %></option>
                                </select><br />


                                <%: Html.ValidationMessageFor(model => model.LocalTime) %>
                            </div>

                            <input type="hidden" name="StartDate" id="StartDate" value="<%:Model.StartDate.ToShortDateString() %>" />
                            <input type="hidden" name="EndDate" id="EndDate" value="1/1/2099" />

                            <div class="clearfix"></div>

                        </div>

                        <%
                            bool isEnterprise = ConfigData.AppSettings("IsEnterprise").ToBool();
                            if (!isEnterprise)
                            {%>

                        <%--	<div class="row section" style="margin-left: 10px; margin-bottom: 10px;">
						<div class="form-group"><%: Html.TranslateTag("Export/_BuildReport|Send Report as Attachment", "Send Report as Attachment*") %></div>
						<div class="w-100">							
							<input type="checkbox" id="ckbAttachment" class="me-2" name="SendAsAttachment" <%:((bool)Model.SendAsAttachment) ? "checked" : ""%>>	
							
							<label id="lblAttachmentWarning">*<%: Html.TranslateTag("Export/_BuildReport|The email will contain a report link that will expire after 30 days", "The email will contain a report link that will expire after 30 days.") %></label>
						</div>
						<div class="clearfix"></div>
					</div>--%>

                        <div class="row sensorEditForm">
                            <div class="col sensorEditFormInput">
                                <div class="form-check form-switch d-flex align-items-center ps-0">
                                    <label class="form-check-label"><%: Html.TranslateTag("Download from Portal", "Download from Portal") %></label>
                                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="reportToggle" name="SendAsAttachment" <%: ((bool)Model.SendAsAttachment) ? "checked" : "" %>>
                                    <label class="form-check-label"><%: Html.TranslateTag("Send as Attachment", "Send as Attachment") %></label>
                                </div>
                            </div>
                        </div>

                        <%}
                            else
                            {%>
                        <input type="hidden" name="SendAsAttachment" value="true">
                        <%} %>

                        <div class="clearfix"></div>
                    </div>

                    <%if (Model.Report.Parameters.Count == 0)
                        {%>
                    <div class="dfac justify-content-end">
                        <span style="color: red;"><%:(string.IsNullOrEmpty(ViewBag.Message)? "" : ViewBag.Message)%></span>
                        <a href="/Export/ReportTemplates" class="btn btn-light me-2 buildReportCancel"><%: Html.TranslateTag("Cancel","Cancel") %></a>
                        <button type="button" value="<%: Html.TranslateTag("Save","Save") %>" class="btn btn-primary" id="saveReportButton">
                            <%: Html.TranslateTag("Save","Save") %>
                        </button>
                        <div style="clear: both;"></div>
                    </div>
                    <%} %>
                </div>
                <%--// card container--%>
            </div>
        </div>
        <%--//card_container shadow-sm rounded mb-4--%>

        <%if (Model.Report.Parameters.Count > 0)
            {%>
        <div class="  ">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Export/_BuildReport|Report Specific Parameters","Report Specific Parameters") %>
                    </div>
                </div>
                <div class="card_container__body">
                    <div class="card_container__body__content">

                        <%foreach (ReportParameter Parameter in Model.Report.Parameters)
                            {
                                var parameterId = Parameter.ReportParameterID; %>

                        <div title="<%:Parameter.HelpText %>" class="form-group card-margin-top">&nbsp;<%: Parameter.LabelText %></div>

                        <div class="card-margin-top" data-paramname="<%:Parameter.ParamName%>" id="Field_<%:Parameter.ParamName%>">
                            <%switch (Parameter.Type.Name)
                                {
                                    case "Boolean":%>
                            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: parameterId %>" id="Param_<%: parameterId %>">
                                <option value="1" <%: (Model.FindParameterValue(parameterId) == "1" || Model.FindParameterValue(parameterId).ToLower() == "true") ? "selected='selected'" : "" %>><%: Html.TranslateTag("True","True") %></option>
                                <option value="0" <%: (Model.FindParameterValue(parameterId) == "0" || Model.FindParameterValue(parameterId).ToLower() == "false") ? "selected='selected'" : "" %>><%: Html.TranslateTag("False","False") %></option>
                            </select>

                            <%break;

                                case "NetworkID":%>
                            <select class="<%:Parameter.Type.UIHint %> form-select" name="Param_<%: parameterId %>" id="Select1" style="width: 500px !important;">
                                <%foreach (CSNet Network in CSNet.LoadByAccountID(Model.AccountID))
                                    { %>

                                <%if (Network.Sensors.Count > query.SensorLimit)
                                    {%>
                                <option title="Exceeds Sensor Limit" disabled="disabled" value="<%:Network.CSNetID %>" <%: Model.FindParameterValue(parameterId).ToLong() == Network.CSNetID ? "disabled='disabled'" : "" %>><%= Network.Name %> (<%:Network.CSNetID %>)</option>

                                <%}
                                    else
                                    {%>
                                <option value="<%:Network.CSNetID %>" <%: Model.FindParameterValue(parameterId).ToLong() == Network.CSNetID ? "selected='selected'" : "" %>><%= Network.Name %> (<%:Network.CSNetID %>)</option>

                                <%}%>
                                <%}%>
                            </select>
                            <%break;

                                case "SensorID":%>
                            <select onclick="dropDownFunction()" style="width: 300px;" class="form-select <%:Parameter.Type.UIHint %>" name="Param_<%: parameterId %>" id="sensorFilter" onkeyup="filterFunction()" value="<%=Model.FindParameterValue(parameterId) %>">
                                <%foreach (Sensor Sen in Sensor.LoadByAccountID(Model.AccountID).OrderBy(s => s.SensorName))
                                    { %>
                                <option id="sensorSelect_<%: Sen.SensorID %>" onclick="selectFunction(<%: Sen.SensorID %>)" data-sensorname="<%:Sen.SensorName %>" <%: Model.FindParameterValue(parameterId).ToLong() == Sen.SensorID ? "selected='selected'" : "" %> value="<%:Sen.SensorID %>"><%= Sen.SensorName %> (<%:Sen.SensorID %>)</option>

                                <%}%>    <%--line 194--%>
                            </select>
                            <%break;

                                case "ApplicationID":%>
                            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: parameterId %>" id="Select3">
                                <option value="-1"><%: Html.TranslateTag("Export/_BuildReport|Any","Any")%></option>
                                <%foreach (MonnitApplication MApp in MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                    {%>
                                <option value="<%:MApp.ApplicationID %>" <%: Model.FindParameterValue(parameterId).ToLong() == MApp.ApplicationID ? "selected='selected'" : "" %>><%:MApp.ApplicationName %> (<%:MApp.ApplicationID %>)</option>
                                <%}%>       <%--line 205 --%>
                            </select>
                            <%break;

                                case "Hour":%>
                            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" name="Param_<%: parameterId %>" id="Select4">
                                <option value="0" <%: Model.FindParameterValue(parameterId) == "0" ? "selected='selected'" : "" %>>12:00 AM</option>
                                <option value="1" <%: Model.FindParameterValue(parameterId) == "1" ? "selected='selected'" : "" %>>1:00 AM</option>
                                <option value="2" <%: Model.FindParameterValue(parameterId) == "2" ? "selected='selected'" : "" %>>2:00 AM</option>
                                <option value="3" <%: Model.FindParameterValue(parameterId) == "3" ? "selected='selected'" : "" %>>3:00 AM</option>
                                <option value="4" <%: Model.FindParameterValue(parameterId) == "4" ? "selected='selected'" : "" %>>4:00 AM</option>
                                <option value="5" <%: Model.FindParameterValue(parameterId) == "5" ? "selected='selected'" : "" %>>5:00 AM</option>
                                <option value="6" <%: Model.FindParameterValue(parameterId) == "6" ? "selected='selected'" : "" %>>6:00 AM</option>
                                <option value="7" <%: Model.FindParameterValue(parameterId) == "7" ? "selected='selected'" : "" %>>7:00 AM</option>
                                <option value="8" <%: Model.FindParameterValue(parameterId) == "8" ? "selected='selected'" : "" %>>8:00 AM</option>
                                <option value="9" <%: Model.FindParameterValue(parameterId) == "9" ? "selected='selected'" : "" %>>9:00 AM</option>
                                <option value="10" <%: Model.FindParameterValue(parameterId) == "10" ? "selected='selected'" : "" %>>10:00 AM</option>
                                <option value="11" <%: Model.FindParameterValue(parameterId) == "11" ? "selected='selected'" : "" %>>11:00 AM</option>
                                <option value="12" <%: Model.FindParameterValue(parameterId) == "12" ? "selected='selected'" : "" %>>12:00 PM</option>
                                <option value="13" <%: Model.FindParameterValue(parameterId) == "13" ? "selected='selected'" : "" %>>1:00 PM</option>
                                <option value="14" <%: Model.FindParameterValue(parameterId) == "14" ? "selected='selected'" : "" %>>2:00 PM</option>
                                <option value="15" <%: Model.FindParameterValue(parameterId) == "15" ? "selected='selected'" : "" %>>3:00 PM</option>
                                <option value="16" <%: Model.FindParameterValue(parameterId) == "16" ? "selected='selected'" : "" %>>4:00 PM</option>
                                <option value="17" <%: Model.FindParameterValue(parameterId) == "17" ? "selected='selected'" : "" %>>5:00 PM</option>
                                <option value="18" <%: Model.FindParameterValue(parameterId) == "18" ? "selected='selected'" : "" %>>6:00 PM</option>
                                <option value="19" <%: Model.FindParameterValue(parameterId) == "19" ? "selected='selected'" : "" %>>7:00 PM</option>
                                <option value="20" <%: Model.FindParameterValue(parameterId) == "20" ? "selected='selected'" : "" %>>8:00 PM</option>
                                <option value="21" <%: Model.FindParameterValue(parameterId) == "21" ? "selected='selected'" : "" %>>9:00 PM</option>
                                <option value="22" <%: Model.FindParameterValue(parameterId) == "22" ? "selected='selected'" : "" %>>10:00 PM</option>
                                <option value="23" <%: Model.FindParameterValue(parameterId) == "23" ? "selected='selected'" : "" %>>11:00 PM</option>
                            </select>
                            <%break;

                                case "Predefined":%>
                            <select class="<%:Parameter.Type.UIHint %> form-select" style="width: 300px;" id="Param_<%: Parameter.ParamName %>" name="Param_<%: parameterId %>">
                                <%string[] values = Parameter.PredefinedValues.Split('|');
                                    foreach (string s in values)
                                    {
                                        string[] pairValues = s.Split(':');
                                        if (pairValues.Length > 1)
                                        {%>
                                <option value="<%:pairValues[0]%>" <%:Model.FindParameterValue(parameterId) == pairValues[0] ? "selected='selected' id='PredefinedScheduleType'" : ""%>><%:pairValues[1]%></option>
                                <%}
                                    else
                                    {%>
                                <option value="<%:s%>" <%:Model.FindParameterValue(parameterId) == s ? "selected='selected' id='PredefinedScheduleType'" : ""%>><%:s%></option>
                                <%}
                                    }%>
                            </select>
                            <%break;

                                case "Date":%>
                            <%string tempValue = Model.FindParameterValue(parameterId);
                                DateTime tempDate;
                                try
                                {
                                    tempDate = DateTime.Parse(tempValue);
                                    tempValue = tempDate.OVToDateShort();
                                }
                                catch
                                {
                                    tempDate = DateTime.Now;
                                    tempValue = "";
                                }%>
                            <input type="text" class="datepicker form-control" name="Param_<%: parameterId %>" id="Param_<%: parameterId %>" data-id="<%:parameterId %>" value="<%:tempValue %>" style="width: 300px;" />
                            <script type="text/javascript">
                                $(function () {
                                    // Mobiscroll DatePicker
                                    $('#Param_<%: parameterId %>').mobiscroll().datepicker({
                                        theme: 'ios',
                                        display: popLocation,
                                        defaultSelection: new Date(<%=tempDate.Year%>,<%=tempDate.Month - 1%>,<%=tempDate.Day%>),
                                        dateFormat: dFormat.toUpperCase(),
                                        controls: ['calendar'],
                                        headerText: 'Date: {value}'
                                    });
                                });
                            </script>

                            <%break;
                                default:%>

                            <input type="text" class="<%:Parameter.Type.UIHint %> form-control" title="<%:Parameter.HelpText %>" name="Param_<%: parameterId %>" id="Param_<%: parameterId %>" data-id="<%:parameterId %>" value="<%:Model.FindParameterValue(parameterId) %>" style="width: 300px;" />
                            <%break;
                                }%>

                            <%: Html.ValidationMessage("Param_" + parameterId) %>
                        </div>
                        <%}%>

                        <%if (Model.Report.Parameters.Count > 0)
                            {%>

                        <div class="buttons dfac justify-content-end card-margin-top">
                            <%--      <span style="color: red;"><%:(string.IsNullOrEmpty(ViewBag.Message)? "" : ViewBag.Message)%></span>--%>
                            <a href="/Export/ReportTemplates" class="btn btn-light me-2"><%: Html.TranslateTag("Cancel","Cancel") %></a>
                            <button type="button" value="<%: Html.TranslateTag("Save","Save") %>" class="btn btn-primary" id="saveReportButton">
                                <%: Html.TranslateTag("Save","Save") %>
                            </button>

                            <div style="clear: both;"></div>
                        </div>

                        <%} %>
                    </div>
                </div>
            </div>
        </div>

        <%}%>
    </div>
</div>

<div id="mainBuildReportLoad" style="display: none;">
    <div id="loadingGIF" class="text-center" style="display: none;">
        <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
            <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
        </div>
    </div>
</div>

<%}%>


<script type="text/javascript">

    document.addEventListener("DOMContentLoaded", function () {
        const reportToggle = document.getElementById("reportToggle");

        reportToggle.addEventListener("change", function () {
            if (reportToggle.checked) {
                console.log("Send as Attachment is enabled.");
            } else {
                console.log("Send as Attachment is disabled.");
            }
        });
    });


    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    var dFormat = '<%= MonnitSession.CurrentCustomer.Preferences["Date Format"].ToLower() %>';
    $(function () {
		<% if (ViewBag.Message == "Success")
    {%> setTimeout("window.location.href = '/Export/ReportIndex';", 500); <%}%>
        $('#mainBuildReport').css('max-height', '');
        $('.formtitle').css('margin-top', '-6px');

        $('#saveReportButton').click(function () {
            $('#mainBuildReport').hide();
            $('#mainBuildReportLoad').show();
            var form = $('form').serializeArray();

            $.each(form, function () {
                if (this.name == 'SendAsAttachment') {
                    if (this.value == 'on') {
                        this.value = 'true';
                    }
                    else {
                        this.value = 'false';
                    }
                }
            });

            $.post('/Export/_BuildReport', form, function (data) {
                $('#mainBuildReport').parent().html(data);
                $('#mainBuildReportLoad').hide();
                $('#mainBuildReport').show();
                if (data == "Success") {

                    window.location.href = '/Export/ReportIndex';
                    //setTimeout(function () {

                    //    toastBuilder("Success");
                    //}, 100)

                }
            });
        });

        $("#ckbAttachment").change(function () {
            if (this.checked) {
                $('#lblAttachmentWarning').html('*<%: Html.TranslateTag("Export/_BuildReport|Sending the report as an attachment could cause the email to arrive as spam","Sending the report as an attachment could cause the email to arrive as spam.") %>');
            }
            else {
                $('#lblAttachmentWarning').html('*<%: Html.TranslateTag("Export/_BuildReport|The email will contain a report link that will expire after 30 days","The email will contain a report link that will expire after 30 days.") %>');
            }
        });

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
                    "1": "<%=Html.TranslateTag("Export/_BuildReport|January", "January")%> 1",
                    "2": "<%=Html.TranslateTag("Export/_BuildReport|February", "February")%> 1",
                    "3": "<%=Html.TranslateTag("Export/_BuildReport|March", "March")%> 1",
                    "4": "<%=Html.TranslateTag("Export/_BuildReport|April", "April")%> 1",
                    "5": "<%=Html.TranslateTag("Export/_BuildReport|May", "May")%> 1",
                    "6": "<%=Html.TranslateTag("Export/_BuildReport|June", "June")%> 1",
                    "7": "<%=Html.TranslateTag("Export/_BuildReport|July", "July")%> 1",
                    "8": "<%=Html.TranslateTag("Export/_BuildReport|August", "August")%> 1",
                    "9": "<%=Html.TranslateTag("Export/_BuildReport|September", "September")%> 1",
                    "10": "<%=Html.TranslateTag("Export/_BuildReport|October", "October")%> 1",
                    "11": "<%=Html.TranslateTag("Export/_BuildReport|November", "November")%> 1",
                    "12": "<%=Html.TranslateTag("Export/_BuildReport|December", "December")%> 1"
                };
                $('.schedule').show();
                $('.timeofday').show();

                break;
            case "Monthly":
                selectValues = {
                    "1": "<%=Html.TranslateTag("Export/_BuildReport|1st", "1st ")%> <%=Html.TranslateTag("Export/_BuildReport|of the month", " of the month")%>",
                    "8": "<%=Html.TranslateTag("Export/_BuildReport|8th", "8th ")%> <%=Html.TranslateTag("Export/_BuildReport|of the month", " of the month")%>",
                    "15": "<%=Html.TranslateTag("Export/_BuildReport|15th", "15th ")%> <%=Html.TranslateTag("Export/_BuildReport|of the month", " of the month")%>",
                    "22": "<%=Html.TranslateTag("Export/_BuildReport|22nd", "22nd ")%> <%=Html.TranslateTag("Export/_BuildReport|of the month", " of the month")%>"
                };
                $('.schedule').show();
                $('.timeofday').show();

                break;
            case "Weekly":
                selectValues = {
                    "1": "<%=Html.TranslateTag("Export/_BuildReport|Sunday", "Sunday")%>",
                    "2": "<%=Html.TranslateTag("Export/_BuildReport|Monday", "Monday")%>",
                    "3": "<%=Html.TranslateTag("Export/_BuildReport|Tuesday", "Tuesday")%>",
                    "4": "<%=Html.TranslateTag("Export/_BuildReport|Wednesday", "Wednesday")%>",
                    "5": "<%=Html.TranslateTag("Export/_BuildReport|Thursday", "Thursday")%>",
                    "6": "<%=Html.TranslateTag("Export/_BuildReport|Friday", "Friday")%>",
                    "7": "<%=Html.TranslateTag("Export/_BuildReport|Saturday", "Saturday")%>"
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

        }   // switch

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

    //function dropDownFunction() {
    //	document.getElementById("myDropdown").classList.toggle("show");
    //}

    function filterFunction() {
        var input, filter, ul, li, a, i;
        input = document.getElementById("sensorFilter");
        filter = input.value.toUpperCase();
        //div = document.getElementById("myDropdown");
        a = div.getElementsByTagName("a");
        for (i = 0; i < a.length; i++) {
            if (a[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                a[i].style.display = "";
            } else {
                a[i].style.display = "none";
            }
        }
    }

    function selectFunction(id) {
        //var name = $("#sensorSelect_"+id).attr("data-sensorname");
        $("#sensorFilter").val(id);
        dropDownFunction();

    }

<%
    string viewBagMessage = "";
    if (ViewBag.Message != null)
    {
        viewBagMessage = ViewBag.Message.ToString();
    }
%>

    var viewBagMessageJS = `<%: viewBagMessage %>`;

    if (viewBagMessageJS.length > 1) {
        if (viewBagMessageJS !== "Success") {
            toastBuilder(viewBagMessageJS)
        } else {
            toastBuilder("Success")
        }
    }

</script>

