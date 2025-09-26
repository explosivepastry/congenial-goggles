<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OnlineOrder>" %>

<div class="cartSummary">
    <table>
        <tr>
            <td><%: Html.TranslateTag("Checkout/Summary|Total Items","Total Items") %>:</td>
            <td><%:Model.Items.Sum(oi=>{return oi.ItemQty;}) %></td>
        </tr>
        <tr>
            <td><%: Html.TranslateTag("Checkout/Summary|Total Amount","Total Amount") %>:</td>
            <td><%:Model.OrderTotal.ToString("C")%></td>
        </tr>
    </table>
</div>