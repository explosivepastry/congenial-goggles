<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%--<div class="form-group">
    <div class="bold col-md-3 col-sm-3 col-xs-12">
        <%if (Model.ApplicationID != 23 || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) > new Version("1.2.0.3"))
            { %>
        <%: Html.TranslateTag("Gateway Check-in Frequency","Gateway Check-in Frequency")%> (<%:Html.TranslateTag("Minutes") %>)
        <%}
            else if (Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) < new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) <= new Version("1.2.0.3"))
            { %>
    Time Before Motion Rearm
    <%} %>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 mdBox">

        <input class="aSettings__input_input" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="ReportInterval" id="ReportInterval" value="<%=Model.ReportInterval %>" />
        <a id="reportNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
    </div>
</div>--%>
