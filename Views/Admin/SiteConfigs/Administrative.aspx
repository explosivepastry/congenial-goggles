<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administrative
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formtitle">Administrative</div>
    <form action="/Admin/EditSiteConfigs" method="post">

        <%:  Html.Hidden("formName", "Administrative")%>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
          { %>
        <div class="formBody">
            <table style="width: 100%;">
                <tr>
                    <td>Admin AccountID</td>
                    <td>
                        <input type="text" name="AdminAccountID" value="<%: ConfigData.AppSettings("AdminAccountID")%>" /></td>
                </tr>
                <tr>
                    <td>Default NetworkID</td>
                    <td>
                        <input type="text" name="DefaultCSNetID" value="<%: ConfigData.AppSettings("DefaultCSNetID")%>" /></td>
                </tr>
                <!-- force these to to one and take them out if it is enterprise -->
                <tr>
                    <td>Min Password Length</td>
                    <td>
                        <input type="text" name="MinPasswordLength" value="<%: ConfigData.AppSettings("MinPasswordLength")%>" /></td>
                </tr>
                <tr>
                    <td>New Account Notification Email</td>
                    <td>
                        <input type="text" name="NewAccountNotificationEmail" value="<%: ConfigData.AppSettings("NewAccountNotificationEmail")%>" /></td>
                </tr>
                <tr>
                    <td>Look Up Host</td>
                    <td>
                        <input type="text" name="LookUpHost" value="<%: ConfigData.AppSettings("LookUpHost")%>" /></td>
                </tr>
                <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                   { %>

                <tr>
                    <td>Current EULA Version</td>
                    <td>
                        <input type="text" name="EULA" value="<%: ConfigData.AppSettings("EULA")%>" /></td>
                </tr>
                <tr>
                    <td>Current Firmware Version</td>
                    <td>
                        <input type="text" name="DefaultFirmwareVersion" value="<%: ConfigData.AppSettings("DefaultFirmwareVersion")%>" /></td>
                </tr>
                <tr>
                    <td>Current Gateway Version</td>
                    <td>
                        <input type="text" name="DefaultGatewayVersion" value="<%: ConfigData.AppSettings("DefaultGatewayVersion")%>" /></td>
                </tr>
                <%} %>

                 <tr>
                    <td>Twilo Account ID</td>
                    <td>
                        <input type="text" name="TwilioAccountSid" value="<%: ConfigData.AppSettings("TwilioAccountSid")%>" /></td>
                </tr>
                 <tr>
                    <td>Twilo Account Auth Token</td>
                    <td>
                        <input type="text" name="TwilioAuthToken" value="<%: ConfigData.AppSettings("TwilioAuthToken")%>" /></td>
                </tr>
                 <tr>
                    <td>Twilo Base API</td>
                    <td>
                        <input type="text" name="TwilioBaseAPI" value="<%: ConfigData.AppSettings("TwilioBaseAPI")%>" /></td>
                </tr>
                 <tr>
                    <td>Twilo Call CallBack</td>
                    <td>
                        <input type="text" name="TwilioCallCallback" value="<%: ConfigData.AppSettings("TwilioCallCallback")%>" /></td>
                </tr>
                 <tr>
                    <td>Twilo Call Resource</td>
                    <td>
                        <input type="text" name="TwilioCallResource" value="<%: ConfigData.AppSettings("TwilioCallResource")%>" /></td>
                </tr>
                 <tr>
                    <td>Twilio Call Url</td>
                    <td>
                        <input type="text" name="TwilioCallUrl" value="<%: ConfigData.AppSettings("TwilioCallUrl")%>" /></td>
                </tr>
                <tr>
                    <td>Twilio Message Callback</td>
                    <td>
                        <input type="text" name="TwilioMessageCallback" value="<%: ConfigData.AppSettings("TwilioMessageCallback")%>" /></td>
                </tr>
                <tr>
                    <td>Twilio Message Resource</td>
                    <td>
                        <input type="text" name="TwilioMessageResource" value="<%: ConfigData.AppSettings("TwilioMessageResource")%>" /></td>
                </tr>
                <!-- take out current firmware/gateway versions -->
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
        </div>
        <div class="buttons">
            <input type="submit" value="Save" class="bluebutton">
            <span style="color: red; padding: 15px; display: inline-block;">
                <%: ViewBag.Result  %>

            </span>
            <div style="clear: both;"></div>
        </div>
        <%}%>
    </form>
</asp:Content>
