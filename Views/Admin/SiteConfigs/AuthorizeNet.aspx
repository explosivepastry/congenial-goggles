<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AuthorizeNet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formtitle">Authorize.net</div>
    <form action="/Admin/EditSiteConfigs" method="post">
        <%: Html.Hidden("formName", "AuthorizeNet") %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
          { %>
        <div class="formBody">
            <table>
                <%if (!string.IsNullOrEmpty(ConfigData.AppSettings("AuthorizeID")))
                  { %>
                <tr>
                    <td>AuthorizeID</td>
                    <td>
                        <input type="text" name="AuthorizeID" value="<%: ConfigData.AppSettings("AuthorizeID")%>" /></td>
                </tr>
                <tr>
                    <td>AuthorizeKey</td>
                    <td>
                        <input type="text" name="AuthorizeKey" value="<%: ConfigData.AppSettings("AuthorizeKey") %>" /></td>
                </tr>
                <tr>
                    <td>AuthorizeTestMode</td>
                    <td>
                        <select name="AuthorizeTestMode">
                            <option value="False">False</option>
                            <option value="True" <%: ConfigData.AppSettings("AuthorizeTestMode").ToBool()? "selected=selected" : "" %>>True</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <%} %>
            </table>
        </div>
        <div class="buttons">
            <input type="submit" value="Save" class="bluebutton">
            <span style="color: red; padding: 15px; display: inline-block;"><%:ViewBag.Result ?? "" %></span>
            <div style="clear: both;"></div>
        </div>
        <%}%>
    </form>
</asp:Content>
