<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReportQuery>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Report Query
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="fullForm" style="width: 100%;">

        <div class="formtitle">
            <div id="MainTitle">
                Edit Report Query
        <a href="/Report/AdminManagement" class="greybutton" style="margin-top: -5px;">Back to List</a>
            </div>
        </div>

        <div class="formBody" style="margin-top: 20px;">
            <form action="/Report/EditQuery/<%:Model.ReportQueryID > 0 ? Model.ReportQueryID.ToString() : "" %>" method="post">
                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                <%: Html.ValidationSummary(true) %>

                <%: Html.HiddenFor(model => model.ReportQueryID) %>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Name) %>
                </div>
                <div class="editor-field">
                    <input type="text" id="Name" name="Name" value="<%= Model.Name %>" />
                    <%: Html.ValidationMessageFor(model => model.Name) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.Description) %>
                </div>
                <div class="editor-field">
                    <input type="text" id="Description" name="Description" value="<%= Model.Description %>" />
                    <%: Html.ValidationMessageFor(model => model.Description) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.AccountID) %>
                </div>
                <div class="editor-field">
                    <input type="text" id="AccountID" name="AccountID" value="<%= Model.AccountID > 0 ? Model.AccountID.ToString() : ""%>" />

                    <%: Html.ValidationMessageFor(model => model.AccountID) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.AccountThemeID) %>
                </div>
                <div class="editor-field">
                    <input type="text" id="AccountThemeID" name="AccountThemeID" value="<%= Model.AccountThemeID > 0 ? Model.AccountThemeID.ToString() : ""%>" />

                    <%: Html.ValidationMessageFor(model => model.AccountThemeID) %>
                </div>

                <div class="editor-label">
                    Viewable By
                </div>
                <div class="editor-field">
                    <%:Html.DropDownList("CustomerAccess", Model.CustomerAccess) %>
                </div>

                <div class="editor-label">
                    Tags
                </div>
                <div class="editor-field">
                    <input type="text" name="Tags" placeholder="CFR|HSB" id="Tags" value="<%=Model.Tags %>" title="Pipe delimited values to restrict visibility. example: CFR|HSB|NewAccounts "/>
                </div>


                <div class="editor-label">
                    <%: Html.LabelFor(model => model.ReportBuilder) %>
                </div>
                <div class="editor-field">
                    <input type="text" id="ReportBuilder" name="ReportBuilder" value="<%= Model.ReportBuilder %>" />

                    <%: Html.ValidationMessageFor(model => model.ReportBuilder) %>
                </div>

                <div class="editor-label">
                    Maximum Query Runtime (seconds)
                </div>
                <div class="editor-field">
                    <input type="text" id="MaxRunTime" name="MaxRunTime" value="<%=  Model.MaxRunTime > 0 ? Model.MaxRunTime.ToString() : "" %>" />

                    <%: Html.ValidationMessageFor(model => model.MaxRunTime) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(model => model.SQL) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextAreaFor(model => model.SQL,10,80,null) %>
                    <%: Html.ValidationMessageFor(model => model.SQL) %>
                </div>

                <div style="clear: both;"></div>
                <div class="editor-label">
                    Schedule Type Availability
                </div>
                <div class="editor-field" id="ReportScheduleSelection">
                    <%foreach (string s in Enum.GetNames(typeof(eReportScheduleType)))
                      { %>
                    <label>
                        <input type="checkbox" name="Schedule<%:s%>" id="Schedule<%:s%>" value="true" />
                        <input type="hidden" name="Schedule<%:s%>" value="false" />
                        <%:s%>&nbsp;
                    </label>
                    <%}%>
                </div>
                <div>
                    <input type="submit" value="Save" class="bluebutton" />
                    <div style="clear: both;"></div>
                </div>
            </form>
        </div>
    </div>
    <%if (Model.ReportQueryID > 0 && MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      {
          bool alt = true; %>
    <div class="formtitle">Parameters</div>
    <table width="100%">
        <tr>
            <th>Type</th>
            <th>Name</th>
            <th>Label</th>
            <th>Help Text</th>
            <th>Default Value</th>
            <th style="width: 75px;"></th>
            <th style="width: 50px;"></th>
        </tr>

        <%foreach (ReportParameter parameter in Model.Parameters)
          {
              alt = !alt; %>
        <tr class='<%:alt ? "alt" : "" %>'>
            <td><%= ReportParameterType.Load(parameter.ReportParameterTypeID).Name %></td>
            <td><%= parameter.ParamName %></td>
            <td><%= parameter.LabelText %></td>
            <td><%= parameter.HelpText %></td>
            <td><%: parameter.DefaultValue %></td>
            <td>
                <a class="ajax" href="/Report/MoveParameter/<%:parameter.ReportParameterID %>?dir=Up">Move Up</a><br />
                <a class="ajax" href="/Report/MoveParameter/<%:parameter.ReportParameterID %>?dir=Down">Move Down</a>
            </td>
            <td>
                <a class="modal" href="/Report/EditParameter/<%:parameter.ReportParameterID %>">Edit</a>
                <br />
                <a class="deleteparameter" href="/Report/DeleteParameter/<%:parameter.ReportParameterID %>">Delete</a>
            </td>
        </tr>
        <%} %>
    </table>

    <div class="buttons" style="margin-top: 0px;">
        <a href="/Report/CreateParameter/<%:Model.ReportQueryID %>" class="modal bluebutton">New Parameter</a>
        &nbsp;
		<div style="clear: both;"></div>

    </div>
    <%}
      else
      {%>
    <div class="buttons" style="margin-top: 0px;">
        <a href="/Report/AdminManagement" class="greybutton">Cancel</a>
        &nbsp;
		<div style="clear: both;"></div>
    </div>
    <%} %>

    <script type="text/javascript">
        $(function () {
            <%if (Model.ScheduleAnnually)
              {%>$('#ScheduleAnnually').attr('checked', true);<%}
              if (Model.ScheduleMonthly)
              {%>$('#ScheduleMonthly').attr('checked', true);<%}
        if (Model.ScheduleWeekly)
        {%>$('#ScheduleWeekly').attr('checked', true);<%}
        if (Model.ScheduleDaily)
        {%>$('#ScheduleDaily').attr('checked', true);<%}
        if (Model.ScheduleImmediately)
        {%>$('#ScheduleOnce').attr('checked', true);<%}%>

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

	    $('.deleteparameter').click(function (e) {
	        e.preventDefault();
	        if (confirm("Are you sure you want to delete this report parameter?")) {
	            $.get($(this).attr("href"), function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }

	                window.location.href = window.location.href;
	            });
	        }
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
</asp:Content>
