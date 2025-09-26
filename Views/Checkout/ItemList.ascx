<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OnlineOrder>" %>


<table style="width: 600px;">
    <tr>
        <th style="width: 60px;"><%: Html.TranslateTag("Checkout/ItemList|Qty","Qty") %></th>
        <th><%: Html.TranslateTag("Checkout/ItemList|Item","Item") %></th>
        <th style="width: 60px; text-align: right;"><%: Html.TranslateTag("Checkout/ItemList|Discounts","Discounts") %></th>
        <th style="width: 60px; text-align: right;"><%: Html.TranslateTag("Checkout/ItemList|Price","Price") %></th>
    </tr>
    <%foreach (OrderItem item in Model.Items)
        { %>
    <tr class="cartItem">
        <td>
            <input id="<%:item.OrderItemID %>" class="cartItemQty" value="<%:item.ItemQty %>" style="width: 30px;" />
            <%if (item.ItemQty == 0)
                { %>
            <a class="cartItemDelete" href="Checkout/Remove/<%:item.OrderItemID %>">
                <img src="/Content/images/delete.png" />
            </a>
            <%} %>
        </td>
        <%--<td title="<%:item.ItemDescription %>"><%:item.ItemName %></td><%:item.Product.Thumbnail %>--%>
        <td title="<%:item.Product.SKU %>">
            <table style="width: 100%;">
                <tr>
                    <td rowspan="2" style="width: 36px;">
                        <img src="<%=string.IsNullOrEmpty(item.Product.Thumbnail) ? "/content/images/product/generic.png" : item.Product.Thumbnail %>" style="width: 36px; height: 36px;" /></td>
                    <td><%=item.ItemName %></td>
                </tr>
                <tr>
                    <td class="small"><%=item.ItemDescription %></td>
                </tr>
            </table>
        </td>
        <td style="text-align: right;"><%=item.ItemTotalAdjustment > 0 ? item.ItemTotalAdjustment.ToString("C") : ""%></td>
        <td style="text-align: right;"><%=(item.ItemPrice * item.ItemQty - item.ItemTotalAdjustment).ToString("C") %></td>
    </tr>
    <%} %>
</table>
<div class="buttons" style="margin: 10px -10px -10px -10px;">
    <a href="/Checkout/Shipping" class="proceed bluebutton"><%: Html.TranslateTag("Checkout/ItemList|Proceed to Checkout","Proceed to Checkout") %></a>
    <div style="clear: both;"></div>
</div>


<script type="text/javascript">

    $(document).ready(function () {

        $('.proceed').click(function (e) {
            e.preventDefault();

            //refreshSummary();
            getMain($(this).attr('href'), 'Checkout', true);
        });

        $('.cartItemQty').change(function (e) {
            $.get('/Checkout/Update/' + $(this).attr("id") + '?qty=' + $(this).val(), function (data) {
                if (data != "Success") {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                } else {
                    refreshSummary();
                    getMain();
                }
            });
        });

        $('.cartItemDelete').click(function (e) {
            e.preventDefault();

            if (confirm('Are you sure you want to remove this item from your cart?')) {
                $.get($(this).attr("href"), function (data) {
                    if (data != "Success") {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    } else {
                        getMain();
                        refreshSummary();
                    }
                });
            }
        });
    });
</script>
