<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PaymentInfoModel>" %>

<div id="newPaymentDiv">
<form id="creditCardForm" method="post">
        <%string returns = (string)ViewBag.returnURL;
          string returnUrl2 = (string)ViewBag.ReturnURLAfterAdd;
          string sku = ViewBag.SKU;
          string qty = ViewBag.Quantity;

          if (!string.IsNullOrEmpty(returns))
          {%>
            <input type="hidden" name="returnURL" value="<%=returns %>" />
        <%}
          if (!string.IsNullOrEmpty(returnUrl2))
          {%>
            <input type="hidden" name="returnURL2" value="<%=returnUrl2 %>" />
        <%}
          if (!string.IsNullOrEmpty(sku))
          {%>
            <input type="hidden" id="hiddenPurchaseSku" name="sku" value="<%=sku %>" />
        <%}
          if (!string.IsNullOrEmpty(qty))
          {%>
            <input type="hidden" id="hiddenPurchaseQty" name="qty" value="<%=qty %>" />
        <%}%>

        <div class="payment">
            <div class=" col-md-6 col-12">
                <div class="form-group owner">
                    <label for="owner"><%: Html.TranslateTag("Retail/PaymentOption|Card Holder","Card Holder")%></label>
                    <input placeholder="<%: Html.TranslateTag("Retail/PaymentOption|John Doe","John Doe")%>" type="text" class="form-control user-dets w-90" name="cardHolder" id="cardHolder">
                </div>
                <div class="form-group">
                </div>
                <div class="form-group" id="card-number-field">
                    <label for="cardNumber"><%: Html.TranslateTag("Retail/PaymentOption|Card Number","Card Number")%></label>
                    <input type="tel" class="form-control user-dets w-90" name="cardNumber" id="cardNumber">
                </div>
                <div class="form-group" id="expiration-date">
                    <label><%: Html.TranslateTag("Expiration Date","Expiration Date")%></label>
                    <div class="dfac w-90" style="justify-content:space-between; flex-wrap:wrap; gap:10px;">
                        <select class="form-select user-dets"  id="expirationMonth" name="expirationMonth">
                            <option value="01"><%: Html.TranslateTag("January","January")%></option>
                            <option value="02"><%: Html.TranslateTag("February","February")%> </option>
                            <option value="03"><%: Html.TranslateTag("March","March")%></option>
                            <option value="04"><%: Html.TranslateTag("April","April")%></option>
                            <option value="05"><%: Html.TranslateTag("May","May")%></option>
                            <option value="06"><%: Html.TranslateTag("June","June")%></option>
                            <option value="07"><%: Html.TranslateTag("July","July")%></option>
                            <option value="08"><%: Html.TranslateTag("August","August")%></option>
                            <option value="09"><%: Html.TranslateTag("September","September")%></option>
                            <option value="10"><%: Html.TranslateTag("October","October")%></option>
                            <option value="11"><%: Html.TranslateTag("November","November")%></option>
                            <option value="12"><%: Html.TranslateTag("December","December")%></option>
                        </select>
                        <select class="form-select user-dets" id="expirationYear" name="expirationYear">
                            <%for (int year = DateTime.UtcNow.Year; year < (DateTime.UtcNow.Year + 10); year++)
                              {%>
                            <option value="<%=year.ToString() %>"><%=year %></option>
                            <%} %>
                        </select>
                    </div>
                </div>
                <div class="form-group" style="padding-top: 10px;">
                    <ul style="color: red; font-size: 1.1em; font-weight: bold;" id="cardMessage"></ul>
                </div>
            </div>
            <div class="col-md-6 col-12">
                <div class="form-group">
                    <label for="address1"><%: Html.TranslateTag("Address 1","Address  ")%>1</label>
                    <input type="text" class="form-control user-dets " name="address1" id="address1">
                </div>
                <div class="form-group">
                    <label for="address2"><%: Html.TranslateTag("Address 2","Address  ")%>2</label>
                    <input type="text" class="form-control user-dets" name="address2" id="address2">
                </div>
                <div class="form-group">
                    <label for="city"><%: Html.TranslateTag("City","City")%></label>
                    <input type="text" class="form-control user-dets" name="city" id="city">
                </div>
                <div class="form-group">
                    <label for="country"><%: Html.TranslateTag("Country","Country")%></label>
                    <select class="form-select user-dets" name="country" id="country">
                        <%foreach (Country c in Country.LoadAll())
                          { %>
                        <option <%=c.Name == "United States" ? "selected='selected'" :"" %> value="<%=c.ISOCode %>"><%=c.Name %></option>
                        <%} %>
                    </select>
                </div>
                <div class="form-group ">
                    <div class="" style="padding-left: 0px">
                        <label for="divStates"><%: Html.TranslateTag("State","State")%> / <%: Html.TranslateTag("Province","Province")%></label>
                        <div id="divStates"></div>
                        <div class="spinner-border spinner-border-sm text-primary" id="statesLoading" role="status">
                          <span class="visually-hidden"><%: Html.TranslateTag("Loading...","Loading...")%></span>
                        </div>
                    </div>
                    <div class="">
                        <label for="zipcode"><%: Html.TranslateTag("Retail/LoginToStore|Postal Code","Postal Code")%></label>
                        <input type="text" class="form-control user-dets" name="zipcode" id="zipcode">
                    </div>
                       <div class="clearfix"></div>
                </div>
            </div>
            <div class="form-group">
            </div>
 <%--           <div class="form-group" id="pay-now">
                
                <div class="col-12 text-end" style="padding-top: 10px;">
                <div class="spinner-border spinner-border-sm text-primary" id="saveLoading" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <button type="button" class="btn btn-primary" id="purchase">
                    <span><%: Html.TranslateTag("Save","Save")%></span>
                </button>
                </div>
            </div>--%>

            <div class="form-group text-end" id="pay-now">
                <button type="submit" id="saveCC"  onclick="$(this).hide();$('#CCsaving').show();" value="<%: Html.TranslateTag("Save","Save") %>" class="btn btn-primary">
                    <%: Html.TranslateTag("Save","Save") %>
                </button>
                <button type="button" id="CCsaving" class="btn btn-primary" style="display: none;"  disabled>
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                    <%: Html.TranslateTag("Saving...","Saving...") %>
                </button>
            </div>
        </div>
    </form>
    <div id="newCardMsg"></div>
</div>

<script type="text/javascript">
    $('#statesLoading').hide();
    /*$('#saveLoading').hide();*/

    $(function () {
        $.get("/Retail/StatesByCountry/" + $('#country').val(), function (data) {
            $('#divStates').html(data);
            $('#state').removeClass('form-control tzSelect');
            $('#state').addClass('form-select');
            $('#state').removeAttr('style');
        });


        $('#country').change(function () {
            $('#divStates').html("");
            $('#statesLoading').show();
            $.get("/Retail/StatesByCountry/" + $('#country').val(), function (data) {
                $('#statesLoading').hide();
                $('#divStates').html(data);
                $('#state').removeClass('form-control tzSelect');
                $('#state').addClass('form-select');
                $('#state').removeAttr('style');

                if ($('#state option').length == 0) {
                    $('#state').parent().parent().hide();
                }
            });
        });
    });

</script>