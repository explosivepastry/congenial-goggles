<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Automated Emails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formtitle">Automated Emails</div>
    <form action="/Admin/EditSiteConfigs" method="post">
        <%: Html.Hidden("formName", "AutomatedEmails")%>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin) { %>
        <div class="formBody">
            <table>

                <tr>
                    <td>Send Inactivity Notifications</td>
                    <td>
                        <select name="SendInactivityNotifications">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("SendInactivityNotifications").ToBool() ? "selected=selected" : ""%>>True</option>
                        </select>
                    </td>
                </tr>

                <tr>
                    <td>Send Subscription Notifications</td>
                    <td>
                        <select name="SendSubscriptionNotifications">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("SendSubscriptionNotifications").ToBool() ? "selected=selected" : ""%>>True</option>
                        </select>
                    </td>
                </tr>

                <tr>
                    <td>Send Maintenance Notifications</td>
                    <td>
                        <select name="SendMaintenanceNotifications">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("SendMaintenanceNotifications").ToBool() ? "selected=selected" : ""%>>True</option>
                        </select>
                    </td>
                </tr>

                <% if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                   { %>
                <tr>
                    <td>Send Advertisements</td>
                    <td>
                        <select name="SendAds">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("SendAds").ToBool() ? "selected=selected" : ""%>>True</option>
                        </select>
                    </td>
                </tr>

                <tr>
                    <td>Send Scheduled Reports</td>
                    <td>
                        <select name="RunScheduledReports">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("RunScheduledReports").ToBool() ? "selected=selected" : ""%>>True</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Report Processing Plug-in Folder</td>
                    <td>
                        <input type="text" name="ReportProcessingPluginPath" value="<%: ConfigData.AppSettings("ReportProcessingPluginPath")%>" /></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <%} %>
            </table>
        </div>
        <div class="buttons">
            <input type="submit" value="Save" class="bluebutton">
            <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? ""%></span>
            <div style="clear: both;"></div>
        </div>
        <%}%>
    </form>
</asp:Content>
