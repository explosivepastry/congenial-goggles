<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<PurchaseLinkStoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PurchasePreview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid mt-4">
        <div class="row">
            <%--<%if (MonnitSession.CurrentStoreLinkInfo != null)
                { %>
            <%--<%Html.RenderPartial("~/Views/Retail/StoreHeaderLink.ascx", Model.account); %>-%>
            <%Html.RenderPartial("~/Views/Retail/StoreAccountInfo.ascx", Model.account); %>
            <%} %>--%>
            <div class="col-12 col-md-6 ps-0">
                <div class="x_panel gridPanel shadow-sm rounded">
                    <div class="x_content">
                        <div class="x_title">
                            <h2 style="max-width: 90%; overflow: unset; font-weight: bold"><%: Html.TranslateTag("Retail/PaymentOption|Purchase","Purchase")%></h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="row ps-3" style="font-size: 1em;">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4">
                                <strong><%=Model.PaymentInfoModelList[0].CustomerName %></strong>
                                <br />
                                <span style="color: grey; vertical-align: text-top;">xxxx-xxxx-xxxx-<%=Model.PaymentInfoModelList[0].CardNumber.Remove(0,(Model.PaymentInfoModelList[0].CardNumber.Length - 4) )%></span>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                                <b><%: Html.TranslateTag("Product","Product")%>: </b><%=Model.ProductList[0].DisplayName %>
                                <br />
                                <b><%: Html.TranslateTag("SKU","SKU")%>: </b><%=Model.ProductList[0].SKU %>
                                <br />
                                <b><%: Html.TranslateTag("Quantity","Quantity")%>: </b><%=Model.Quantity%>
                            </div>
                        </div>
                        <hr />
                        <div class="clearfix"></div>
                        <div class="row">
                            <div class="col-12">
                                <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;" id="itemPrice" >
                                    <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                        <%: Html.TranslateTag("Price","Price")%>:
                                    </span>
                                    <span style="font-size: 1.2em; font-weight: 600;">
                                        <%double itemPrice = (Model.ProductList[0].Price - Model.ProductList[0].Discount) * Model.Quantity; %>
                                        <%=itemPrice.ToString("C")  %>
                                    </span>
                                </div>

                                <%
                                    double prorateAmount = ViewBag.ProrateAmount != null ? ViewBag.ProrateAmount : 0;

                                    if(prorateAmount > 0)
                                    {%>
                                    <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;" id="proratePrice" >
                                        <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                            <%: Html.TranslateTag("Discount","Discount**")%>:
                                        </span>
                                        <span style="font-size: 1.2em; font-weight: 600;">
                                            <%=(0-prorateAmount).ToString("C")  %>
                                        </span>
                                    </div>
                                  <%} %>


                                <div class="col-12" style="padding-top: 5px; text-align: left; display:flex;justify-content:space-between; padding:0 10px;" id="tax" data-value="<%=Model.ProductList[0].Tax%>">
                                    <span style="font-size: 1.2em; font-weight: 600; color: grey;">
                                        <%: Html.TranslateTag("Tax","Tax")%>:
                                    </span>
                                    <span style="font-size: 1.2em; font-weight: 600;">
                                        <%=Model.ProductList[0].Tax.ToString("C")%>
                                    </span>
                                </div>
                                <div class="col-12" style="padding-top: 5px; text-align: left;background:#eee; display:flex;justify-content:space-between; padding:5px 10px;" id="price">
                                    <span style="font-size: 1.5em; font-weight: bold; color: #444;">
                                        <%: Html.TranslateTag("Total","Total")%>:
                                    </span>&nbsp;
                                    <span style="font-size: 1.5em; font-weight: bold; color: #444;">
                                        <%=(itemPrice - prorateAmount + Model.ProductList[0].Tax).ToString("C")  %>
                                    </span>
                                </div>
                                <div class="col-3" id="waitSpinner" style="padding-left: 5px; font-size: 1.2em; font-weight: bold;">
                                </div>
                            </div>
                            <div style="text-align: end; padding-right:10px;" class="col-12">
                                <span style="float:left;">
                                    <h4><%: Html.TranslateTag("To be emailed Invoice/Receipt","To be emailed Invoice/Receipt")%>*:</h4>
                                    <input id="toBeEmailed" type="text" value="<%=MonnitSession.CurrentCustomer.NotificationEmail %>">
                                    <br/>
                                    <strong style="font-size: 12px;">*You can enter multiple email addresses by separating them with a semicolon.</strong>
                                    <%if(prorateAmount > 0)
                                    {
                                        int monthDifference = ViewBag.MonthDifference != null ? ViewBag.MonthDifference : 0;%>
                                        <br />
                                        <strong style="font-size: 12px;">
                                            **<%:Html.TranslateTag("Retail/PurchasePreview|Premiere Upgrade Prorate", 
                                                                   "Because you are upgrading your plan, we've prorated your current plan and applied a discount to your new purchase.")%>
                                            <br />
                                            <%:Html.TranslateTag("Retail/PurchasePreview|Premiere Upgrade Prorate Months remaining", "Months remaining: ") + monthDifference%>
                                            <br />
                                            <%:Html.TranslateTag("Retail/PurchasePreview|Premiere Upgrade Prorate Amount", "Discount Applied: " ) + prorateAmount.ToString("C")%>
                                        </strong>
                                  <%} %>
                                </span>
                                <button id="purchaseButton" onclick="purchaseProduct(<%=Model.PaymentInfoModelList[0].ProfileID%>,'<%=Model.ProductList[0].SKU %>');" style="margin-top: 5px;" type="button" class="btn btn-info"><i class="fa fa-lock"></i>&nbsp;<%: Html.TranslateTag("Purchase","Purchase")%></button>
                                
                                <a id="updatePaymentBtn" style="display: none;" href="/Retail/PremiereCredit/<%=Model.account.AccountID%>">
                                    <button style="margin-top: 5px;" type="button" class="btn btn-info">
                                        <i class="fa fa-lock"></i>&nbsp;
                                        <%: Html.TranslateTag("Change Payment","Change Payment")%>
                                    </button>
                                </a>
                            </div>
                        </div>
                        <div class="row" id="purchaseMessage"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var noBackMessage = '<%: Html.TranslateTag("Retail/PaymentOption|Authorizing: Do not click \"Back\", or Refresh page.","Authorizing: Do not click \"Back\", or Refresh page.")%>';
        var successMessage = '<%: Html.TranslateTag("Retail/PaymentOption|Success! You will receive an Email with your transaction receipt","Success! You will receive an Email with your transaction receipt")%>';
        function purchaseProduct(profileid, SKU) {
            $('#purchaseButton').hide();
            $('#purchaseMessage').html(noBackMessage);
            $('#waitSpinner').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
            var tax = $('#tax').attr('data-value');
            var qty = '<%=Model.Quantity%>';
            var toBeEmailed = $('#toBeEmailed').val();

            $.post("/Retail/ProcessPurchase/<%=Model.account.AccountID%>", { profileID: profileid, sku: SKU, tax: tax, qty: qty, toBeEmailed: toBeEmailed }, function (data) {
                if (data.includes("Success")) {
                    if (qty > 1) {
                        var successfullyApplied = data.match(/Success/g).length;
                        $('#waitSpinner').hide().before('<h3 style=color:green>' + successfullyApplied + '/' + qty + ' Credit' + (successfullyApplied > 1 ? 's' : '') + ' successfully applied');
                    }

                    $('#purchaseMessage').hide();

                    var splitArray = data.split('~');
                    if (data.toLowerCase().includes("-nc-")) {
                        for (var i = 0; i < splitArray.length; i++) {
                            var temp = splitArray[i];
                            temp = temp.split('_')[0];
                            splitArray[i] = temp;
                        }
                    }
                    //var purchasedItemID = splitArray != undefined && splitArray.length > 1 ? splitArray[1] : '';
                    var purchasedItemID = ''
                    for (var i = 0; i < splitArray.length; i++) {
                        var splitValue = splitArray[i].split('|');
                        if (splitValue != undefined && splitValue.length > 1) {
                            var id = splitValue[1];
                            purchasedItemID += id + (i == splitArray.length - 1 ? "" : "|");
                        }
                    }

                    window.location.href = "/Retail/PurchaseConfirm/<%=Model.account.AccountID%>?sku=" + SKU + "&purchasedItemID=" + purchasedItemID;
                } else {
                    $('#purchaseMessage').addClass('alert alert-danger').html(data);
                    $('#updatePaymentBtn').show();

                    if (data.includes('Card not authorized') || data.includes('[Status: DECLINED]')) {
                        $('#purchaseButton').hide();
                    } else { //if (data.includes('Failed: Unauthorized')) {
                        $('#purchaseButton').show().html('<%: Html.TranslateTag("Try Purchase again"," Try Purchase again")%>');
                    }
                }
                $('#waitSpinner').html('');
            });
        }


        document.getElementById('toBeEmailed').addEventListener('keyup', function (e) {
            let emailValue = this.value.replace(/ /g, '').replace(/;{2,}/, ';');

            if (emailValue.endsWith(';')) {
                let validEmailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                let emails = emailValue.split(';');
                for (let i = 0; i < emails.length; i++) {
                    let email = emails[i];
                    if (email.length > 0 && !email.match(validEmailRegex)) {
                        emailValue = emailValue.replace(email + ";", '');
                    }
                }
            }

            this.value = emailValue;
        });
    </script>
</asp:Content>
