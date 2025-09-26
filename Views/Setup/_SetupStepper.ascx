<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<long>" %>

<%
    string id = Model.ToString();
    bool isGateway = false;

    Sensor sensor = Sensor.Load(Model);

    if (sensor != null)
        isGateway = false;

    else
    {
        Gateway gateway = Gateway.Load(Model);
        if (gateway != null)
            isGateway = true;
    }%>

<ul class="list-unstyled multi-steps shadow-sm rounded mt-4" id="rootWizard">
    <li class="<%:Request.Path.StartsWith("/Setup/AssignDevice") ? "is-active" : " " %>">
        <% if (Request.Path.Contains("/Setup/DefaultActions/"))
            { %>
        <a style="cursor: default;" href="#"><%: Html.TranslateTag("Add Device", "Add Device")%></a>
        <% }
            else
            {%>
        <a href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID%>?networkID=<%=MonnitSession.SensorListFilters.CSNetID%>"><%: Html.TranslateTag("|Add Device", "Add Device")%></a>
        <% } %>
    </li>

    <li class="<%:Request.Path.StartsWith("/Setup/SensorEdit/") || Request.Path.StartsWith("/Setup/GatewayEdit/") ? "is-active" : " " %>">
        <% if (Request.Path.Contains("Setup/AssignDevice"))
            { %>
        <a style="cursor: default;" href="#"><%: Html.TranslateTag("Setup", "Setup")%></a>
        <% }
            else
            {
                if (isGateway)
                {%>
        <a href="/Setup/GatewayEdit/<%=id + (string.IsNullOrWhiteSpace(Request.Params["reset"]) ? "" : "?reset=" + Request.Params["reset"]) %>"><%: Html.TranslateTag("Setup", "Setup")%></a>
        <%}
            else
            {%>
        <a href="/Setup/SensorEdit/<%=id%>"><%: Html.TranslateTag("Setup", "Setup")%></a>
        <%}
            }%>
    </li>

<%--    <li class="<%:Request.Path.StartsWith("/Setup/DefaultActions") ? "is-active" : " " %>">
        <% if (Request.Path.Contains("/Setup/DefaultActions/"))
            { %>
        <a style="cursor: default;" href="#"><%: Html.TranslateTag("Rules", "Rules")%></a>
        <% }
            else
            {%>
        <a href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID%>?networkID=<%=MonnitSession.SensorListFilters.CSNetID%>"><%: Html.TranslateTag("|Add Rule", "Add Rule")%></a>
        <% } %>
    </li>--%>

    <li class="<%:Request.Path.StartsWith("/Setup/StatusVerification") || Request.Path.StartsWith("/Setup/GatewayStatusVerification") ? "is-active" : " " %>">
        <% if (Request.Path.Contains("Setup/AssignDevice") || Request.Path.Contains("Setup/SensorEdit/"))
            { %>
        <a style="cursor: default;" href="#"><%: Html.TranslateTag("Validation", "Validation")%></a>
        <% }
            else
            {
                if (isGateway)
                {%>
        <a href="/Setup/GatewayStatusVerification/<%=id%>"><%: Html.TranslateTag("Validation", "Validation")%></a>
        <%}
            else
            {%>
        <a href="/Setup/StatusVerification/<%=id%>"><%: Html.TranslateTag("Validation", "Validation")%></a>
        <%}
            }%>
    </li>

    <li class="<%:Request.Path.StartsWith("/Setup/GatewayComplete/") || Request.Path.StartsWith("/Setup/SensorComplete/") ? "is-active" : " "%>">
        <a style="cursor: default;" href="#"><%: Html.TranslateTag("Complete","Complete")%></a>
    </li>
</ul>

<script>

</script>

<style>
    .is-active {
        font-weight: bold;
    }
</style>
