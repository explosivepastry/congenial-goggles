<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<PurchaseLinkStoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PaymentOption
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid d-flex flex-column">
        <div class="col-12">

            <%:Html.Partial("RetailHeader", Model.account) %>
        </div>

        <div class="col-12 d-flex flex-wrap">
            <div class="col-md-6 col-12 px-0 mb-4 <%=MonnitSession.CurrentStoreLinkInfo != null ? "pe-md-2" : "" %>">
                <div class="rule-card_container w-100" style="min-height: 112px;">
                    <div class="card_container__top dfac">
                        <div class="card_container__top__title">
                            <div class="walleticon"><%=Html.GetThemedSVG("wallet") %></div>
                            <div style="max-width: 90%;" class="ms-2"><%: Html.TranslateTag("Retail/PaymentOption|Wallet","Wallet")%></div>
                        </div>
                    </div>
                    <div class="x_content" id="leftContent">
                        <% Html.RenderPartial("PaymentInfoList"); %>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-12 mb-4 ps-0 <%=MonnitSession.CurrentStoreLinkInfo != null ? "ps-md-2" : "" %>">
                <div class="rule-card_container w-100">
                    <div class="card_container__top dfac">
                        <div class="card_container__top__title">
                            <%=Html.GetThemedSVG("card") %>
                            <div style="max-width: 90%;" class="ms-2"><%: Html.TranslateTag("Retail/PaymentOption|Add Card to Wallet","Add Card to Wallet")%></div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content" id="rightContent">
                        <% Html.RenderPartial("NewPaymentMethod", new PaymentInfoModel()); %>
                        <div class="clearfix"></div>
                        <div id="spinner"></div>
                    </div>
                </div>
            </div>
        </div>

        <%if (MonnitSession.CurrentStoreLinkInfo != null)
            { %>
        <%Html.RenderPartial("~/Views/Retail/StoreHeaderLink.ascx", Model.account); %>
        <%} %>
    </div>


    <script type="text/javascript">
        $('.defaultPaymentChxbox').click(setDefaultCard);

        var removeCardString = "<%: Html.TranslateTag("Are you sure you want to remove this card?")%>";
        var setDefaultCardString = "<%: Html.TranslateTag("Do you want to set this card as default Auto Payment?")%>";
        var holdernameString = "<%: Html.TranslateTag("Retail/PaymentOption|Holder name required","Holder name required.")%>";
        var billingreqString =  "<%: Html.TranslateTag("Retail/PaymentOption|Postal code required","Postal code required.")%>";
        var countryreqString =  "<%: Html.TranslateTag("Retail/PaymentOption|Country required","Country required.")%>";
        var addressreqString =  "<%: Html.TranslateTag("Retail/PaymentOption|Address required","Address required.")%>";
        var cityreqString = "<%: Html.TranslateTag("Retail/PaymentOption|City required","City required.")%>";

        var passwordreqString = "<%: Html.TranslateTag("Retail/PaymentOption|Password Required","Password Required.")%>";
        var expiryValidString = "<%: Html.TranslateTag("Retail/PaymentOption|Invalid Expiration","Invalid Expiration.")%>";
        var usernamereqString = "<%: Html.TranslateTag("Retail/PaymentOption|UserName Required","UserName Required.")%>";
        var cardnumberString =  "<%: Html.TranslateTag("Retail/PaymentOption|Invalid card number","Invalid card number.")%>";

        $(document).ready(function () {

            $('#cardNumber').payform('formatCardNumber');

            $('#cardNumber').keyup(function () {

                if ($.payform.validateCardNumber($('#cardNumber').val()) == false) {
                    $('#card-number-field').addClass('has-error');
                    $('#cardNumber').removeClass('is-valid');
                    $('#cardNumber').addClass('is-invalid');
                } else {
                    $('#card-number-field').removeClass('has-error');
                    $('#card-number-field').addClass('has-success');
                    $('#cardNumber').removeClass('is-invalid');
                    $('#cardNumber').addClass('is-valid');
                }
            });

            $('body').on('click', '.checkoutCardBtn', function () {
                var profileID = $(this).attr('data-id');
                var sku = $('#hiddenPurchaseSku').val();
                var qty = $('#hiddenPurchaseQty').val();

                window.location.href = "/Retail/PurchasePreview/<%=MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.AccountID > 0 ? MonnitSession.CurrentCustomer.AccountID.ToString() : ""%>?profileID=" + profileID + "&sku=" + sku + "&qty=" + qty;
            });

            $('#newPaymentDiv')[0].setAttribute('tabindex', '0');
            $('#newPaymentDiv')[0].addEventListener('focus', function () { $('#cardNumber').keyup(); }, { once: true });


            $('#saveCC').click(function (e) {
                e.preventDefault();

                if ($(this).hasClass("disabled"))
                    return false;
                $this = $(this);

                $this.addClass("disabled");
                let errorString = "";
                var isCardValid = $.payform.validateCardNumber($('#cardNumber').val());
                var isExpiryValid = $.payform.validateCardExpiry(Number($('#expirationMonth').val()), Number($('#expirationYear').val()));

                setTimeout(allowClick, 4 * 1000);

                var allValid = true;
                if ($('#cardHolder').val().length < 5) {
                    allValid = false;
                    errorString += holdernameString + "\n"
                }
                if (!isExpiryValid) {
                    allValid = false;
                    errorString += expiryValidString + "\n"
                }

                if (!isCardValid) {
                    allValid = false;
                    errorString += cardnumberString + "\n"
                }

                if ($('#zipcode').val() == "") {
                    allValid = false;
                    errorString += billingreqString + "\n"
                }

                if ($('#city').val() == "") {
                    allValid = false;
                    errorString += cityreqString + "\n"
                }

                if ($('#address1').val() == "") {
                    allValid = false;
                    errorString += addressreqString + "\n"
                }

                if ($('#country').val() == "") {
                    allValid = false;
                    errorString += countryreqString + "\n"
                }

                if (!allValid) {
                    toastBuilder(errorString);
                    $('#CCsaving').hide();
                    $('#saveCC').show();
                }
                else {
                    $.post('/Retail/SubmitNewPayment/<%=Model.account.AccountID%>', $("#creditCardForm").serialize(), function (data) {
                        $('#CCsaving').hide();
                        $('#saveCC').show();
                        if (data.startsWith("Redirect|")) {
                            var url = data.split("|")[1];
                            window.location.href = url;
                        } else {
                            $('#leftContent').html(data);
                            if (document.querySelector(".ccard")) {
                                toastBuilder("Success");
                                $("#creditCardForm")[0].reset();
                                $('#cardNumber').removeClass('is-valid', 'has-error');
                            }
                        }
                    });
                }
            });
        });

        function removeCard(profileid) {
            var sku = $('#hiddenPurchaseSku').val();
            var qty = $('#hiddenPurchaseQty').val();

            let values = {};
            values.url = `/Retail/RemovePaymentMethod/<%=Model.account.AccountID%>?profileID=${profileid}&userID=<%=Model.account.StoreUserID%>&StoreLinkGuid=<%=Model.account.StoreLinkGuid%>&sku=${sku}&qty=${qty}`;
            values.text = removeCardString;
            values.partialTag = $('#leftContent');
            openConfirm(values);
        }

        function setDefaultCard() {
            $(this).prop('checked', !$(this).prop('checked'));
            if ($(event.target).prop("checked") == false) {
                profileid = $(this).data("profileid"); 
                let values = {};
                values.url = `/Retail/SetDefaultPaymentMethod/<%=Model.account.AccountID%>?profileID=${profileid}&userID=<%=Model.account.StoreUserID%>&StoreLinkGuid=<%=Model.account.StoreLinkGuid%>`;
                values.text = setDefaultCardString;
                values.partialTag = $('#leftContent');
                values.callback = function (data) {

                    if (data == 'Success') {
                        $('.defaultPaymentChxbox:checked').prop("checked", false);
                        $('#defaultPaymentChxbox_' + profileid).prop("checked", true);

                    } else {
                        showSimpleMessageModal(data)
                    }
                }
                openConfirm(values);
            }
            else {
                $(event.target).prop("checked", true)
            }             
        }
        

        function allowClick() {
            $this.removeClass("disabled");
        }

    </script>

</asp:Content>
