<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Gateway/_GatewayName|Gateway Name","Gateway Name")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_GatewayName|The unique name you give the Gateway to easily identify it in a list and in any notifications.","The unique name you give the Gateway to easily identify it in a list and in any notifications.")%>
        <hr />
    </div>
</div>



