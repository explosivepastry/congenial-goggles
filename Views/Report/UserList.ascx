<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.Customer>>" %>

    <table border="0">
    <%foreach (Customer item in Model.OrderBy(c=>c.FullName)) { %>
        <tr id="UserList<%:item.CustomerID %>">
            <td width="20">
                <input type="checkbox" id="customerID_<%:item.CustomerID %>" />
            </td>
            <td>
                <label for="customerID_<%:item.CustomerID %>"><%= item.FullName%></label>
            </td>
        </tr>
    <% } %>
    </table>
