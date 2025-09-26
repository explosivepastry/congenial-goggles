<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountTheme>" %>

    <%//Account Contact List Section%>
    <% using (Html.BeginForm("EditAccountThemeContact", "AccountController"))
       { %>
    <%: Html.ValidationSummary(false) %>
    <%: Html.Hidden("AccountThemeID",Model.AccountThemeID) %>
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

    <%//Used as a spacer between%>

  

    <%//Adding Account Contact List%>
    <div class="formtitle">Account Contact List<a class="bluebutton" href="/Account/CreateAccountThemeContact/<%: Model.AccountThemeID %>" style="margin: -3px 18px 2px 0px;">Add Contact</a></div>
    <div id="forms" class="formbody">
        <%if(ViewBag.AccountThemeContactList != null){ %>
        <%var atcNumber = AccountThemeContact.LoadByAccountThemeID(Model.AccountThemeID); %>
        <%if (atcNumber.Count.ToLong() >= 1) 
          { %>
        <table style="width: 100%">
            <thead>
                <tr>
                    <th width="25px"></th>
                    <th width="250px">First Name</th>
                    <th width="250px">Last Name</th>
                    <th width="300px">Email</th>
                    <th width="300px">Other</th>
                    <th width="25px"></th>
                </tr>
            </thead>
            <%} %>
            <%} %>

            <tbody>
                <% if (ViewBag.AccountThemeContactList != null) {%>
                <% foreach (var item in AccountThemeContact.LoadByAccountThemeID(Model.AccountThemeID))
                   { %>
                <tr>
                    <td width="25px"></td>
                    <td><%= item.FirstName %></td>
                    <td><%= item.LastName %></td>
                    <td><%= item.Email %></td>
                    <td><%= item.Other %></td>
                    <td><a class="greybutton"   style="margin: 0px 0px 0px 0px;" href="/Account/EditAccountThemeContact/<%: item.AccountThemeContactID %>">Edit</a></td>
                    <td><a title="Delete" href="/Account/DeleteAccountThemeContact/<%: item.AccountThemeContactID %>"><img src="/Content/images/notification/trash.png" height="25" width="35"></a></td>
                    <td width="25px"></td>
                </tr>
                <%} %>
                <%} %>
            </tbody>

        </table>
    </div>
    <%}%>