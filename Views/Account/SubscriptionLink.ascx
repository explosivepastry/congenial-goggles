<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Account>" %>

<%if(Model.IsPremium){ %>
<td>Premiere Until
</td>
<td>
    <input value="<%: Model.CurrentSubscription.ExpirationDate.ToShortDateString()   %>" disabled='disabled' style='width: 70px; background-color: <%: Model.IsPremium ? "#C5FFBF" : "#FFBFBF" %>;' />
    <%=Model.CurrentSubscription.AccountSubscriptionType.Name %>
</td>
<%}else{ %>

<td></td>
<td>Contact Support to upgrade your account</td>
<%} %>
