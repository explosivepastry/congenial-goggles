<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SMTPSendEmail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form action="/Admin/EditSiteConfigs" method="post">
    <%: Html.Hidden("formName", "GatewayServerBehavior")  %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <%if(MonnitSession.IsCurrentCustomerMonnitAdmin){ %>
    <table style="width: 100%;">
        <tr>
            <td>Addresses to listen on<br>
                (Any:3000 or 192.168.0.2:3000)
            </td>
            <td>
                <input type="text" name="Addresses" value="<%: ConfigData.AppSettings("Addresses") %>"></td>
        </tr>
        <tr>
            <td>Inbound Packet Retention<br>
                (None|All|{GatewayID})
            </td>
            <td>
                <input type="text" name="InboundSettings" value="<%: ConfigData.AppSettings("InboundSettings") %>"></td>
        </tr>
        <tr>
            <td>Outbound Packet Retention<br>
                (None|All|{GatewayID})
            </td>
            <td>
                <input type="text" name="OutboundSettings" value="<%: ConfigData.AppSettings("OutboundSettings") %>"></td>
        </tr>
    </table>
    <%} %>
    <div class="buttons">
        <input type="submit" value="Save" class="bluebutton">
        <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? "" %></span>
        <div style="clear: both;"></div>
    </div>
    </form>
</asp:Content>
