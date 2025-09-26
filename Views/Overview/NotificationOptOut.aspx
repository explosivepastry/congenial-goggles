<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Notification Opt Out
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="msg_container" id="fullForm" style="margin-top:1rem">
        <div  class="card_container__top__title" style="margin:1rem .5rem 0 1rem;"><%: Html.TranslateTag("Overview/NotificationOptOut|Successfully Unsubscribed","Successfully Unsubscribed")%></div>
        <div class="formBody"style="padding:1rem;">
            <div class="susub-text"><%: Html.TranslateTag("Overview/NotificationOptOut|Your email address will be blocked from receiving future sensor notifications","Your email address will be blocked from receiving future sensor notifications")%>.</div><br />
            <div class=""><%: Html.TranslateTag("Overview/NotificationOptOut|Please note it can take up to 12 hours for this to take effect on all servers","Please note it can take up to 12 hours for this to take effect on all servers")%>.</div>
        </div>
        
        <div class="buttons">
            &nbsp;
            <div style="clear: both;"></div>
        </div>
    </div>

</asp:Content>
