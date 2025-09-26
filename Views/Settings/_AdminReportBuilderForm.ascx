<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportQuery>" %>

<!-- Name -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.Name) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="Name" class="form-control " name="Name" value="<%= Model.Name %>" />
        <%: Html.ValidationMessageFor(model => model.Name) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Description -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.Description) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="Description" name="Description" class="form-control " value="<%= Model.Description %>" />
        <%: Html.ValidationMessageFor(model => model.Description) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Account ID -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.AccountID) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="AccountID" name="AccountID" class="form-control " value="<%= Model.AccountID > 0 ? Model.AccountID.ToString() : ""%>" />
        <%: Html.ValidationMessageFor(model => model.AccountID) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- AccountTheme ID -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.AccountThemeID) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="AccountThemeID" name="AccountThemeID" class="form-control " value="<%= Model.AccountThemeID > 0 ? Model.AccountThemeID.ToString() : ""%>" />
        <%: Html.ValidationMessageFor(model => model.AccountThemeID) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Viewable By -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Viewable By","Viewable By")%>
    </div>
    <div class="col sensorEditFormInput">
        <%--            <%:Html.DropDownList("CustomerAccess", Model.CustomerAccess) %>   --%>
        <select id="CustomerAccess" name="CustomerAccess" class="form-select" style="width: 250px;">
            <option value="Everyone" selected="selected">Everyone</option>
            <option value="Reseller">Reseller</option>
            <option value="Premier">Premiere</option>
        </select>
    </div>
    <div class="clearfix"></div>
</div>
<!-- ReportType  -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Report Type","Report Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="ReportTypeID" name="ReportTypeID" class="form-select" style="width: 250px;">
            <%foreach (ReportType reportType in ReportType.LoadAll())
                { %>
            <option value="<%=reportType.ReportTypeID %>" <%=Model.ReportTypeID == reportType.ReportTypeID ? "selected='selected'":"" %>><%=reportType.Name %></option>
            <%} %>
        </select>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Tags -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Tags","Tags")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" name="Tags" placeholder="CFR21|MEDIUM|BETA" id="Tags" class="form-control " value="<%=Model.Tags %>" title="<%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Pipe delimited values to restrict visibility. example: CFR|HSB|NewAccounts","Pipe delimited values to restrict visibility. example: CFR|HSB|NewAccounts")%>" />
    </div>
    <div class="clearfix"></div>
</div>
<!-- ReportBuilder -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.ReportBuilder) %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="ReportBuilder" name="ReportBuilder" class="form-control " value="<%= Model.ReportBuilder %>" />
        <%: Html.ValidationMessageFor(model => model.ReportBuilder) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Maximun Query Runtime -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Maximum Query Runtime (seconds)","Maximum Query Runtime (seconds)")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="MaxRunTime" name="MaxRunTime" class="form-control " value="<%=  Model.MaxRunTime > 0 ? Model.MaxRunTime.ToString() : "" %>" />
        <%: Html.ValidationMessageFor(model => model.MaxRunTime) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Limit -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Sensor Limit","Sensor Limit")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" id="SensorLimit" name="SensorLimit" class="form-control " value="<%=  Model.SensorLimit > 0 ? Model.SensorLimit.ToString() : "" %>" />
        <%: Html.ValidationMessageFor(model => model.SensorLimit) %>
    </div>
    <div class="clearfix"></div>
</div>

<!-- SQL -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.LabelFor(model => model.SQL) %>
    </div>
    <div class="col sensorEditFormInput form-floating">
        <%: Html.TextAreaFor(model => model.SQL, new { @class = "resizer form-control" }) %>
        <%: Html.ValidationMessageFor(model => model.SQL) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Schedule Type Availablity -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Schedule Type Availability","Schedule Type Availability")%>
    </div>
    <div class="col sensorEditFormInput d-flex flex-column">
        <label class="col-12">
            <input type="checkbox" <%=Model.RequiresPreAggs ? "checked='checked'":"" %> name="RequiresPreAggs" id="RequiresPreAggs" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Requires PreAggs","Requires PreAggs")%>&nbsp;
        </label>
        <label class="col-12">
            <input type="checkbox" <%=Model.ScheduleAnnually ? "checked='checked'":"" %> name="ScheduleAnnually" id="ScheduleAnnually" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Annually","Annually")%>&nbsp;
        </label>
        <label class="col-12">
            <input type="checkbox" <%=Model.ScheduleMonthly ? "checked='checked'":"" %> name="ScheduleMonthly" id="ScheduleMonthly" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Monthly","Monthly")%>&nbsp;
        </label>
        <label class="col-12">
            <input type="checkbox" <%=Model.ScheduleWeekly ? "checked='checked'":"" %> name="ScheduleWeekly" id="ScheduleWeekly" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Weekly","Weekly")%>&nbsp;
        </label>
        <label class="col-12">
            <input type="checkbox" <%=Model.ScheduleDaily ? "checked='checked'":"" %> name="ScheduleDaily" id="ScheduleDaily" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Daily","Daily")%>&nbsp;
        </label>
        <label class="col-12">
            <input type="checkbox" <%=Model.ScheduleImmediately ? "checked='checked'":"" %> name="ScheduleImmediately" id="ScheduleImmediately" />
            <%: Html.TranslateTag("Settings/_AdminReportBuilderForm|Once","Once")%>&nbsp;
        </label>
    </div>
    <div class="clearfix"></div>
</div>
<style>
    .resizer {
        width: 100%;
        height: 180px;
    }
</style>

<script type="text/javascript"> 
    $("#CustomerAccess").addClass("form-control");

    $(function () {
            <%if (Model.RequiresPreAggs)
    {%>$('#RequiresPreAggs').attr('checked', true);<%}
    if (Model.ScheduleAnnually)
    {%>$('#ScheduleAnnually').attr('checked', true);<%}
    if (Model.ScheduleMonthly)
    {%>$('#ScheduleMonthly').attr('checked', true);<%}
    if (Model.ScheduleWeekly)
    {%>$('#ScheduleWeekly').attr('checked', true);<%}
    if (Model.ScheduleDaily)
    {%>$('#ScheduleDaily').attr('checked', true);<%}
    if (Model.ScheduleImmediately)
    {%>$('#ScheduleImmediately').attr('checked', true);<%}%>

        $('#ReportScheduleSelection > label > input').click(function (e) {
            if (!e.target.checked) {
                $(e.target).removeAttr('checked');
            } else {
                $(e.target).attr('checked', true);
            }
        });

        $(".modal").click(function (e) {
            e.preventDefault();
            newModal($(this).html(), $(this).attr("href"), 500, 700);
        });

        $('.ajax').click(function (e) {
            e.preventDefault();
            $.get($(this).attr("href"), function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }

                window.location.href = window.location.href;
            });
        });
    });
</script>

<style>
    #SQL {
        max-width: 500px;
        min-height: 200px;
    }
</style>
