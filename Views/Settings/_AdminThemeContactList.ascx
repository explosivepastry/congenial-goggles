<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountTheme>" %>

<%//Account Contact List Section%>
<% using (Html.BeginForm("EditAccountThemeContact", "AccountController"))
    { %>
<%: Html.ValidationSummary(false) %>
<%: Html.Hidden("AccountThemeID",Model.AccountThemeID) %>
<%--<% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>

<div id="forms" class="formbody">
    <%List<AccountThemeContact> contactList = AccountThemeContact.LoadByAccountThemeID(Model.AccountThemeID); %>
    <%if (contactList.Count() > 0)
        {%>

    <table class="table mx-1 my-1 table-hover">
        <thead>
            <tr>
                <th scope="col">Name</th>
                <th scope="col">Email</th>
                <th scope="col">Other</th>
                <th scope="col"></th>
            </tr>
        </thead>
        <tbody>
            <%foreach (var item in contactList)
                {%>
            <tr>
                <td><%= item.FirstName %>&nbsp;<%= item.LastName %></td>
                <td class="grey-container" style="width: fit-content;"><%= item.Email %></td>
                <td><%= item.Other %></td>
                <td>
                    <a title="<%: Html.TranslateTag("Delete","Delete")%>" href="/Settings/DeleteAccountThemeContact/<%: item.AccountThemeContactID %>">
                        <%=Html.GetThemedSVG("delete") %>
                    </a>
                </td>
            </tr>
            <%} %>
        </tbody>
    </table>
    <% }
        else
        {%>
    <h2><%: Html.TranslateTag("Settings/_AdminThemeContactList|No contacts found for this account.","No contacts found for this account.")%></h2>
    <%}%>
</div>
<%}%>