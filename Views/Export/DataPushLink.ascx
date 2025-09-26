<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExternalDataSubscription>" %>

<div class="view-btns deviceView_btn_row rounded" style="margin-top:15px;margin-bottom:30px;">

    <a href="/Export/DataPushInfo/<%=Model.ExternalDataSubscriptionID %>" class="deviceView_btn_row__device" >
        <div class="btn-<%:Request.Path.StartsWith("/Export/DataPushInfo")?"active-fill":"secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("info") %><span class="extra">&nbsp<%: Html.TranslateTag("Export/DataPushLink|API Info","API Info")%></span></div></a>



    <a href="/Export/DataPushHistory/<%:Model.ExternalDataSubscriptionID %>" class="deviceView_btn_row__device">
        <div class=" btn-<%:Request.Path.Contains("History")?"active-fill":"secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("list") %><span class="extra">&nbsp<%: Html.TranslateTag("History","History")%></span></div></a>

    <%if (Model.ExternalDataSubscriptionID > 0)
      { %>
    <a href="/Export/DataPushEdit/<%:Model.ExternalDataSubscriptionID %>" class="deviceView_btn_row__device" >
        <div class="btn-<%:Request.Path.Contains("Configure")?"active-fill":"secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("edit") %><span class="extra">&nbsp<%: Html.TranslateTag("Edit","Edit")%></span></div></a>
    <%}
      else
      { %>

    <a href="/Export/DataWebhook/" class="deviceView_btn_row__device" >
        <div class="btn-<%:(Request.Path.StartsWith("/Export/DataWebhook/") ||Request.Path.Contains("Configure") )?"active-fill":"secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("dataPush") %><span class="extra">&nbsp<%: Html.TranslateTag("Create","Create")%></span></div></a>

    <%} %>

    <a href="/Export/DataPushNotification/" class="deviceView_btn_row__device" >
        <div class="btn-<%:Request.Path.StartsWith("/Export/DataPushNotification")?"active-fill":"secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("dataPushNotification") %><span class="extra">&nbsp<%: Html.TranslateTag("Notification","Notification")%></span></div></a>
</div>



