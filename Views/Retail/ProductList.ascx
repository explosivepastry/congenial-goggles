<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PurchaseLinkStoreModel>" %>

<div class="col-12">
    <%if (Model.ProductList.Count == 0)
      {%>
          <%: Html.TranslateTag("Retail/PaymentOption|No Items Found.","No Items Found.")%>
    <%}
      else
      {
        foreach (ProductInfoModel product in Model.ProductList)
        {%>
            <div class="row" style="padding: 5px 15px;">
                <div class="col-5">
                    <%=product.DisplayName %>
                    <br />
                    <%=product.Description.Replace("(Selected)", "") %>
                </div>
                <div class="col-5">
                    <%=(product.Price - product.Discount).ToString("C") %> 
                    <br />
                </div>
                <div class="col-1">
                    <%
                        NameValueCollection queryDictionary = HttpUtility.ParseQueryString(Request.Url.Query);
                        string isChecked = queryDictionary["SKU"] == product.SKU ? "checked=checked" : "";
                    %>
                    &nbsp;&nbsp;&nbsp;<input type="radio" class="productChoiceRadio" name="productChoice" value="<%=product.ProductType %>_<%=product.SKU %>" <%=isChecked%>><br />
                    <label for="productChoice"><%: Html.TranslateTag("Select","Select")%></label>
                </div>
            </div>
            <hr />
            <div class="clearfix"></div>
      <%}
      }%>
</div>