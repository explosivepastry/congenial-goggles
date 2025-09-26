<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Primary Server","Primary Server")%>
    </div>
    <div class="col sensorEditFormInput">
        <%:  Model.ServerHostAddress%>:<%: Model.Port%>
    </div>
</div>
<%if (Model.isEnterpriseHost)
  { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        
    </div>
    <div class="col sensorEditFormInput">
        <span style="color: red;"><%: Html.TranslateTag("Gateway/_Servers|Immediate gateway configuration update on gateway communication.","Immediate gateway configuration update on gateway communication.")%>
        </span>
        <%: Html.TranslateTag("Gateway/_Servers|This gateway has been configured to update its communication every time it talks to this server.","This gateway has been configured to update its communication every time it talks to this server.")%><%: Html.TranslateTag("Gateway/_Servers|If you want the gateway to communicate with this server press save to update the configuration and permit standard communication with this server.","If you want the gateway to communicate with this server press save to update the configuration and permit standard communication with this server.")%>
    </div>
</div>
<%} %>