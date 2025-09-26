<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>


<%: Html.HiddenFor(model => model.ReportScheduleID) %>
<%: Html.HiddenFor(model => model.AccountID) %>
<%: Html.HiddenFor(model => model.ReportQueryID) %>

<div class="editor-label">
</div>
<div class="editor-field">
    <b>If you have just received your report it will take at least 2 hours to receive this report again even if the report is set to immediately. </b>
</div>

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
    <%: Model.Name %>
</div>
<div class="editor-label">
    Result of last ran report
</div>
<div class="editor-field">

    <% ReportScheduleResult rsr = ReportScheduleResult.Load(Model.LastReportScheduleResultID);
        if (rsr != null)
        {%>
    <%: Html.Label(rsr.Result.ToString()) %>
    <%} %>
</div>
<div class="editor-label">
    Report Type
</div>
<div class="editor-field">
    <%: Model.Report.Name %>
</div>

<div class="editor-label">
    Schedule Type
</div>
<div class="editor-field">
    <%switch (Model.ScheduleType)
        {
            default:
            case eReportScheduleType.Annually:

            case eReportScheduleType.Monthly:

            case eReportScheduleType.Weekly:

            case eReportScheduleType.Daily:
                Response.Write(Model.ScheduleType.ToString());
                break;
            case eReportScheduleType.Immediately:
    %>Once<%
                  break;


              }  %>
</div>

<%if (Model.ScheduleType != eReportScheduleType.Immediately)
    { %>
<div class="editor-label">
    Time of day to run
</div>
<div class="editor-field">
    <%if (Model.LocalTime.Hours < 6)
        { %>
            Night
            <%}
                else if (Model.LocalTime.Hours < 12)
                { %>
            Morning
            <%}
                else if (Model.LocalTime.Hours < 18)
                { %>
            Mid-day
            <%}
                else
                { %>
            Evening
            <%}%>
</div>
<%}%>

<%--<div class="editor-label">
            Run From
        </div>
        <div class="editor-field">
            <%:Model.StartDate.ToShortDateString() %>
        </div>

        <div class="editor-label">
            Until
        </div>
        <div class="editor-field">
            <%:Model.EndDate.ToShortDateString() %>
        </div>--%>

<%foreach (ReportParameter Parameter in Model.Report.Parameters)
    { %>
<div class="editor-label">

    <%: Parameter.LabelText %>
</div>
<div class="editor-field">
    <%if (Parameter.ReportParameterTypeID == 3)
        {
            var val = Model.FindParameterValue(Parameter.ReportParameterID);
            if ((val.ToInt()) == 0)
            {
    %>
    <%: Html.Label("False")%>

    <%}
        else
        {%>
    <%: Html.Label("True")%>
    <%}
        }
        else
        { %>
    <%:Model.FindParameterValue(Parameter.ReportParameterID)%>
    <%} %>
</div>
<%} %>

<div style="clear: both;"></div>

<div>
    <table width="100%">
        <tr>
            <td style="vertical-align: top; width: 45%;" class="notiLT">
                <div class="tableLeft">
                    <div class="blockSectionTitle">

                        <div class="deviceSearch">
                            <div class="searchInput">
                                <input id="userFilter" name="userFilter" type="text" style="width: 205px;" /></div>
                            <div class="searchButton">
                                <img src="../../Content/images/Notification/device-search.png" /></div>
                        </div>
                        <!-- deviceSearch -->

                        <div style="clear: both;"></div>
                    </div>
                    <div id="divUserList" style="height: 286px; overflow-y: auto; padding: 10px;">
                    </div>
                </div>
            </td>
            <td style="vertical-align: top;" align="center" class="notiCT">
                <div id="addRecipients">
                    <a href="Add" onclick="addRecipient('Email'); return false;" class="addbutton">
                        <img src="/content/images/notification/add-arrows.png" class="" /></a><br />
                </div>
            </td>
            <td style="vertical-align: top; width: 45%;" class="notiRT">
                <div class="tableRight">
                    <div class="blockSectionTitle">
                        <div class="blockTitle">Report will be sent to</div>
                        <div style="clear: both;"></div>
                    </div>
                    <div style="max-height: 300px; overflow-y: auto;">
                        <table id="recipientsTable" width="100%">
                            <% Html.RenderPartial("AddRecipient", Model.DistributionList); %>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>







<div class="buttons" style="margin: 10px -10px -10px -10px;">
    <a class="bluebutton loadMain" href="/Report/BuildReport/<%:Model.ReportScheduleID %>" title="<%:Model.Report.Name + " - " + Model.Report.Description %>">Edit</a>
    <a class="greybutton delete" href="/Report/Delete/<%:Model.ReportScheduleID %>" title="Delete">Delete</a>
    <div style="clear: both;"></div>
</div>

<script>
    var userFilterTimeout = null;
    $(function () {
        $('.loadMain').click(function (e) {
            e.preventDefault();

            getMain($(this).attr('href'), '<%:Model.Name%>');
        });

        $('.delete').click(function (e) {
            e.preventDefault();

            if (confirm("Are you sure you want to delete this scheduled report.")) {
                $.get($(this).attr("href"), function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }

                    window.location.href = window.location.href;
                });
            }
        });

        loadUsers();

        $('#userFilter').watermark('User Search', {
            left: 5,
            top: 0
        }).keyup(function () {
            if (userFilterTimeout != null)
                clearTimeout(userFilterTimeout);
            userFilterTimeout = setTimeout("loadUsers();", 1000);
        });
    });

    function loadUsers() {
        $.get("/Report/UserList/<%:Model.ReportScheduleID %>?q=" + $('#userFilter').val(), function (data) {
            $('#divUserList').html(data);
        });
    }

    function addRecipient() {
        var url = "/Report/AddRecipient/<%:Model.ReportScheduleID %>";
        var params = "";
        var custChecked = false;
        $("input:checked").each(function () {
            var id = $(this).attr("id");
            if (id && id.indexOf("customerID_") == 0)//make sure it is from the correct list
            {
                params += "&customerIDs=" + id.replace("customerID_", "");
                custChecked = true;
            }
        });

        if (custChecked == false) {
            showSimpleMessageModal("<%=Html.TranslateTag("You must select at least one user to add.")%>");
            return;
        }

        $.post(url, params, function (data) {
            $('#recipientsTable').html(data);
            loadUsers();
        });
    }

    function removeRecipient(customerID) {
        var url = "/Report/RemoveRecipient/<%:Model.ReportScheduleID %>";
        var params = "customerID=" + customerID;
        $.post(url, params, function (data) {
            $('#recipientsTable').html(data);
            loadUsers();
        });
    }
</script>
