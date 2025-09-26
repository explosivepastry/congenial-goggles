<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.AccountTheme>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateChangeAccountTheme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
    <div class="formtitle">
        CreateChangeAccountTheme
        <a class="bluebutton" href="/Admin/AccountTheme" style="margin: -3px 20px 0px 0px;">Create New Theme</a>
    </div>

    <% using (Html.BeginForm())
       {
           bool alt = true;
           bool ShowDNS = Request.QueryString["DNS"].ToBool();
    %>

    <table style="width: 100%">
        <thead>
            <tr>
                <th style="width: 20px;"></th>
                <th>AccountThemeID</th>
                <th>AccountID</th>
                <th>Theme</th>
                <th>Domain <a href="/Admin/CreateChangeAccountTheme?DNS=true">DNS Resolution</a></th>
                <th>Current EULA</th>
                <th></th>
                <th style="width: 20px;"></th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var theme in Model)
               {
                   alt = !alt;
            %>
            <tr class="<%: alt ? "alt" : "" %>">
                <td></td>
                <td>
                    <%: theme.AccountThemeID %>
                </td>
                <td>
                    <%: theme.AccountID %>
                </td>
                <td>
                    <%: theme.Theme %>
                </td>
                <td>
                    <%: theme.Domain %> 
                    <%if(ShowDNS){
                          string ip = "Unknown";
                          string color = "#FF0000";
                          try
                          {
                              ip = System.Net.Dns.GetHostAddresses(theme.Domain)[0].ToString();
                              if (ip.Contains("25"))
                                  color = "#555555";
                          }
                          catch { } 
                          %>
                        (<span style="color:<%:color%>"><%:ip %></span>) 
                    <%} %>
                </td>
                <td>
                    <%: theme.CurrentEULA %>
                </td>
                <td style="text-align: right">
                    <a href="/Admin/AccountTheme/<%:theme.AccountThemeID %>">Edit</a> |
                    <a href="/Email/EmailTemplateOverview/<%:theme.AccountID %>">Email Template</a>
                    <a href="/Admin/ThemePreference/<%:theme.AccountThemeID %>">Preferences</a>
                </td>
                <td></td>
            </tr>

            <%} %>
        </tbody>
    </table>

    <div class="buttons">
        <div style="clear: both;"></div>
    </div>
    <%} %>
</asp:Content>
