<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form action="/Admin/EditSiteConfigs" method="post">
        <%: Html.Hidden("formName", "ExceptionLogging")%>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
          { %>
        <table>
            <tr>
                <th colspan="2">Exception Logging</th>
            </tr>
            <tr>
                <td>Log Unknown Gateways</td>
                <td>
                    <select name="LogUnknownGatewaysAsException">
                        <option value="False">False</option>
                        <option value="True" <%: ConfigData.AppSettings("LogUnknownGatewaysAsException").ToBool() ? "selected=selected" : ""%>>True</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>Log External Subscription</td>
                <td>
                    <select name="LogExternalSubscriptionAsException">
                        <option value="False">False</option>
                        <option value="True" <%: ConfigData.AppSettings("LogExternalSubscriptionAsException").ToBool() ? "selected=selected" : ""%>>True</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>Log Bad Web Path</td>
                <td>
                    <select name="LogBadPathAsException">
                        <option value="False">False</option>
                        <option value="True" <%: ConfigData.AppSettings("LogBadPathAsException").ToBool() ? "selected=selected" : ""%>>True</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
        </table>
        <div class="buttons">
            <input type="submit" value="Save" class="bluebutton">
            <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? ""%></span>
            <div style="clear: both;"></div>
        </div>
        <%}%>
    </form>
</asp:Content>
