<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Heartbeat Interval","Heartbeat Interval")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Gateway/_HeartBeat|How often the Gateway communicates with the server if no aware message is recorded.","How often the Gateway communicates with the server if no aware message is recorded.")%>
        <hr />
    </div>
</div>


