<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cellular Usage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formtitle">Cellular Usage API</div>
    <form action="/Admin/EditSiteConfigs" method="post">
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <%:  Html.Hidden("formName", "CellularUsage")%>
    <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      { %>
    <div class="formBody">
    <table>
        <%if (!string.IsNullOrEmpty(ConfigData.AppSettings("AuthorizeID")))
          { %>
        <tr>
            <td>API Base Address</td>
            <td>
                <input type="text" name="MEA_API_Location" value="<%: ConfigData.AppSettings("MEA_API_Location")%>" /></td>
        </tr>
        <tr>
            <td>Auth Token</td>
            <td>
                <input type="text" name="MEA_API_Auth_Guid" value="<%: ConfigData.AppSettings("MEA_API_Auth_Guid")%>" /></td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <%} %>
    </table>
    </div>
    <div class="buttons">
        <input type="submit" value="Save" class="bluebutton"/>
        <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? ""%></span>
        <div style="clear: both;"></div>
    </div>
    <%}%>
        </form>
</asp:Content>
