<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Monnit.ReportQuery>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Report Management
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="fullForm" style="width: 100%;">

        <div class="formtitle">
            <div id="MainTitle">
                Admin Report Management
        <a href="/Report/CreateQuery" class="bluebutton" style="margin-top: -5px;">Create New</a>
            </div>
        </div>

        <div class="formTable">
            <table width="100%">
                <tr>
                    <th width="20px"></th>
                    <th>Name
                    </th>
                    <th>Description
                    </th>
                    <th>Account
                    </th>
                    <th>Theme
                    </th>
                    <th>Custom Build Class
                    </th>
                    <%--<th>
            <%: Html.DisplayNameFor(model => model.SQL) %>
        </th>--%>
                    <th></th>
                    <th width="20px"></th>
                </tr>

                <% bool alt = true;
                    foreach (var item in Model)
                    {
                        alt = !alt; %>
                <tr class='<%: alt ? "alt" : "" %>'>
                    <td></td>
                    <td>
                        <%=  item.Name %>
                    </td>
                    <td>
                        <%= item.Description %>
                    </td>
                    <td>
                        <%if (item.AccountID > 0)
                        { %>
                        <span title="<%: item.AccountID %>"><%= Account.Load(item.AccountID).CompanyName %></span>
                        <%} %>
                    </td>
                    <td>
                        <%if (item.AccountThemeID > 0)
                            {
                                AccountTheme theme = AccountTheme.Load(item.AccountThemeID); %>
                        <span title="<%: theme.Domain %>"><%:theme.Theme %></span>
                        <%} %>
                    </td>
                    <td>
                        <%: item.ReportBuilder %>
                    </td>
                    <%--<td>
            <%: Html.DisplayFor(modelItem => item.SQL) %>
        </td>--%>
                    <td>
                        <a href="/Report/EditQuery/<%:item.ReportQueryID %>">Edit</a> |
            <a class="delete" href="/Report/DeleteQuery/<%:item.ReportQueryID %>">Delete</a>
                    </td>
                    <td></td>
                </tr>
                <% } %>
            </table>

        </div>
    </div>

    <div class="buttons" style="margin-top: 0px;">
        &nbsp;
    <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        $(function () {
            $('.delete').click(function (e) {
                e.preventDefault();

                if (confirm("Are you sure you want to delete this report query?")) {
                    $.get($(this).attr("href"), function (data) {
                        if (data != "Success") {
                            console.log(data);
                            showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }

                    window.location.href = window.location.href;
                });
            }
        });
    });
    </script>
</asp:Content>
